using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing;

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
