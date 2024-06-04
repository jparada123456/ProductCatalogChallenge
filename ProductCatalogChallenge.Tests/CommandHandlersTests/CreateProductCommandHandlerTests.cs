using Moq;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Tests.CommandHandlersTests
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldAddProduct()
        {
            // Arrange
            var mockRepo = new Mock<IWriteRepository<Product>>();
            var handler = new CreateProductCommandHandler(mockRepo.Object);
            var command = new CreateProductCommand
            {
                Name = "Headphones",
                Description = "Sony headphones for game players",
                Price = 149.99m,
                
            };

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
