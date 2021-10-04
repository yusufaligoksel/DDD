using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities.Customer
{
    public class UserInvoicePaymentInstallment : BaseEntity
    {
        public int UserInvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentTotal { get; set; }
        public bool IsPaid { get; set; }
        public byte PaymentMethodId { get; set; }
        [ForeignKey("UserInvoiceId")]
        public UserInvoice UserInvoice { get; set; }
    }
}
