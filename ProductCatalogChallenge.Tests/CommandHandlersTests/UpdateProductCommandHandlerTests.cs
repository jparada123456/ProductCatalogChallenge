using Moq;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Enums;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Tests.CommandHandlersTests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IWriteRepository<Product>> _mockWriteRepository;
        private readonly Mock<IReadRepository<Product>> _mockReadRepository;
        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _mockWriteRepository = new Mock<IWriteRepository<Product>>();
            _mockReadRepository = new Mock<IReadRepository<Product>>();
            _handler = new UpdateProductCommandHandler(_mockWriteRepository.Object, _mockReadRepository.Object);
        }

        [Fact]
        public async Task HandleAsync_ProductExists_ShouldUpdateProduct()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductId = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 99.99m,
                StatusId = (int)ProductCatalogStatus.Active,
                UpdatedAt = DateTime.UtcNow
            };

            var existingProduct = new Product
            {
                ProductId = 1,
                Name = "Original Product",
                Description = "Original Description",
                Price = 50.00m,
                StatusId = (int)ProductCatalogStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            _mockReadRepository.Setup(repo => repo.GetByIdAsync(command.ProductId)).ReturnsAsync(existingProduct);
            _mockWriteRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.True(result);
            _mockWriteRepository.Verify(repo => repo.UpdateAsync(It.Is<Product>(p =>
                p.ProductId == command.ProductId &&
                p.Name == command.Name &&
                p.Description == command.Description &&
                p.Price == command.Price &&
                p.StatusId == command.StatusId
            )), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ProductDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductId = 1,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 99.99m,
                StatusId = 1
            };

            _mockReadRepository.Setup(repo => repo.GetByIdAsync(command.ProductId)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.HandleAsync(command));
            _mockWriteRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
