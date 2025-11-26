using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class KelasKuliahController
    {
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Semester> daftarSemester = new List<Semester>();

        public KelasKuliahController(List<KelasKuliah> kk, List<MataKuliah> mk, List<Prodi> prodi, List<Semester> s)
        {
            daftarKelasKuliah = kk;
            daftarMataKuliah = mk;
            daftarProdi = prodi;
            daftarSemester = s;

        }
    }
}
