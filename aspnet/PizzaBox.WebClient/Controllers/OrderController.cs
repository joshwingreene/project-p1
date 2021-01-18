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

    private string SerializeOrderViewModel(OrderViewModel model)
    {
      return JsonSerializer.Serialize(model);
    }

    private OrderViewModel DeserializeOrderViewModel(object modelTempData)
    {
      return JsonSerializer.Deserialize<OrderViewModel>(modelTempData.ToString());
    }

    /*
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
    */

    private decimal GetSpecifiedSizePrice(string sizeName)
    {
      foreach (var s in _ctx.GetSizes())
      {
          if (s.Name == sizeName)
          {
            return s.Price;
          }
      }

      return 0.0m;
    }

    private decimal GetSpecifiedCrustPrice(string crustName)
    {
      foreach (var c in _ctx.GetCrusts())
      {
          if (c.Name == crustName)
          {
            return c.Price;
          }
      }

      return 0.0m;
    }

    private PizzaViewModel InitPizzaViewModel()
    {
      return new PizzaViewModel()
        {
          AvailablePizzaNames = new List<string>() { "Meat", "Pineapple", "Gumbo" }, // TODO: This is temp. Get the types from the db later
          AvailableCrustNames = _ctx.GetCrustNames(),
          AvailableSizeNames = _ctx.GetSizeNames()
        };
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateOrder(OrderViewModel model) // Only provides the properties that were submitted in the form
    {
      if (ModelState.IsValid)
      {
        ViewData["Title"] = "Select Pizza";
        TempData["OrderVM"] = SerializeOrderViewModel(model);

        PizzaViewModel PizzaVM = InitPizzaViewModel();

        return View("SelectPizza", PizzaVM);
      }
      return View("home", model);
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public IActionResult AddPizza(PizzaViewModel pizzaViewModel) // Only provides the properties that were submitted in the form
    {
      if (ModelState.IsValid)
      {
        var OrderVM = DeserializeOrderViewModel(TempData["OrderVM"]);

        pizzaViewModel.TypePrice = Order.GetSpecifiedPizzaTypePrice(pizzaViewModel.ChosenPizza);
        pizzaViewModel.CrustPrice = GetSpecifiedCrustPrice(pizzaViewModel.ChosenCrust);
        pizzaViewModel.SizePrice = GetSpecifiedSizePrice(pizzaViewModel.ChosenSize);
        
        OrderVM.Pizzas.Add(pizzaViewModel);

        ViewData["Title"] = "Current Tally";
        TempData["OrderVM"] = SerializeOrderViewModel(OrderVM);

        return View("TallyAndOptions", OrderVM);
      }
      return View("home", pizzaViewModel);
    }

    [HttpGet("more")]
    public IActionResult AddMorePizzas()
    {
      ViewData["Title"] = "Select Pizza";

      PizzaViewModel PizzaVM = InitPizzaViewModel();

      return View("SelectPizza", PizzaVM);
    }

    [HttpPost("checkout")]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(Order model)
    {
      if (ModelState.IsValid)
      {
        // Generate an order from the saved view model
        var OrderVM = DeserializeOrderViewModel(TempData["OrderVM"]);

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
