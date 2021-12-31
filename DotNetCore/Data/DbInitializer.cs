using jschmitt2747ex1i.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jschmitt2747ex1i.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchedulerContext context)
        {
            context.Database.EnsureCreated();

            if (context.ComponentProducts.Any())
            {
                return;
            }

            var Categories = new Category[]
            {
                new Category
                {
                    CategoryName = "Beef"
                },
                new Category
                {
                    CategoryName = "Chicken"
                },
                new Category
                {
                    CategoryName = "Fish"
                }
            };

            foreach(Category cat in Categories)
            {
                context.Categories.Add(cat);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product{
                    ProductName = "Hamburger Bun",
                    ProductDescription = "Generic Hamburger Bun "
                },
                new Product{
                    ProductName = "Hamburger Patty",
                    ProductDescription = "Generic Hamburger Patty 1/4 Pound"
                },
                new Product{
                    ProductName = "Cheese Slice",
                    ProductDescription = "American Cheese - Single Slice"
                },
                new Product{
                    ProductName = "Fish Patty",
                    ProductDescription = "Generic Cod Fish Filet"
                },
                new Product{
                    ProductName = "Chicken Patty",
                    ProductDescription = "Generic Chicken Patty"
                }
            };

            foreach(Product pro in products)
            {
                context.Products.Add(pro);
            }
            context.SaveChanges();

            var UnitsOfMeasure = new UnitOfMeasure[]
            {
                new UnitOfMeasure{
                    UnitOfMeasureDescription = "Each"
                }
            };

            foreach(UnitOfMeasure unitOfMeasure in UnitsOfMeasure)
            {
                context.UnitsOfMeasure.Add(unitOfMeasure);
            }
            context.SaveChanges();

            var finishedProducts = new FinishedProduct[]
{
                new FinishedProduct
                {
                    FinishedProductName = "Hamburger",
                    FinishedProductDescription = "Hamburger with Cheese on Bun",
                    CategoryId = 1

                },

                new FinishedProduct
                {
                    FinishedProductName = "Fish Sandwich",
                    FinishedProductDescription = "Fish with Cheese on Bun",
                    CategoryId = 3
                },

                new FinishedProduct
                {
                    FinishedProductName = "Chicken Sandwich",
                    FinishedProductDescription = "Chicken with Cheese on Bun",
                    CategoryId = 2

                }
};

            foreach (FinishedProduct fp in finishedProducts)
            {
                context.FinishedProducts.Add(fp);
            }
            context.SaveChanges();

            var componentProducts = new ComponentProduct[]
            {
                new ComponentProduct{
                    ProductId = 1,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 1
                },
                new ComponentProduct{
                    ProductId = 2,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 1
                },
                new ComponentProduct{
                    ProductId = 3,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 1
                    
                },
                new ComponentProduct{
                    ProductId = 1,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 3

                },
                new ComponentProduct{
                    ProductId = 4,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 3
                },
                new ComponentProduct{
                    ProductId = 3,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 3

                },
                new ComponentProduct{
                    ProductId = 1,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 2
                },
                new ComponentProduct{
                    ProductId = 5,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 2
                },
                new ComponentProduct{
                    ProductId = 3,
                    ComponentQuantity = 1,
                    UnitOfMeasureId = 1,
                    FinishedProductId = 2

                },
            };

            foreach (ComponentProduct componentProduct in componentProducts)
            {
                context.ComponentProducts.Add(componentProduct);
            }
            context.SaveChanges();



        }
    }
}
