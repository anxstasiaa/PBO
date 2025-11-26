using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string IDProdi { get; set; }
        public string NIM {  get; set; }

        public void InputUserAdm()
        {
            Console.WriteLine($"User dengan Username {Username}, Role {Role}, Prodi {IDProdi}");
        }

        public void InputUserMhs()
        {
            Console.WriteLine($"User dengan Username {Username}, Role {Role}, Prodi {IDProdi}, NIM {NIM}");
        }
    }
}
