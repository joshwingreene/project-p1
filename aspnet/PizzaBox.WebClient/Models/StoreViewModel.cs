using System.Collections.Generic;

namespace PizzaBox.WebClient.Models
{
  public class StoreViewModel
  {
    public List<string> Stores { get; set; }

    public string Name { get; set; }

    public StoreViewModel()
    {
    }
  }
}