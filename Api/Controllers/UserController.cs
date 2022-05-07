using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Dto;
using Model.ViewModel;
using Service.IServices;
using System;

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
    }
}
