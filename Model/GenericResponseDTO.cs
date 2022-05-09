using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GenericResponse<T>
    {
        private bool _success = true;

        public T Data { get; set; }
        public bool Success { get { return _success; } set { _success = value; } }
        public string Messagge { get; set; }
    }
}
