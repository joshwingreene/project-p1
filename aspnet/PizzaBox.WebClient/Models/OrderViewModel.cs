using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.WebClient.Models
{
  public class OrderViewModel
  {
    public List<string> Stores { get; set; }

    [Required]
    public string Store { get; set; }

    public List<PizzaViewModel> Pizzas { get; set; }

    public OrderViewModel()
    {
      Pizzas = new List<PizzaViewModel>();
    }

    public decimal GetCurrentTally()
    {
        decimal total = 0.0m;

        foreach (var pizza in Pizzas)
        {
            total += pizza.GetTotalPrice();
        }

        return total;
    }
  }
}
