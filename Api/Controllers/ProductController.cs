using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(IProductService _productService, ILogger<ProductController> _logger)
        {
            this._productService = _productService;
            this._logger = _logger;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<GenericResponse<ProductDTO>>> GetProducts()
        {
            GenericResponse<List<ProductDTO>> oResponse = new GenericResponse<List<ProductDTO>>();
            try
            {
                oResponse = await _productService.GetProducts();
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
