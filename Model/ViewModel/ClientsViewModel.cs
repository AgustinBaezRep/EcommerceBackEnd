using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ClientsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public long Dni { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdHome { get; set; }
        public int IdRol { get; set; }
    }
}
