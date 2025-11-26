using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminProdiController
    {
        static List<AdminProdi> daftarAdminProdi = new List<AdminProdi>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        public AdminProdiController(List<AdminProdi> admp, List<Prodi> prodi) 
        {
            daftarAdminProdi = admp;
            daftarProdi = prodi;
        }
    }
}
