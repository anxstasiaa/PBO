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

            Console.WriteLine("\nNo | ID Tagihan          | Semester | Jumlah        | Status       | Tgl Bayar    | Metode");
            Console.WriteLine("---+---------------------+----------+---------------+--------------+--------------+----------");

            int no = 1;
            foreach (var t in tagihanMhs)
            {
                string status = t.IsLunas ? "Lunas" : "Belum Lunas";
                string tanggalBayar = t.TanggalPembayaran.HasValue
                    ? t.TanggalPembayaran.Value.ToString("dd/MM/yyyy")
                    : "-";
                string metode = string.IsNullOrWhiteSpace(t.MetodePembayaran)
                    ? "-"
                    : t.MetodePembayaran;

                Console.WriteLine($"{no,2} | {t.IDTagihan,-19} | {t.IDSemester,-8} | Rp {t.TotalPembayaran,10:N0} | {status,-12} | {tanggalBayar,-12} | {metode}");
                no++;
            }
            Console.WriteLine($"\nTotal Tagihan: {tagihanMhs.Count}");

            var belumLunas = tagihanMhs.Where(t => !t.IsLunas).ToList();
            if (belumLunas.Any())
            {
                double totalBelumLunas = belumLunas.Sum(t => t.TotalPembayaran);
                Console.WriteLine($"Total Belum Lunas: Rp {totalBelumLunas:N0}");

                Console.Write("\nApakah Anda ingin membayar tagihan? (Y/N): ");
                string jawab = Console.ReadLine()?.Trim().ToUpper();

                if (jawab == "Y")
                {
                    BayarTagihan(nim);
                }
            }

            Console.WriteLine("\nTekan Enter untuk kembali...");
            Console.ReadLine();

        }

        public void BayarTagihan(string nim)
        {
            Console.Clear();
            Console.WriteLine("===== Bayar Tagihan =====");

            var tagihanBelumLunas = daftarTagihan
                .Where(t => t.NIM == nim && !t.IsLunas)
                .ToList();

            if (tagihanBelumLunas.Count == 0)
            {
                Console.WriteLine("Tidak ada tagihan yang perlu dibayar.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("\nTagihan yang perlu dibayar:");
            for (int i = 0; i < tagihanBelumLunas.Count; i++)
            {
                var t = tagihanBelumLunas[i];
                Console.WriteLine($"{i + 1}. {t.IDTagihan} | Semester: {t.IDSemester} | Jumlah: Rp {t.TotalPembayaran:N0}");
            }
            Console.Write("\nPilih nomor tagihan yang ingin dibayar (0 untuk batal): ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 0 || pilih > tagihanBelumLunas.Count)
                return;
            if (pilih == 0)
                return;

            var tagihanDipilih = tagihanBelumLunas[pilih - 1];

            Console.WriteLine($"\nDetail Tagihan:");
            Console.WriteLine($"ID Tagihan: {tagihanDipilih.IDTagihan}");
            Console.WriteLine($"Semester: {tagihanDipilih.IDSemester}");
            Console.WriteLine($"Total Pembayaran: Rp {tagihanDipilih.TotalPembayaran:N0}");

            Console.WriteLine("\nMetode Pembayaran:");
            Console.WriteLine("1. Transfer Bank");
            Console.WriteLine("2. Virtual Account");
            Console.WriteLine("3. E-Wallet");
            Console.WriteLine("4. Tunai");
            Console.Write("Pilih metode pembayaran: ");

            string metode = "";
            if (int.TryParse(Console.ReadLine(), out int metodePilih))
            {
                switch (metodePilih)
                {
                    case 1: metode = "Transfer Bank"; break;
                    case 2: metode = "Virtual Account"; break;
                    case 3: metode = "E-Wallet"; break;
                    case 4: metode = "Tunai"; break;
                    default: metode = "Lainnya"; break;
                }
            }
            Console.Write("\nKonfirmasi pembayaran? (Y/N): ");
            string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

            if (konfirmasi == "Y")
            {
                tagihanDipilih.IsLunas = true;
                tagihanDipilih.TanggalPembayaran = DateTime.Now;
                tagihanDipilih.MetodePembayaran = metode;

                Console.WriteLine("\n✓ Pembayaran berhasil!");
                Console.WriteLine($"Tagihan {tagihanDipilih.IDTagihan} telah dibayar.");
                Console.WriteLine($"Metode: {metode}");
                Console.WriteLine($"Tanggal: {tagihanDipilih.TanggalPembayaran:dd/MM/yyyy HH:mm}");
            }
            else
            {
                Console.WriteLine("\nPembayaran dibatalkan.");
            }

            Console.WriteLine("\nTekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }


    }
}
