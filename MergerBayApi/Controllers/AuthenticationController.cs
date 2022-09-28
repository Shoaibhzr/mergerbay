using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MergerBay.Domain.Entities.Login;
using MergerBay.Domain.Model;
using MergerBay.Infrastructure.Interfaces.Login;
using MergerBay.Infrastructure.Interfaces.Token;
using Microsoft.AspNetCore.Mvc;

namespace MergerBayApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IToken _tokenService;
        private readonly ILogin _login;
        public AuthenticationController(IToken tokenService, ILogin login)
        {
            _tokenService = tokenService;
            _login = login;
        }
        [HttpPost("login")]
        public async Task<UserLoginReponse> User_Authentication([FromBody] UserSignIn model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _login.UserAuthentication(model);
                    if (response != null)
                    {
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sid, Guid.NewGuid().ToString()),
                        };
                        var token = _tokenService.GenerateAccessToken(claims);
                        response.User_Token = token;
                        return response;
                    }
                    else
                    {
                        return response;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> RefreshToken(string authenticationToken, string refreshToken)
        //{
        //    var principal = _tokenService.GetPrincipalFromExpiredToken(authenticationToken);
        //    var username = principal.Identity.Name; //this is mapped to the Name claim by default

        //    var user = _context.UserRefreshTokens.SingleOrDefault(u => u.Username == username);
        //    if (user == null || user.RefreshToken != refreshToken) return BadRequest();

        //    var newJwtToken = _tokenService.GenerateAccessToken(principal.Claims);
        //    var newRefreshToken = _tokenService.GenerateRefreshToken();

        //    user.RefreshToken = newRefreshToken;
        //    await _context.SaveChangesAsync();

        //    return new ObjectResult(new
        //    {
        //        authenticationToken = newJwtToken,
        //        refreshToken = newRefreshToken
        //    });
        //}
    }
}
