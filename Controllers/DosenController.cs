using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class DosenController
    {
        static List<Dosen> daftarDosen = new List<Dosen>();
        static List<Prodi> daftarProdi = new List<Prodi>();

        public DosenController(List<Dosen> dd, List<Prodi> prodi)
        {
            daftarDosen = dd;
            daftarProdi = prodi;
        }
    }
}
