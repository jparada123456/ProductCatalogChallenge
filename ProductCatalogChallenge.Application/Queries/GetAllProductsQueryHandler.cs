﻿using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Queries
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IReadRepository<Product> _productRepository;

        public GetAllProductsQueryHandler(IReadRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> HandleAsync(GetAllProductsQuery query)
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> HandleAsync(GetProductByIdQuery query)
        {
            return await _productRepository.GetByIdAsync(query.ProductId);
        }
    }
}
