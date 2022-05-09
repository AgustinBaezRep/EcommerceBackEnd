using Model;
using Model.Dto;
using Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IUserAuthService
    {
        Task<GenericResponse<UserWithTokenDTO>> AuthenticateUser(AuthRequestViewModel User);
        Task<GenericResponse<UserRolDTO>> GetUserRol();
        Task<GenericResponse<List<ClientsDTO>>> GetClients();
        Task<GenericResponse<ClientsDTO>> GetClientById(int id);
        Task<GenericResponse<bool>> CreateClient(ClientsViewModel client);
    }
}
