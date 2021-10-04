using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities.Customer
{
    public class UserAddress : BaseEntity
    {
        public int UserId { get; set; }
        public byte AddressTypeId { get; set; }
        public string Title { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
    }
}
