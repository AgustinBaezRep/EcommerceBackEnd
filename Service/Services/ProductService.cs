using Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dto;
using Service.CommonServices;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly EcommerceContext _context;

        public ProductService(EcommerceContext _context)
        {
            this._context = _context;
        }

        public async Task<GenericResponse<List<ProductDTO>>> GetSixRandomProducts()
        {
            var oResponse = new GenericResponse<List<ProductDTO>>();
            try
            {
                var random = new Random();
                oResponse.Data = new List<ProductDTO>();
                oResponse.Data = await _context.Product.Select(x => new ProductDTO() { Description = x.Description}).Skip(random.Next(5,10)).Take(6).ToListAsync();

                return oResponse;
            }
            catch (Exception e)
            {
                return CommonMethods<List<ProductDTO>>.ErrorManager(e);
            }
        }
    }
}
