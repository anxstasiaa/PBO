using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminUnivController
    {
        static List<AdminUniv> daftarAdminUniv = new List<AdminUniv>();
        public AdminUnivController(List<AdminUniv> admu)
        {
            daftarAdminUniv = admu;
        }
    }
}
