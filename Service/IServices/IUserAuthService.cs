using Model;
using Model.Dto;
using Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IUserAuthService
    {
        TokenResponseDTO Auth(AuthRequestViewModel User);
        GenericResponse<UserRolDTO> GetUserRol();
        Task<List<ClientsDTO>> GetClients();
        ClientsDTO GetClient(int id);
        Task<bool> CreateClient(ClientsViewModel client);
    }
}
