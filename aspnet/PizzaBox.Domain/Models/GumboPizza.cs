using PizzaBox.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class GumboPizza : APizzaModel
    {
        protected override void AddName()
        {
            Name = "Gumbo Pizza";
        }
        protected override void AddTypePrice()
        {
            TypePrice = 7.99m;
        }

        public override void AddToppings(List<Topping> availableToppings)
        {   
            PizzaToppings = new List<PizzaTopping>
            {
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Cheese")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Tomato Sauce")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Shrimp")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Crab"))
            };
        }
    }
}