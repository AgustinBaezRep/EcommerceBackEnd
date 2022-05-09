using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.Common;
using Model.Dto;
using Model.Enum;
using Model.ViewModel;
using Service.CommonServices;
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

        public async Task<GenericResponse<UserWithTokenDTO>> AuthenticateUser(AuthRequestViewModel User)
        {
            var oResponse = new GenericResponse<UserWithTokenDTO>();
            try
            {
                Users user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Email && x.Password == User.Password);
                if (user == null)
                {
                    oResponse.Data = null;
                    oResponse.Messagge = "No se encontro ningun usuario";

                    return oResponse;
                }

                oResponse.Data = new UserWithTokenDTO() { Email = User.Email, Token = this.GetToken(user) };
            }
            catch (Exception e)
            {
                return CommonMethods<UserWithTokenDTO>.ErrorManager(e);
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

        public async Task<GenericResponse<UserRolDTO>> GetUserRol()
        {
            var oResponse = new GenericResponse<UserRolDTO>();
            try
            {
                oResponse.Data = new UserRolDTO()
                {
                    Clients = await _context.Users.Join(_context.Rol, u => u.IdRol, r => r.Id, (u, r) => new { User = u, Rol = r })
                                    .Where(x => x.Rol.Id.Equals((int)Roles.Customer))
                                    .Select(x => new User() { Name = x.User.Name, Rol = x.Rol.Description })
                                    .ToListAsync(),
                    Admins = await _context.Users.Join(_context.Rol, u => u.IdRol, r => r.Id, (u, r) => new { User = u, Rol = r })
                                    .Where(x => x.Rol.Id.Equals((int)Roles.Admin))
                                    .Select(x => new User() { Name = x.User.Name, Rol = x.Rol.Description })
                                    .ToListAsync()
                };

                if (oResponse.Data.Admins.Count == 0 && oResponse.Data.Clients.Count == 0)
                {
                    oResponse.Data = null;
                    oResponse.Messagge = "No se encontraron usarios con roles definidos";
                }

                return oResponse;
            }
            catch (Exception e)
            {
                return CommonMethods<UserRolDTO>.ErrorManager(e);
            }
        }

        public async Task<GenericResponse<ClientsDTO>> GetClientById(int id)
        {
            var oResponse = new GenericResponse<ClientsDTO>();
            try
            {
                oResponse.Data = new ClientsDTO();
                ClientsDTO client = await _context.Users.Where(x => x.Id == id)
                                    .Select(x => new ClientsDTO()
                                    {
                                        Name = x.Name,
                                        Surname = x.Surname,
                                        DateBirth = x.DateBirth,
                                        Dni = x.Dni,
                                        Phone = x.Phone,
                                        Email = x.Email
                                    }).FirstOrDefaultAsync();

                oResponse.Data = client ?? null;
            }
            catch (Exception e)
            {
                return CommonMethods<ClientsDTO>.ErrorManager(e);
            }

            return oResponse;
        }

        public async Task<GenericResponse<List<ClientsDTO>>> GetClients()
        {
            var oResponse = new GenericResponse<List<ClientsDTO>>();
            try
            {
                oResponse.Data = new List<ClientsDTO>();
                oResponse.Data = await _context.Users.Select(x => new ClientsDTO()
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    DateBirth = x.DateBirth,
                    Dni = x.Dni,
                    Phone = x.Phone,
                    Email = x.Email
                }).ToListAsync();
            }
            catch (Exception e)
            {
                return CommonMethods<List<ClientsDTO>>.ErrorManager(e);
            }

            return oResponse;
        }

        public async Task<GenericResponse<bool>> CreateClient(ClientsViewModel client)
        {
            var oResponse = new GenericResponse<bool>();
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
                oResponse.Data = true;
                oResponse.Messagge = "Usuario creado de manera exitosa";
                return oResponse;
            }
            catch (Exception e)
            {
                return CommonMethods<bool>.ErrorManager(e);
            }
        }
    }
}
