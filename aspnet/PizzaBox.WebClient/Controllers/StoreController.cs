using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing;
using PizzaBox.WebClient.Models;
using PizzaBox.Domain.Abstracts;
using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.WebClient.Controllers
{
  [Route("[controller]")] // route parser
  public class StoreController : Controller
  {
    private readonly PizzaBoxRepository _ctx;
    public StoreController(PizzaBoxRepository context)
    {
      _ctx = context;
    }

    [HttpGet("store")]
    public IActionResult SelectStore()
    {
      System.Console.WriteLine("SelectStore");

      return View("SelectStoreWithoutOrder", new StoreViewModel() { Stores = _ctx.GetStoreNames() });
    }

    [HttpPost("view_store_options")]
    public IActionResult StoreSelected(StoreViewModel storeViewModel)
    {
        //System.Console.WriteLine("selected name: " + storeViewModel.Name);

        TempData["StoreName"] = storeViewModel.Name;

        return View("StoreViewingOptions");
    }

    [HttpGet("order_history")]
    public IActionResult StoreOrderHistory()
    {
      var StoreName = TempData["StoreName"].ToString();

      // Get the orders
      var Store = _ctx.ReadStore(StoreName);

      var Orders = _ctx.GetStoreOrders(Store);

      TempData["StoreName"] = StoreName;

      Orders.Sort();

      return View("StoreOrderHistory", Orders );
    }

    [HttpGet("sales")]
    public IActionResult StoreSalesHistory()
    {
      var StoreName = TempData["StoreName"].ToString();

      // Get the sales
      var Orders = _ctx.GetStoreOrders(_ctx.ReadStore(StoreName));

      // Filter into three piles of pizzas
      List<APizzaModel> meatPizzas = new List<APizzaModel>();
      List<APizzaModel> pineapplePizzas = new List<APizzaModel>();
      List<APizzaModel> gumboPizzas = new List<APizzaModel>();

      foreach (var o in Orders)
      {
          meatPizzas.AddRange(o.Pizzas.FindAll(p => p.Name == "Meat Pizza"));
          pineapplePizzas.AddRange(o.Pizzas.FindAll(p => p.Name == "Pineapple Pizza"));
          gumboPizzas.AddRange(o.Pizzas.FindAll(p => p.Name == "Gumbo Pizza"));
      }

      var PB = new PizzaBundler(meatPizzas, pineapplePizzas, gumboPizzas);

      TempData["StoreName"] = StoreName;

      return View("StoreSales", PB);
    }
  }
}