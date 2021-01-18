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

        string orderJson = JsonSerializer.Serialize(order);

        TempData["Order"] = orderJson;

        PizzaViewModel Pizza = new PizzaViewModel()
        {
          AvailablePizzaNames = new List<string>() { "Meat", "Pineapple", "Gumbo" }, // TODO: This is temp. Get the types from the db later
          AvailableCrustNames = _ctx.GetCrustNames(),
          AvailableSizeNames = _ctx.GetSizeNames()
        };

        return View("SelectPizza", Pizza);
      }
      return View("home", model);
    }

    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public void AddPizza(PizzaViewModel model) // Only provides the properties that were submitted in the form
    {
      var Order = JsonSerializer.Deserialize<Order>(TempData["Order"].ToString());

      if (ModelState.IsValid)
      {
        Order.AddSpecifiedPizza(
          model.ChosenPizza,
          model.ChosenCrust,
          model.ChosenSize,
          _ctx.GetCrusts(),
          _ctx.GetSizes(),
          _ctx.GetToppings()
        );

        System.Console.Write(Order);
      }
      //return View("home", model);
    }
  }
}
