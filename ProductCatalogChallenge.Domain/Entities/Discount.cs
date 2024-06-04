using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalogChallenge.Domain.Entities
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
