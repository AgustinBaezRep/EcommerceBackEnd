using Model;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CommonServices
{
    public class CommonMethods<T>
    {
        public static GenericResponse<T> ErrorManager(Exception e)
        {
            return new GenericResponse<T>()
            {
                Data = default(T),
                Messagge = e.Message.Contains("See the inner exception for details") ? string.Format("Ocurrio un error: {0}", e.InnerException.ToString()) : string.Format("Ocurrio un error: {0}", e.Message),
                Success = false
            };
        }
    }
}
