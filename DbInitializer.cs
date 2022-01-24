using System;
using System.Linq;
using PizzaDelivery.Models;

namespace PizzaDelivery
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Pizzas.Any())
            {
                return;   // DB has been seeded
            }

            //sample data get from https://www.dominos.ca/en/pages/order/menu#!/menu/category/entrees/
            var pizzas = new Pizza[]
              {
                new Pizza {
                    Name = "Cheeseburger",
                    Description="Ketchup-mustard sauce, beef crumble, fresh onions, and fresh tomatoes, topped with 3 different types of cheese.​​",
                    Price=15,
                    ImagePath="images/01.jpg"},
                new Pizza {
                    Name = "ExtravaganZZa",
                    Description="Loads of pepperoni, ham, savory Italian sausage, beef crumble, fresh onions, fresh green peppers, fresh mushrooms and black olives with extra cheese.",
                    Price= 20,
                    ImagePath="images/02.jpg"},
                new Pizza {
                    Name = "Brooklyn Pizza",
                    Description="Specifically engineered to be big, thin, and perfectly foldable.",
                    Price=14,
                    ImagePath="images/03.jpg"},
                new Pizza {
                    Name = "Veggie",
                    Description="A medley of fresh green peppers, onion, tomatoes, mushrooms, and olives.",
                    Price=8,
                    ImagePath="images/04.jpg"}

              };

            foreach (var p in pizzas)
            {
                context.Pizzas.Add(p);
            }

            context.SaveChanges();

        }
    }
}
