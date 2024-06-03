using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Domain.Entities
{
    public  class Inventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int Stock {  get; set; }
        public Product Product { get; set; }

    }
}
