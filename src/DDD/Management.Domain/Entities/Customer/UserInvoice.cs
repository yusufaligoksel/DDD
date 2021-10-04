using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities.Customer
{
    public class UserInvoice : BaseEntity
    {
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public int UserId { get; set; }
        public string InvoiceNo { get; set; }
        public decimal PaymentTotal { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey("BillingAddressId")]
        public UserAddress BillingAddress { get; set; }

        [ForeignKey("ShippingAddressId")]
        public UserAddress ShippingAddress { get; set; }
    }
}
