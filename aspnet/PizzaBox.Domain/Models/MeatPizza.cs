using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class MeatPizza : APizzaModel
    {
        protected override void AddName()
        {
            Name = "Meat Pizza";
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
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Sausage")),
                new PizzaTopping(this, availableToppings.Find(t => t.Name == "Tomato Sauce")),
            };
        }
    }
}