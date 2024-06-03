using MediatR;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IWriteRepository<Product> _productRepository;

        public CreateProductCommandHandler(IWriteRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StatusId = 1,
                CreatedAt = DateTime.Now,
            };

            await _productRepository.AddAsync(product);

            return product.ProductId;
        }
    }
}
