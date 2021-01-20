using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing
{
  public class PizzaBoxRepository
  {
    private PizzaBoxContext _ctx;

    public PizzaBoxRepository(PizzaBoxContext context)
    {
      _ctx = context;
    }

    public bool CheckIfUsernameExists(string username)
    {
        return null != _ctx.Customers.FirstOrDefault<Customer>(c => c.Username == username);
    }

    public Customer GetCustomer(string username)
    {
        return _ctx.Customers.FirstOrDefault<Customer>(c => c.Username == username);
    }

    public void AddOrder(Order order)
    {
      _ctx.Orders.Add(order);
    }

    public void SaveCustomer(Customer customer)
    {
        _ctx.Add(customer);
        SaveChanges();
    }

    public Store ReadStore(string name)
    {
        return _ctx.Stores.FirstOrDefault<Store>(store => store.Name == name);
    }

    public void SaveChanges()
    {
      _ctx.SaveChanges();
    }

    public IEnumerable<Store> GetStores()
    {
      return _ctx.Stores;
    }

    public List<string> GetStoreNames()
    {
      return _ctx.Stores.Select(s => s.Name).ToList();
    }

    public List<string> GetCrustNames()
    {
      return _ctx.Crusts.Select(c => c.Name).ToList();
    }

    public List<string> GetSizeNames()
    {
      return _ctx.Sizes.Select(s => s.Name).ToList();
    }

    public List<Topping> GetToppings()
    {
        return _ctx.Toppings.ToList();
    }

    public List<Crust> GetCrusts()
    {
        return _ctx.Crusts.ToList();
    }
    
    public List<Size> GetSizes()
    {
        return _ctx.Sizes.ToList();
    }

    /*
    public IEnumerable<T> Get<T>() where T : AModel // TODO: Enhancement: Change AModel so it includes a name property so the first line here can be used
    {
      // return _ctx.Set<T>().Select(t => t.Name).ToList();
      // return _ctx.Set<T>().Select(t => t.GetType().GetProperty()).ToList();
    }
    */
  }
}
