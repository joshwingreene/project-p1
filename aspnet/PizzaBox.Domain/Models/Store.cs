using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
  public class Store : AModel
  {
    public string Name { get; set; }
    public List<Order> Orders { get; set; }

    public Store()
    {
        Orders = new List<Order>();
    }

    public void CreateOrder()
    {
        Orders.Add(new Order());
    }

    public bool DeleteOrder(Order order) // "tell me what to delete"
    {
        try
        {
            Orders.Remove(order);

            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            GC.Collect(); // tells GC that whatever is here is ready to be cleaned out (level 1 or its critical that we need more memory)
        }
    }

    public override string ToString()
    {
        return Name;
    }
  }
}
