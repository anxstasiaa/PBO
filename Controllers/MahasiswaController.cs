using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class MahasiswaController
    {
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<Prodi> daftarProdi = new List<Prodi>();

        static string[] prodiValid = { "ILMU KOMPUTER", "BIOLOGI", "FISIKA" };

        public MahasiswaController(List<Mahasiswa> mhs, List<Prodi> prodi)
        {
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
        }
    }
}
