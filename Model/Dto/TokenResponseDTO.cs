using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class TokenResponseDTO
    {
        private bool _success;

        public UserWithTokenDTO Data { get; set; }
        public bool Success { get { return _success; } set { _success = true; } }
        public string Messagge { get; set; }
    }

    public class UserWithTokenDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
