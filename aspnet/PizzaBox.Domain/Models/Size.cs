using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
    public class Size : AModel
    {
        public string Name { get; set; }

        public int Inches { get; set; }

        public decimal Price { get; set; }

        public Size() {}

        public Size(string name, int inches, decimal price)
        {
            Name = name;
            Inches = inches;
            Price = price;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}