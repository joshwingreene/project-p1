using Microsoft.Extensions.Configuration;

namespace PizzaBox.WebClient.Models
{
  public class CustomerViewModel
  {
    public string Username { get; set; }

    public bool UsernameWasTaken { get; set; }

    public OrderViewModel Order { get; set; }

    public CustomerViewModel()
    {
      //Name = "fred";
      Order = new OrderViewModel();
    }
  }
}
