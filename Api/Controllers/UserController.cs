using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Dto;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCors")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserAuthService _userAuthService;

        public UserController(IUserAuthService _userAuthService, ILogger<UserController> _logger)
        {
            this._userAuthService = _userAuthService;
            this._logger = _logger;
        }

        [HttpPost("Login")]
        public ActionResult<TokenResponseDTO> AuthenticateUser([FromBody] AuthRequestViewModel User)
        {
            TokenResponseDTO oResponse = new TokenResponseDTO();
            try
            {
                oResponse = _userAuthService.Auth(User);
                if (oResponse.Data == null) return NotFound(oResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpGet("GetUserRol")]
        [Authorize]
        public ActionResult<GenericResponse<UserRolDTO>> GetUserRol()
        {
            var oResponse = new GenericResponse<UserRolDTO>();
            try
            {
                oResponse = _userAuthService.GetUserRol();
                if (oResponse.Data == null) return NotFound(oResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpGet("GetClients")]
        [Authorize]
        public async Task<ActionResult<List<ClientsDTO>>> GetClients()
        {
            List<ClientsDTO> oResponse = new List<ClientsDTO>();
            try
            {
                oResponse = await _userAuthService.GetClients();
                if (oResponse.Count == 0) return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpGet("GetClient")]
        [Authorize]
        public ActionResult<ClientsDTO> GetClient([FromQuery] int id)
        {
            ClientsDTO oResponse = new ClientsDTO();
            try
            {
                oResponse = _userAuthService.GetClient(id);
                if (oResponse == null) return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpPost("PostClient")] // no se necesita autenticacion para crear un usuario
        public async Task<ActionResult> CreateClient([FromBody] ClientsViewModel client)
        {
            try
            {
                bool Ok = await _userAuthService.CreateClient(client);
                if (!Ok) return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok();
        }
    }
}
