using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Application.Commands
{
    public class UpdateProductCommand :IRequest<bool>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StatusId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
