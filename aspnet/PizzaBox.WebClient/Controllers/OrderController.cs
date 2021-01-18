using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;
using PizzaBox.WebClient.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PizzaBox.WebClient.Controllers
{
  [Route("[controller]")] // route parser
  public class OrderController : Controller
  {
    private readonly PizzaBoxRepository _ctx;

    public OrderController(PizzaBoxRepository context)
    {
      _ctx = context;
    }

    private string SerializeOrder(Order order)
    {
      return JsonSerializer.Serialize(order);
    }

    private Order DeserializeOrder(object orderTempData)
    {
      return JsonSerializer.Deserialize<Order>(orderTempData.ToString());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Post(OrderViewModel model)
    {
      if (ModelState.IsValid)
      {
        var order = new Order()
        {
          DateModified = DateTime.Now,
          Store = _ctx.GetStores().FirstOrDefault(s => s.Name == model.Store)
        };

        _ctx.AddOrder(order);
        _ctx.SaveChanges();

        return View("OrderPlaced");
      }

      return View("home", model);
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(OrderViewModel model) // Only provides the properties that were submitted in the form
    {
      if (ModelState.IsValid)
      {
        var order = new Order()
        {
          Store = _ctx.GetStores().FirstOrDefault(s => s.Name == model.Store)
        };

        ViewData["Title"] = "Select Pizza";
        TempData["Order"] = SerializeOrder(order);

        PizzaViewModel PizzaVM = new PizzaViewModel()
        {
          AvailablePizzaNames = new List<string>() { "Meat", "Pineapple", "Gumbo" }, // TODO: This is temp. Get the types from the db later
          AvailableCrustNames = _ctx.GetCrustNames(),
          AvailableSizeNames = _ctx.GetSizeNames()
        };

        return View("SelectPizza", PizzaVM);
      }
      return View("home", model);
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public IActionResult AddPizza(PizzaViewModel model) // Only provides the properties that were submitted in the form
    {
      var Order = DeserializeOrder(TempData["Order"]);

      if (ModelState.IsValid)
      {
        // TODO: Thinking of changing this to create a Pizza object directly just for the purpose of its parts being displayed
        Order.AddSpecifiedPizza(
          model.ChosenPizza,
          model.ChosenCrust,
          model.ChosenSize,
          _ctx.GetCrusts(),
          _ctx.GetSizes(),
          _ctx.GetToppings()
        );

        var PizzaVM = new PizzaViewModel() { ChosenPizza = model.ChosenPizza, ChosenCrust = model.ChosenCrust, ChosenSize = model.ChosenSize };

        var OrderVM = new OrderViewModel()
        {
          Store = Order.Store.Name,
          Pizzas = new List<PizzaViewModel>() { PizzaVM }
        };

        //System.Console.Write(Order);

        ViewData["Title"] = "Current Tally";
        TempData["OrderVM"] = JsonSerializer.Serialize(OrderVM);

        return View("TallyAndOptions", Order);
      }
      return View("home", model);
    }

    [HttpPost("checkout")]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(Order model)
    {
      if (ModelState.IsValid)
      {
        // Generate an order from the saved view model
        var OrderVM = JsonSerializer.Deserialize<OrderViewModel>(TempData["OrderVM"].ToString());

        var Order = new Order()
        {
          DateModified = DateTime.Now,
          Store = _ctx.GetStores().FirstOrDefault(s => s.Name == OrderVM.Store),
        };

        foreach (var pVM in OrderVM.Pizzas)
        {
          Order.AddSpecifiedPizza(
            pVM.ChosenPizza,
            pVM.ChosenCrust,
            pVM.ChosenSize,
            _ctx.GetCrusts(), // TODO: Thinking that this should only happen once per Order
            _ctx.GetSizes(),
            _ctx.GetToppings()
          );
        }

        _ctx.AddOrder(Order);
        _ctx.SaveChanges();

        return View("OrderPlaced");
      }

      return View("home", model);
    }
  }
}
