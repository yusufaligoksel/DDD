using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Identity.Domain.Dto;
using Identity.Domain.Entities;
using Identity.Domain.Request;
using Identity.Domain.Response;
using Identity.Infrastructure.Services.Abstract;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserRefreshTokenService _userRefreshTokenService;
        public AuthService(IOptions<List<Client>> clientOptions,
            ITokenService tokenService,
            IUserService userService,
            IUserRoleService userRoleService,
            IUserRefreshTokenService userRefreshTokenService)
        {
            _clients = clientOptions.Value;
            _tokenService = tokenService;
            _userService = userService;
            _userRoleService = userRoleService;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<GenericResult<UserTokenDto>> CreateUserTokenAsync(LoginRequest request)
        {
            var user = await _userService.FindUserByEmailAsync(request.Email);
            if (user == null)
            {
                ErrorResult error = new("Girilen adrese ait bir hesap bulunamadı.");
                return GenericResult<UserTokenDto>.ErrorResponse(error, statusCode: 400);
            }

            if (!_userService.CheckPassword(user, request.Password))
            {
                ErrorResult error = new("Email veya şifre hatalıdır.");
                return GenericResult<UserTokenDto>.ErrorResponse(error, statusCode: 400);
            }
            var roles = await _userRoleService.GetRolesByUserId(user.Id);

            var token = _tokenService.CreateUserToken(user, roles);

            var userRefreshToken = await _userRefreshTokenService.FindAsync(x => x.RefreshToken == token.RefreshToken);
            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.InsertAsync(new UserRefreshToken
                { UserId = user.Id, RefreshToken = token.RefreshToken,Expiration = token.RefreshTokenExpiration});
            }
            else
            {
                userRefreshToken.RefreshToken = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
                await _userRefreshTokenService.UpdateAsync(userRefreshToken);
            }

            return GenericResult<UserTokenDto>.SuccessResponse(token, (int)HttpStatusCode.OK);
        }

        public async Task<GenericResult<UserTokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var userRefreshToken = await _userRefreshTokenService.FindAsync(x => x.RefreshToken == refreshToken && x.Expiration > DateTime.Now);

            if (userRefreshToken == null)
                return GenericResult<UserTokenDto>.ErrorResponse(new ErrorResult("RefreshToken süresi doldu."), (int)HttpStatusCode.BadRequest);

            var user = await _userService.FindAsync(userRefreshToken.UserId);
            var roles = await _userRoleService.GetRolesByUserId(user.Id);
            var token = _tokenService.CreateUserToken(user, roles);

            //update
            userRefreshToken.RefreshToken = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
            await _userRefreshTokenService.UpdateAsync(userRefreshToken);

            return GenericResult<UserTokenDto>.SuccessResponse(token, (int)HttpStatusCode.OK);
        }

        public async Task<GenericResult<string>> RevokeRefreshToken(string refreshToken)
        {
            var userRefreshToken = await _userRefreshTokenService.FindAsync(x => x.RefreshToken == refreshToken);

            if (userRefreshToken == null)
                return GenericResult<string>.ErrorResponse(new ErrorResult("RefreshToken bulunamadı."), (int)HttpStatusCode.BadRequest);

            await _userRefreshTokenService.DeleteAsync(userRefreshToken);

            return GenericResult<string>.SuccessResponse("User refresh token deleted", 200);
        }

        public GenericResult<ClientTokenDto> CreateClientToken(ClientTokenRequest request)
        {
            var client = _clients.SingleOrDefault(x => x.Id == request.ClientId && x.Secret == request.ClientSecret);

            if (client == null)
                return GenericResult<ClientTokenDto>.ErrorResponse(new ErrorResult("Client bilgileri hatalı!"), (int)HttpStatusCode.BadRequest);

            var token = _tokenService.CreateClientToken(client);

            return GenericResult<ClientTokenDto>.SuccessResponse(token, (int)HttpStatusCode.OK);
        }
    }
}