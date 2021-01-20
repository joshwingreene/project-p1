using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PizzaBox.Storing;
using PizzaBox.WebClient.Models;
using System.Collections.Generic;
using PizzaBox.Domain.Models;
using System;

namespace PizzaBox.WebClient.Controllers
{
  [Route("[controller]")] // route parser
  public class CustomerController : Controller
  {
    private readonly PizzaBoxRepository _ctx;
    public CustomerController(PizzaBoxRepository context)
    {
      _ctx = context;
    }

    [HttpGet("customer")]
    public IActionResult Account()
    {
      return View("CreateAccountOrLogin", new CustomerViewModel());
    }

    [HttpPost("create_account")]
    public IActionResult CreateAccount(CustomerViewModel customerViewModel) // TODO: Check if the ModelState is valid (removed it bc the model was not valid for some reason)
    {
      System.Console.WriteLine("CreateAccount");
      System.Console.WriteLine("Given value: " + customerViewModel.Username);

      // check if the username can be found
      if (_ctx.CheckIfUsernameExists(customerViewModel.Username))
      {
        // ask for the username again and say that there was an issue
        return View("CreateAccountOrLogin", new CustomerViewModel() { UsernameWasTaken = true });
      }
      else 
      {
        var ChosenUsername = customerViewModel.Username;

        // if not, insert the username into the database
        var NewCustomer = new Customer(ChosenUsername); 

        _ctx.SaveCustomer(NewCustomer);

        // will return this model in order to get access to its name after a store is chosen
        customerViewModel.Order = new OrderViewModel()
        {
          Stores = _ctx.GetStoreNames()
        };

        TempData["Username"] = ChosenUsername;

        return View("Order", customerViewModel);
      }
      
      //System.Console.WriteLine("model not valid");
      //return View("CreateAccountOrLogin", customerViewModel);
    }

    [HttpPost("login")]
    public IActionResult Login(CustomerViewModel customerViewModel) // model is not valid as well
    {
      //System.Console.WriteLine("Login");
      //System.Console.WriteLine(ModelState.IsValid ? "model is valid" : "model is not valid");

      // will return this model in order to get access to its name after a store is chosen
      customerViewModel.Order = new OrderViewModel()
      {
        Stores = _ctx.GetStoreNames()
      };

      TempData["Username"] = customerViewModel.Username;

      return View("Order", customerViewModel);
    }

    [HttpGet("start_order_process")]
    public IActionResult StartOrder()
    {
      var customer = new CustomerViewModel();

      customer.Order = new OrderViewModel()
      {
        Stores = _ctx.GetStoreNames()
      };

      return View("Order", customer.Order);
    }

    [HttpGet("order_history")]
    public IActionResult ViewOrderHistory()
    {
      var Username = TempData["Username"].ToString();

      var Orders = _ctx.GetCustomerOrderHistory(_ctx.GetCustomer(Username));

      Orders.Sort();
      
      TempData["Username"] = Username;

      return View("OrderHistory", Orders);
    }
  }
}
