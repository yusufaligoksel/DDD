using System.Threading.Tasks;
using Management.Domain.Entities.Product;

namespace Management.Infrastructure.Services.Abstract
{
    public interface ICategoryService:IBaseService<Category>
    {
        Task<bool> CheckCatagoryByNameAsync(string name);
    }
}