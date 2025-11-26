using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminProdi
    {
        public string IDProdi { get; set; }
        public string NamaProdi { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public void InputProdi()
        {
            Console.WriteLine($"Program Studi {NamaProdi} dengan ID {IDProdi}, username {Username}, role {Role}");
        }
    }
}
