using MediatR;
using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Commands
{
    public class UpdateProductCommandHandler :  ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IWriteRepository<Product> _productRepository;
        private readonly IReadRepository<Product> _productReadRepository;

        public UpdateProductCommandHandler(IWriteRepository<Product> productRepository, IReadRepository<Product> productReadRepository)
        {
            _productRepository = productRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<bool> HandleAsync(UpdateProductCommand command)
        {
            // Retrieve the existing product by its ID
            var product = await _productReadRepository.GetByIdAsync(command.ProductId);

            if (product == null)
            {
                // Handle the case where the product does not exist
                // For example, throw an exception or return false
                throw new KeyNotFoundException($"Product with ID {command.ProductId} not found.");
            }

            // Update the product properties
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.StatusId = command.StatusId;
            product.UpdatedAt = DateTime.UtcNow;

            // Save the updated product
            await _productRepository.UpdateAsync(product);

            return true;
        }
    }
}
