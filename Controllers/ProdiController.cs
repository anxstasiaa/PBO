using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class ProdiController
    {
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();

        public ProdiController(List<Mahasiswa> mhs, List<Prodi> prodi, List<MataKuliah> mk)
        {
            daftarProdi = prodi;
            daftarMahasiswa = mhs;
            daftarMataKuliah = mk;
        }
    }
}
