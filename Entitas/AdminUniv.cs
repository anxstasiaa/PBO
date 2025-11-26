using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminUniv
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public void InputAdminUniv()
        {
            Console.WriteLine($"Admin Universitas dengan Username {Username}, Role {Role}");
        }
    }
}
