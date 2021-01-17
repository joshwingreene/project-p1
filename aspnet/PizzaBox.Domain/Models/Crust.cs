using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Crust : AModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Crust() {}

        public Crust(string name, decimal price) {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}