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
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_Returns_Product_IfExists()
        {
            // Arrange
            var productId = 1;
            var expectedProduct = new Product { ProductId = productId, Name = "Test Product", Description = "Test Description", Price = 10.99m };

            var mockRepository = new Mock<IReadRepository<Product>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            var handler = new GetProductByIdQueryHandler(mockRepository.Object);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.Equal(expectedProduct, result);
        }

        [Fact]
        public async Task HandleAsync_Returns_Null_IfProduct_NotFound()
        {
            // Arrange
            var productId = 1;

            var mockRepository = new Mock<IReadRepository<Product>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var handler = new GetProductByIdQueryHandler(mockRepository.Object);
            var query = new GetProductByIdQuery(productId);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            Assert.Null(result);
        }
    }
}
