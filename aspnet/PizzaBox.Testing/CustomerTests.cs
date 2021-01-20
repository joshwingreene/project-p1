using Xunit;
using PizzaBox.Domain.Models;
using System.Collections.Generic;

namespace PizzaWorld.Testing
{
    public class CustomerTests
    {
        [Fact]
        private void Test_UserExists()
        {
            // arrange
            var sut = new Customer("test");

            // act
            var actual = sut;

            // assert
            Assert.IsType<Customer>(actual);
            Assert.NotNull(actual);
        }

        [Fact]
        private void Test_CheckNumberOfOrders()
        {
            // arrange
            var user = new Customer("test");
            var order = new Order();
            var meat = new MeatPizza();
            var pineapple = new PineapplePizza();

            order.Pizzas.Add(meat);
            order.Pizzas.Add(pineapple);

            // act
            user.Orders.Add(order);
            var actual = user;

            // assert
            Assert.True(actual.Orders.Count == 1);
        }
        
        /*
        [Fact]
        private void Test_CheckToString()
        {
            // arrange
            var user = new Customer("test");
            var store = new Store();
            store.Name = "First Store";
            user.SelectedStore = store;
            var order = new Order();
            var meat = new MeatPizza();
            var pineapple = new PineapplePizza();

            List<Crust> crusts = new List<Crust>()
            {
                new Crust { EntityId = 1, Name = "Thin", Price = .99m },
                new Crust { EntityId = 2, Name = "Regular", Price = 1.99m },
                new Crust { EntityId = 3, Name = "Large", Price = 2.99m }
            };

            List<Size> sizes = new List<Size>()
            {
                new Size { EntityId = 1, Name = "Small", Inches = 10, Price = .99m },
                new Size { EntityId = 2, Name = "Medium", Inches = 12, Price = 2.99m },
                new Size { EntityId = 3, Name = "Large", Inches = 14, Price = 4.99m }
            };

            List<Topping> toppings = new List<Topping>()
            {
                    new Topping { EntityId = 1, Name = "Cheese"},
                    new Topping { EntityId = 2, Name = "Pepperoni"},
                    new Topping { EntityId = 3, Name = "Sausage"},
                    new Topping { EntityId = 4, Name = "Pineapple"}
            };

            meat.AddCrust(crusts);
            meat.AddSize(sizes);
            meat.AddToppings(toppings);

            pineapple.AddCrust(crusts);
            pineapple.AddSize(sizes);
            pineapple.AddToppings(toppings);

            order.Pizzas.Add(meat);
            order.Pizzas.Add(pineapple);

            user.Orders.Add(order);

            // act
            var actual = user.ToString();

            // assert
            Assert.True(actual == "you have selected this store: First Store and ordered these pizzas: Meat Pizza\nPineapple Pizza\n");
        }
        */
    }
}