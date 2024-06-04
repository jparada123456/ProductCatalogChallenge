using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Queries
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product>
    {
        private readonly IReadRepository<Product> _productRepository;

        public GetProductByIdQueryHandler(IReadRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> HandleAsync(GetProductByIdQuery query)
        {
            return await _productRepository.GetByIdAsync(query.ProductId);
        }
    }
}
