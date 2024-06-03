using MediatR;
using ProductCatalogChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Queries
{
    public  class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
