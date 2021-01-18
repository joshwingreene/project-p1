using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzaBox.WebClient.Models
{
  public class PizzaViewModel
  {
    public List<string> AvailablePizzaNames { get; set; }

    public List<string> AvailableCrustNames { get; set; }

    public List<string> AvailableSizeNames { get; set; }

    [Required]
    public string ChosenPizza { get; set; }

    [Required]
    public string ChosenCrust { get; set; }

    [Required]
    public string ChosenSize { get; set; }

    public decimal TypePrice { get; set; }

    public decimal SizePrice { get; set; }

    public decimal CrustPrice { get; set; }

    public decimal GetTotalPrice()
    {
      return TypePrice + SizePrice + CrustPrice;
    }
  }
}
