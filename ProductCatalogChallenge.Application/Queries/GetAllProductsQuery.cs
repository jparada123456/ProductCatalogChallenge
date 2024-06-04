using MediatR;
using ProductCatalogChallenge.Domain.Entities;

namespace ProductCatalogChallenge.Application.Queries
{
    public  class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
