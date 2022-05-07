using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Dto;
using Model.ViewModel;
using Service.IServices;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyCors")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;

        public ClientController(ILogger<ClientController> _logger, IClientService _clientService)
        {
            this._logger = _logger;
            this._clientService = _clientService;
        }

        [HttpGet("GetClients")]
        public ActionResult<List<ClientsDTO>> GetClients()
        {
            List<ClientsDTO> oResponse = new List<ClientsDTO>();
            try
            {
                oResponse = _clientService.GetClients();
                if (oResponse.Count == 0) return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpGet("GetClient")]
        public ActionResult<ClientsDTO> GetClient([FromQuery] int id)
        {
            ClientsDTO oResponse = new ClientsDTO();
            try
            {
                oResponse = _clientService.GetClient(id);
                if (oResponse == null) return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return Ok(oResponse);
        }

        [HttpPost("PostClient")]
        public ActionResult CreateClient([FromBody] ClientsViewModel client)
        {
            try
            {
                bool Ok = _clientService.CreateClient(client);
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
