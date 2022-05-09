using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Common;
using Model.Dto;
using Model.Enum;
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
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public GenericResponse<UserRolDTO> GetUserRol()
        {
            var oResponse = new GenericResponse<UserRolDTO>();
            try
            {
                oResponse.Data = new UserRolDTO();
                oResponse.Data.Clients.AddRange(_context.Users.Where(x => x.IdRol.Equals((int)Roles.Cliente)).Select(x => new User() { Name = x.Name, Rol = x.IdRol.ToString() }).ToList());
                oResponse.Data.Admins.AddRange(_context.Users.Where(x => x.IdRol.Equals((int)Roles.Administrador)).Select(x => new User() { Name = x.Name, Rol = x.IdRol.ToString() }).ToList());

                if (oResponse.Data.Admins.Count == 0 && oResponse.Data.Clients.Count == 0)
                {
                    oResponse.Data = null;
                    oResponse.Messagge = "No se encontraron usarios con roles definidos";
                }

                return oResponse;
            }
            catch (Exception e)
            {
                oResponse.Data = null;
                oResponse.Messagge = string.Format("Ocurrio el siguiente error {0}", e.Message);
                oResponse.Success = false;
                return oResponse;
            }
        }

        public ClientsDTO GetClient(int id)
        {
            ClientsDTO oResponse = new ClientsDTO();
            try
            {
                ClientsDTO client = _context.Users.Where(x => x.Id == id)
                                    .Select(x => new ClientsDTO()
                                    {
                                        Name = x.Name,
                                        Surname = x.Surname,
                                        DateBirth = x.DateBirth,
                                        Dni = x.Dni,
                                        Phone = x.Phone,
                                        Email = x.Email
                                    }).FirstOrDefault();

                oResponse = client != null ? client : null;
            }
            catch (Exception)
            {
                return oResponse;
            }

            return oResponse;
        }

        public async Task<List<ClientsDTO>> GetClients()
        {
            List<ClientsDTO> oResponse = new List<ClientsDTO>();
            try
            {
                oResponse = await _context.Users.Select(x => new ClientsDTO()
                            {
                                Name = x.Name,
                                Surname = x.Surname,
                                DateBirth = x.DateBirth,
                                Dni = x.Dni,
                                Phone = x.Phone,
                                Email = x.Email
                            }).ToListAsync();
            }
            catch (Exception)
            {
                return oResponse;
            }

            return oResponse;
        }

        public async Task<bool> CreateClient(ClientsViewModel client)
        {
            try
            {
                _context.Users.Add(new Users()
                {
                    Name = client.Name,
                    Surname = client.Surname,
                    DateBirth = client.DateBirth,
                    Dni = client.Dni,
                    Phone = client.Phone,
                    Email = client.Email,
                    Password = client.Password,
                    IdHome = client.IdHome,
                    IdRol = client.IdRol
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
