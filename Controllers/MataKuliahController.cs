using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class MataKuliahController
    {
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<Prodi> daftarProdi = new List<Prodi>();

        public MataKuliahController(List<Prodi> prodi)
        {
            daftarProdi = prodi;
        }
    }
}
