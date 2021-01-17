using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Topping : AModel
    {
        public string Name { get; set; }

        public Topping() {}

        public Topping(string name)
        {
            Name = name;
        }
    }
}