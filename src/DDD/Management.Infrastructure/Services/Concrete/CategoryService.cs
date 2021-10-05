using System.Threading.Tasks;
using Management.Domain.Entities.Product;
using Management.Infrastructure.Repository;
using Management.Infrastructure.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Services.Concrete
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckCatagoryByNameAsync(string name)
        {
            return await _repository.Table.AnyAsync(x => x.Name == name);
        }
    }
}