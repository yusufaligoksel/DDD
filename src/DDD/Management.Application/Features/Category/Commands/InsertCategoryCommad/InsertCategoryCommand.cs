using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Response;
using Management.Domain.Dto;
using Management.Infrastructure.Services.Abstract;
using MediatR;

namespace Management.Application.Features.Category.Commands.InsertCategoryCommad
{
    public class InsertCategoryCommand : IRequest<GenericResult<CategoryDto>>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, GenericResult<CategoryDto>>
        {
            private readonly ICategoryService _categoryService;

            public InsertCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<GenericResult<CategoryDto>> Handle(InsertCategoryCommand request,
                CancellationToken cancellationToken)
            {
                bool checkCategory = await _categoryService.CheckCatagoryByNameAsync(request.Name);
                if (checkCategory)
                    return GenericResult<CategoryDto>.ErrorResponse(
                        new ErrorResult(
                            "Eklemeye çalıtığınız kategori daha önceden ekli olduğu için tekrar ekleyemezsiniz."),
                        (int)HttpStatusCode.BadRequest);

                var insertedCategory = await _categoryService.InsertAsync(new Domain.Entities.Product.Category
                {
                    Name = request.Name,
                    IsActive = request.IsActive,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now
                });
                var response = new CategoryDto { Name = insertedCategory.Name, Id = insertedCategory.Id };
                return GenericResult<CategoryDto>.SuccessResponse(response, 200);
            }
        }
    }
}