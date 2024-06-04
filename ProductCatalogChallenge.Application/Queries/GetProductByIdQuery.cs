using MediatR;
using ProductCatalogChallenge.Domain.Entities;

namespace ProductCatalogChallenge.Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; }

        public GetProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
