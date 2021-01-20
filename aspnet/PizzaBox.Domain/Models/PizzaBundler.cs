using PizzaBox.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaBox.Domain.Models
{
    public class PizzaBundler : AModel
    {
        public List<APizzaModel> meatPizzas { get; set; }
        public List<APizzaModel> pineapplePizzas { get; set; }
        public List<APizzaModel> gumboPizzas { get; set; }

        public PizzaBundler(List<APizzaModel> meat, List<APizzaModel> pineapple, List<APizzaModel> gumbo) 
        {
            meatPizzas = meat;
            pineapplePizzas = pineapple;
            gumboPizzas = gumbo;
        }
    }
}