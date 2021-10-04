using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Domain.Entities.Customer;

namespace Management.Domain.Entities.Product
{
    public class Category:BaseEntity
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
