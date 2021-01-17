using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{
    public class APizzaModel : AModel // no longr abstract bc creating a new migration expects concrete classes
    {
        public string Name { get; set; }
        public decimal TypePrice { get; set; }
        public Crust Crust { get; set; }
        public Size Size { get; set; }
        public List<PizzaTopping> PizzaToppings { get; set; }

        protected APizzaModel()
        {
            AddName();
            AddTypePrice();
        }

        public decimal GetTotalPrice() 
        {
            return Crust.Price + Size.Price + TypePrice;
        }

        public void AddCrust(List<Crust> availCrusts)
        {
            Crust = availCrusts.Find(c => c.Name == "Regular");
        }

        public void AddSize(List<Size> availSizes)
        {
            Size = availSizes.Find(s => s.Name == "Medium");
        }

        public override string ToString()
        {
            return Name;
        }

        protected virtual void AddName() {}
        protected virtual void AddTypePrice() {}
        public virtual void AddToppings(List<Topping> availableToppings) {}
    }
}