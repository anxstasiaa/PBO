using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class KelasKuliah
    {
        public string KodeKelas { get; set; }
        public string KodeMK { get; set; }
        public string IDProdi { get; set; }
        public string IDSemester { get; set; }
        public string Ruangan { get; set; }
        public string NamaKelas { get; set; }
        public MataKuliah MataKuliah { get; set; }
        public int JumlahPeeserta { get; set; }
        public int KapasitasKelas { get; set; }

        //public KelasKuliah(MataKuliah mk, string kodeKelas, string namaKelas, string kodeSemester, int kapasitas)
        //{
        //    KodeKelas = kodeKelas;
        //    NamaKelas = namaKelas;
        //    MataKuliah = mk;
        //    KodeMK = mk.KodeMK;
        //    this.kodeSemester = kodeSemester;
        //    KodeProdi = mk.KodeProdi;
        //    KapasitasKelas = kapasitas;
        //    JumlahPeserta = 0;
        //}

        //public bool TambahPeserta()
        //{
        //    if (JumlahPeserta < KapasitasKelas)
        //    {
        //        JumlahPeserta++;
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Kelas {NamaKelas} sudah penuh!");
        //        return false;
        //    }
        //}

        //public void TampilkanInfo()
        //{
        //    Console.WriteLine(
        //        $"Kelas Kuliah: {NamaKelas} | " +
        //        $"Kode Kelas: {KodeKelas} | " +
        //        $"Kode MK: {KodeMK} ({MataKuliah.NamaMK}) | " +
        //        $"Semester: {kodeSemester} | " +
        //        $"KodeProdi: {KodeProdi} | " +
        //        $"Peserta: {JumlahPeserta}/{KapasitasKelas}"
        //    );
        //}

    }
}
