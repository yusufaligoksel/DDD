using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Entities.Customer;
using Management.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Management.Persistance.Context
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options)
            : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<UserInvoice> UserInvoice { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserInvoicePaymentInstallment> UserInvoicePaymentInstallment { get; set; }
        public DbSet<InvoiceProduct> InvoiceProduct { get; set; }
    }
}
