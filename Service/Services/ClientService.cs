using Data;
using Model.Dto;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ClientService : IClientService
    {
        private readonly EcommerceContext _context;

        public ClientService(EcommerceContext _context)
        {
            this._context = _context;
        }

        public bool CreateClient(ClientsViewModel client)
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
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
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

        public List<ClientsDTO> GetClients()
        {
            List<ClientsDTO> oResponse = new List<ClientsDTO>();
            try
            {
                oResponse.AddRange(_context.Users.Select(x => new ClientsDTO()
                {
                    Name = x.Name,
                    Surname = x.Surname,
                    DateBirth = x.DateBirth,
                    Dni = x.Dni,
                    Phone = x.Phone,
                    Email = x.Email
                }).ToList());
            }
            catch (Exception)
            {
                return oResponse;
            }

            return oResponse;
        }
    }
}
