using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Enums;
using ProductCatalogChallenge.Domain.Interfaces;

namespace ProductCatalogChallenge.Application.Commands
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, int>
    {
        private readonly IWriteRepository<Product> _productRepository;

        public CreateProductCommandHandler(IWriteRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> HandleAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                StatusId = (int)ProductCatalogStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            await _productRepository.AddAsync(product);

            return product.ProductId;
        }
    }
}
