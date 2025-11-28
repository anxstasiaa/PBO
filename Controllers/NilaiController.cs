using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class NilaiController
    {
        static List<Nilai> NilaiSaya = new List<Nilai>();
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();

        public NilaiController(List<Nilai> n, List<Mahasiswa> mhs, List<KelasKuliah> kk, List<MataKuliah> mk)
        {
            NilaiSaya = n;
            daftarMahasiswa = mhs;
            daftarKelasKuliah = kk;
            daftarMataKuliah = mk;
        }
    }
}
