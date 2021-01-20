using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PizzaBox.Storing;
using PizzaBox.WebClient.Models;
using System.Collections.Generic;
using PizzaBox.Domain;
using System;

namespace PizzaBox.WebClient.Controllers
{
  [Route("[controller]")] // route parser
  public class AppController : Controller
  {
    private readonly PizzaBoxRepository _ctx;
    public AppController(PizzaBoxRepository context)
    {
      _ctx = context;
    }

    [HttpGet]
    public IActionResult Home()
    {
      return View("home");
    }
  }
}
