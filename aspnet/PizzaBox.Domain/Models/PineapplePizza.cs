using PizzaBox.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class PineapplePizza : APizzaModel
    {
        protected override void AddName()
        {
            Name = "Pineapple Pizza";
        }
        protected override void AddTypePrice()
        {
            TypePrice = 5.99m;
        }

        public override void AddToppings(List<Topping> availableToppings)
        {   
            PizzaToppings = new List<PizzaTopping>
            {
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Cheese")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Pepperoni")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Pineapple")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Tomato Sauce"))
            };
        }
    }
}