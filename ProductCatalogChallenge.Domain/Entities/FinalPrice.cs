using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Domain.Entities
{
    public  class FinalPrice
    {
        [Key]
        public int FinalPriceId { get; set; }
        public int ProductId { get; set; }
        public int DiscountId { get; set; }
        public decimal FinalPriceValue { get; set; }
    }
}
