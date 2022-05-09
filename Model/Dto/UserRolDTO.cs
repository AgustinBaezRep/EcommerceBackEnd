using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class UserRolDTO
    {
        public UserRolDTO()
        {
            Admins = new List<User>();
            Clients = new List<User>();
        }

        public List<User> Admins { get; set; }
        public List<User> Clients { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string Rol { get; set; }
    }
}
