using Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model.Common;
using Model.Dto;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly EcommerceContext _context;
        private readonly AppSettings _appSettings;

        public UserAuthService(EcommerceContext _context, IOptions<AppSettings> _appSettings)
        {
            this._context = _context;
            this._appSettings = _appSettings.Value;
        }

        public TokenResponseDTO Auth(AuthRequestViewModel User)
        {
            TokenResponseDTO oResponse = new TokenResponseDTO();
            try
            {
                Users user = _context.Users.FirstOrDefault(x => x.Email == User.Email && x.Password == User.Password);
                if (user == null)
                {
                    oResponse.Data = null;
                    oResponse.Messagge = "No se encontro ningun usuario";

                    return oResponse;
                }

                oResponse.Data = new UserWithTokenDTO()
                {
                    Email = User.Email,
                    Token = this.GetToken(user)
                };
            }
            catch (Exception e)
            {
                oResponse.Success = false;
                oResponse.Messagge = string.Format("Ocurrio el siguiente error {0}", e.Message);
            }

            return oResponse;
        }

        private string GetToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
