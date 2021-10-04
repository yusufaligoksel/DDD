using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities.Customer
{
    public class InvoiceProduct : BaseEntity
    {
        public int UserInvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("UserInvoiceId")]
        public UserInvoice UserInvoice { get; set; }

        [ForeignKey("ProductId")]
        public Product.Product Product { get; set; }
    }
}
