using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project_PBO;

namespace Project_PBO
{
    internal class MahasiswaController
    {
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Nilai> daftarNilai = new List<Nilai>();
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<Tagihan> daftarTagihan = new List<Tagihan>();

        public MahasiswaController(List<Mahasiswa> mhs, List<Prodi> prodi, List<Nilai> nilai, List<KelasKuliah> kelasKuliah, List<MataKuliah> mataKuliah, List<Tagihan> tagihan)
        {
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
            daftarNilai = nilai;
            daftarKelasKuliah = kelasKuliah;
            daftarMataKuliah = mataKuliah;
            daftarTagihan = tagihan;
        }

        public void MenuKRS(string nim, string IDProdi)
        {
            Console.Clear();
            Console.WriteLine("===== KRS - Kartu Rencana Studi =====");

            // Cari semester aktif
            string semesterAktif = "20241"; // bisa kamu ubah pakai SemesterController nanti

            // Ambil semua kelas dari prodi mahasiswa
            var kelasProdi = daftarKelasKuliah
                .Where(k => k.IDProdi == IDProdi && k.IDSemester == semesterAktif)
                .ToList();

            if (kelasProdi.Count == 0)
            {
                Console.WriteLine("Tidak ada kelas ditawarkan untuk semester ini.");
                Console.ReadLine();
                return;
            }

            int no = 1;
            foreach (var k in kelasProdi)
            {
                var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == k.KodeMK);
                string namaMK = mk?.NamaMK ?? "(Nama MK tidak ditemukan)";
                int sks = mk?.SKS ?? 0;

                Console.WriteLine($"{no}. {k.KodeKelas} | {k.KodeMK} - {namaMK} ({sks} SKS)");
                Console.WriteLine($"   Dosen : {k.DosenPengampu ?? "-"}");
                Console.WriteLine($"   Kapasitas : {k.KapasitasKelas}, Terisi : {k.JumlahPeserta}");
                no++;
            }

            Console.Write("\nAmbil kelas nomor berapa? (0 untuk batal): ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 1 || pilih > kelasProdi.Count)
                return;

            var kelasDipilih = kelasProdi[pilih - 1];

            // VALIDASI KAPASITAS
            if (kelasDipilih.JumlahPeserta >= kelasDipilih.KapasitasKelas)
            {
                Console.WriteLine("\n Kelas penuh! Tidak bisa mengambil.");
                Console.ReadLine();
                return;
            }

            // CEK SUDAH AMBIL BELUM?
            if (daftarNilai.Any(n => n.NIM == nim && n.KodeKelas == kelasDipilih.KodeKelas))
            {
                Console.WriteLine("\n Kamu sudah mengambil kelas ini sebelumnya!");
                Console.ReadLine();
                return;
            }

            // AMBIL DATA MATA KULIAH
            var mataKuliah = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kelasDipilih.KodeMK);


            // BUAT RECORD NILAI BARU
            daftarNilai.Add(new Nilai
            {
                NIM = nim,
                KodeKelas = kelasDipilih.KodeKelas,
                KodeMK = kelasDipilih.KodeMK,                    
                IDProdi = kelasDipilih.IDProdi,                  
                IDSemester = kelasDipilih.IDSemester,            
                NamaMK = mataKuliah?.NamaMK ?? "",               
                SKS = mataKuliah?.SKS ?? 0,                      
                NilaiTugas = 0,                                   
                NilaiUTS = 0,                                     
                NilaiUAS = 0,                                     
                NilaiSoftSkill = 0,                               
                NilaiAkhir = 0,                                   
                AngkaMutu = null,
                HurufMutu = null
            });


            // UPDATE JUMLAH PESERTA
            kelasDipilih.JumlahPeserta++;

            Console.WriteLine("\n Kelas berhasil diambil!");
            Console.ReadLine();
        }

        public void MenuKHS(string nim, string IDProdi)
        {
            Console.Clear();
            Console.WriteLine("===== KHS - Kartu Hasil Studi =====");
            Console.Write("Masukkan semester (misal 20241): ");
            string sem = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(sem))
            {
                Console.WriteLine("Semester tidak valid!");
                Console.ReadLine();
                return;
            }

            var nilaiMhs = daftarNilai
                .Where(n => n.NIM == nim)
                .Where(n =>
                {
                    var kelas = daftarKelasKuliah.FirstOrDefault(k => k.KodeKelas == n.KodeKelas);
                    return kelas != null && kelas.IDSemester == sem;
                })
                .ToList();

            if (nilaiMhs.Count == 0)
            {
                Console.WriteLine("Tidak ada KHS untuk semester ini.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nMK  |  Nama Mata Kuliah          | SKS | NH | NA");
            Console.WriteLine("--------+-------------------------------+-----+----+------");

            double totalNilai = 0;
            int totalSKS = 0;

            foreach (var n in nilaiMhs)
            {
                var kelas = daftarKelasKuliah.FirstOrDefault(k => k.KodeKelas == n.KodeKelas);
                if (kelas == null) continue;

                var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kelas.KodeMK);
                if (mk == null) continue;

                string huruf = n.HurufMutu ?? "-";
                string angka = n.AngkaMutu.HasValue ? n.AngkaMutu.Value.ToString("0.00") : "-";

                Console.WriteLine($"{mk.KodeMK,-7} | {mk.NamaMK,-29} | {mk.SKS,3} | {huruf,-2} | {angka,4}");

                if (n.AngkaMutu.HasValue)
                {
                    totalNilai += n.AngkaMutu.Value * mk.SKS;
                    totalSKS += mk.SKS;
                }
            }

            if (totalSKS > 0)
            {
                double ipSemester = totalNilai / totalSKS;
                Console.WriteLine("--------+-------------------------------+-----+----+------");
                Console.WriteLine($"IP Semester: {ipSemester:0.00}");
            }

            Console.WriteLine("\nTekan Enter untuk kembali...");
            Console.ReadLine();
        }

        public void LihatTagihan(string nim)
        { 
            Console.Clear();
            Console.WriteLine("===== Tagihan Mahasiswa =====");

            var tagihanMhs = daftarTagihan
            .Where(t => t.NIM == nim)
            .OrderBy(t => t.IsLunas)
            .ToList();

            if (tagihanMhs.Count == 0)
            {
                Console.WriteLine("Tidak ada tagihan.");
                Console.WriteLine("\nTekan Enter untuk kembali...");
                Console.ReadLine();
                return;
            }
            
        }


    }
}
