using Management.Domain.Entities.Product;
using Management.Infrastructure.Repository;
using Management.Infrastructure.Services.Abstract;

namespace Management.Infrastructure.Services.Concrete
{
    public class ProductService:BaseService<Product>,IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}