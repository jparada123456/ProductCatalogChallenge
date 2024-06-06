using ProductCatalogChallenge.Domain.Entities;

namespace ProductCatalogChallenge.Infraestructure.Seeds
{
    public static class DataBaseSeeder
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {

            if (!context.ProductStatuses.Any())
            {
                context.ProductStatuses.AddRange(
                    new ProductStatus { StatusId = 1, StatusName = "Active" },
                    new ProductStatus { StatusId = 2, StatusName = "Inactive" }
                );
                context.SaveChanges();
            }
            else
            {
                var existingStatus1 = context.ProductStatuses.Find(1);
                if (existingStatus1 == null)
                {
                    context.ProductStatuses.Add(new ProductStatus { StatusId = 1, StatusName = "Active" });
                }

                var existingStatus2 = context.ProductStatuses.Find(2);
                if (existingStatus2 == null)
                {
                    context.ProductStatuses.Add(new ProductStatus { StatusId = 2, StatusName = "Inactive" });
                }

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", Price = 100, StatusId = 1 },
                    new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", Price = 200, StatusId = 2 }
                );
                context.SaveChanges();
            }

            if (!context.Discounts.Any())
            {
                context.Discounts.AddRange(
                    new Discount { DiscountId = 1, DiscountValue = 10 },
                    new Discount { DiscountId = 2, DiscountValue = 20 }
                );
                context.SaveChanges();
            }

            if (!context.FinalPrices.Any())
            {
                context.FinalPrices.AddRange(
                    new FinalPrice { FinalPriceId = 1, ProductId = 1, DiscountId = 1, FinalPriceValue = 90 },
                    new FinalPrice { FinalPriceId = 2, ProductId = 2, DiscountId = 2, FinalPriceValue = 180 }
                );
                context.SaveChanges();
            }

            if (!context.Inventories.Any())
            {
                context.Inventories.AddRange(
                    new Inventory { InventoryId = 1, ProductId = 1, Stock = 10 },
                    new Inventory { InventoryId = 2, ProductId = 2, Stock = 5 }
                );
                context.SaveChanges();
            }
        }
    }
}
