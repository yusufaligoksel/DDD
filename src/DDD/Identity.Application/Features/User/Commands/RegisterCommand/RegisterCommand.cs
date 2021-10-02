using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Entities;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using MediatR;

namespace Identity.Application.Features.User.Commands.RegisterCommand
{
    public class RegisterCommand : IRequest<GenericResult<UserDto>>
    {
        public RegisterCommand()
        {
            this.Roles = new List<int>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public List<int> Roles { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, GenericResult<UserDto>>
        {
            private readonly IUserService _userService;
            private readonly IUserRoleService _userRoleService;
            private readonly IPasswordService _passwordService;

            public RegisterCommandHandler(IUserService userService,
                IUserRoleService userRoleService,
                IPasswordService passwordService)
            {
                _userService = userService;
                _userRoleService = userRoleService;
                _passwordService = passwordService;
            }

            public async Task<GenericResult<UserDto>> Handle(RegisterCommand request,
                CancellationToken cancellationToken)
            {
                if (await _userService.CheckUser(request.Email))
                    return GenericResult<UserDto>.ErrorResponse(
                        new ErrorResult("Bu email ile daha önceden kayıt olunduğu için kayıt olamazsınız."),
                        (int)HttpStatusCode.BadRequest);

                var entity = new Domain.Entities.User
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email,
                    CreatedDate = DateTime.Now,
                    Username = request.Username,
                    PasswordHash = _passwordService.HashPassword(request.Password)
                };

                var insertedUser = await _userService.InsertAsync(entity);

                if (request.Roles != null)
                    foreach (var item in request.Roles)
                        await _userRoleService.InsertAsync(new UserRole { UserId = insertedUser.Id, RoleId = item });


                var roles = await _userRoleService.GetRolesByUserId(insertedUser.Id);
                var response = new UserDto
                {
                    Id = insertedUser.Id,
                    FirstName = insertedUser.Firstname,
                    Lastname = insertedUser.Lastname,
                    Email = insertedUser.Email,
                    Username = insertedUser.Username,
                };

                foreach (var item in roles)
                    response.Roles.Add(item.Role.Name);

                return GenericResult<UserDto>.SuccessResponse(response, 200);
            }
        }
    }
}