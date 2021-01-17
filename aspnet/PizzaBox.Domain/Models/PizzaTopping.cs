using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class PizzaTopping : AModel
    {
        public APizzaModel Pizza { get; set; }
        public Topping Topping { get; set; }

        public PizzaTopping() {}

        public PizzaTopping(APizzaModel pizza, Topping topping)
        {
            Pizza = pizza;
            Topping = topping;
        }
    }
}