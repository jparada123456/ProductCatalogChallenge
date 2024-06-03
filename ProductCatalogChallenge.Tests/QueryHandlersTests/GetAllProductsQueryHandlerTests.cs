using Moq;
using ProductCatalogChallenge.Application.Queries;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Tests.QueryHandlersTests
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnAllProducts()
        {
            // Arrange
            var mockRepo = new Mock<IReadRepository<Product>>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetSampleProducts());
            var handler = new GetAllProductsQueryHandler(mockRepo.Object);
            var query = new GetAllProductsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count());
        }

        private IEnumerable<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop", Description = "14 inch, 8GB RAM, 256GB SSD", Price = 799.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 2, Name = "Smartphone", Description = "6.5 inch display, 128GB storage", Price = 699.49m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 3, Name = "Headphones", Description = "Noise cancelling, over-ear", Price = 129.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 4, Name = "Smartwatch", Description = "Heart rate monitor, GPS", Price = 199.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 5, Name = "Tablet", Description = "10 inch display, 64GB storage", Price = 329.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 6, Name = "Camera", Description = "24MP, 4K video recording", Price = 499.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 7, Name = "Printer", Description = "Wireless, color printing", Price = 149.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 8, Name = "Keyboard", Description = "Mechanical, RGB lighting", Price = 89.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 9, Name = "Mouse", Description = "Wireless, ergonomic design", Price = 59.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null },
                new Product { ProductId = 10, Name = "Monitor", Description = "27 inch, 144Hz refresh rate", Price = 279.99m, StatusId = 1, CreatedAt = DateTime.Now, UpdatedAt = null }
            };
        }
    }
}
