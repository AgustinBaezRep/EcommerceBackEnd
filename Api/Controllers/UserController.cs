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
        public async Task<ActionResult<GenericResponse<UserWithTokenDTO>>> AuthenticateUser([FromBody] AuthRequestViewModel User)
        {
            var oResponse = new GenericResponse<UserWithTokenDTO>();
            try
            {
                oResponse = await _userAuthService.AuthenticateUser(User);
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
        public async Task<ActionResult<GenericResponse<UserRolDTO>>> GetUserRol()
        {
            var oResponse = new GenericResponse<UserRolDTO>();
            try
            {
                oResponse = await _userAuthService.GetUserRol();
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
        public async Task<ActionResult<GenericResponse<List<ClientsDTO>>>> GetClients()
        {
            var oResponse = new GenericResponse<List<ClientsDTO>>();
            try
            {
                oResponse = await _userAuthService.GetClients();
                if (oResponse.Data.Count == 0) return NotFound(oResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpGet("GetClient")]
        [Authorize]
        public async Task<ActionResult<GenericResponse<ClientsDTO>>> GetClientById([FromQuery] int id)
        {
            var oResponse = new GenericResponse<ClientsDTO>();
            try
            {
                oResponse = await _userAuthService.GetClientById(id);
                if (oResponse.Data == null) return NotFound(oResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpPost("PostClient")]
        public async Task<ActionResult<GenericResponse<bool>>> CreateClient([FromBody] ClientsViewModel client)
        {
            var oResponse = new GenericResponse<bool>();
            try
            {
                oResponse = await _userAuthService.CreateClient(client);
                if (!oResponse.Data) return BadRequest(oResponse);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }
    }
}
