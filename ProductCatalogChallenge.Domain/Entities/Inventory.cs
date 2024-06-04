using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Domain.Entities
{
    public  class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int Stock {  get; set; }
    }
}
