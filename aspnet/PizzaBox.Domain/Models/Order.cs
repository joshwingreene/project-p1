using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Factories;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace PizzaBox.Domain.Models
{
  public class Order : AModel, IComparable<Order>
  {
    public Store Store { get; set; }
    public long StoreEntityId { get; set; }
    public DateTime DateModified { get; set; }

    private GenericPizzaFactory _pizzaFactory = new GenericPizzaFactory();

    public List<APizzaModel> Pizzas { get; set; }

    public Order()
    {
        Pizzas = new List<APizzaModel>();
    }

    public int CompareTo(Order order)
    {
        if (EntityId < order.EntityId)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void PrintPriceOfLastPizza()
    {
        Console.WriteLine(Pizzas.Last().GetTotalPrice());
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

    public void PrintPizzas()
    {
        var sb = new StringBuilder();

        foreach(var p in Pizzas)
        {
            sb.AppendLine("" + p);
        }

        System.Console.WriteLine("\nYour order includes the following pizzas:\n");
        System.Console.WriteLine(sb);
    }

    private string GetPriceInParenthesis(decimal number)
    {
        return " ($" + number + ")";
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        APizzaModel currentPizza;

        for (var p = 0; p < Pizzas.Count; p++)
        {
            currentPizza = Pizzas[p];

            sb.AppendLine("Pizza #" + (p+1));
            sb.AppendLine("- Type: " + currentPizza + GetPriceInParenthesis(currentPizza.TypePrice));
            sb.AppendLine("- Crust: " + currentPizza.Crust + GetPriceInParenthesis(currentPizza.Crust.Price));
            sb.AppendLine("- Size: " + currentPizza.Size + GetPriceInParenthesis(currentPizza.Size.Price) + "\n");
        }
        sb.AppendLine("Total Price: $" + GetCurrentTally());

        return sb.ToString();
    }

    public void ChangeLastPizzaSize(string sizeName, List<Size> availSizes)
    {
        var lastPizza = Pizzas.Last();

        switch (sizeName)
        {
            case "Small":
                lastPizza.Size = availSizes.Find(s => s.Name == "Small");
                break;
            case "Medium":
                lastPizza.Size = availSizes.Find(s => s.Name == "Medium");
                break;
            case "Large":
                lastPizza.Size = availSizes.Find(s => s.Name == "Large");
                break;
        }
    }

    public void ChangePizzaSize(int index, string sizeStr, List<Size> availSizes)
    {
        var selectedPizza = Pizzas[index];

        switch (sizeStr)
        {
            case "Small":
                selectedPizza.Size = availSizes.Find(c => c.Name == "Small");
                break;
            case "Medium":
                selectedPizza.Size = availSizes.Find(c => c.Name == "Medium");
                break;
            case "Large":
                selectedPizza.Size = availSizes.Find(c => c.Name == "Large");
                break;
        }
    }

    public void ChangePizzaCrust(int index, string crustStr, List<Crust> availCrusts)
    {
        var selectedPizza = Pizzas[index];

        switch (crustStr)
        {
            case "Thin":
                selectedPizza.Crust = availCrusts.Find(c => c.Name == "Thin");
                break;
            case "Regular":
                selectedPizza.Crust = availCrusts.Find(c => c.Name == "Regular");
                break;
            case "Large":
                selectedPizza.Crust = availCrusts.Find(c => c.Name == "Large");
                break;
        }
    }
    
    public void RemovePizza(int index)
    {
        Pizzas.RemoveAt(index);
    }

    public bool CheckIfZeroPizzas()
    {
        return Pizzas.Count == 0;
    }

    public void AddSpecifiedPizza(string pizzaType, string crustName, string sizeName, List<Crust> availCrusts, List<Size> availSizes, List<Topping> availToppings)
    {
        switch(pizzaType)
        {
            case "Meat":
                MakeMeatPizza(crustName, sizeName, availCrusts, availSizes, availToppings);
                break;
            case "Pineapple":
                MakePineapplePizza(crustName, sizeName, availCrusts, availSizes, availToppings);
                break;
            case "Gumbo":
                MakeGumboPizza(crustName, sizeName, availCrusts, availSizes, availToppings);
                break;
            default:
                break;
        }
    }

    private void AddMajorPizzaParts(APizzaModel currentPizza, string crustName, string sizeName, List<Crust> availCrusts, List<Size> availSizes, List<Topping> availToppings)
    {
        currentPizza.AddCrust(availCrusts, crustName);
        currentPizza.AddSize(availSizes, sizeName);
        currentPizza.AddToppings(availToppings);
    }

    public void MakeMeatPizza(string crustName, string sizeName, List<Crust> availCrusts, List<Size> availSizes, List<Topping> availToppings)
    {
        Pizzas.Add(_pizzaFactory.Make<MeatPizza>());
        AddMajorPizzaParts(Pizzas.Last(), crustName, sizeName, availCrusts, availSizes, availToppings);
    }

    public void MakePineapplePizza(string crustName, string sizeName, List<Crust> availCrusts, List<Size> availSizes, List<Topping> availToppings)
    {
        Pizzas.Add(_pizzaFactory.Make<PineapplePizza>());
        AddMajorPizzaParts(Pizzas.Last(), crustName, sizeName, availCrusts, availSizes, availToppings);
    }

    public void MakeGumboPizza(string crustName, string sizeName, List<Crust> availCrusts, List<Size> availSizes, List<Topping> availToppings)
    {
        Pizzas.Add(_pizzaFactory.Make<GumboPizza>());
        AddMajorPizzaParts(Pizzas.Last(), crustName, sizeName, availCrusts, availSizes, availToppings);
    }
  }
}
