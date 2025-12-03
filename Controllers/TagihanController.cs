using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PBO
{
    internal class TagihanController
    {
        static List<Mahasiswa> daftarMahasiswa;
        static List<Tagihan> daftarTagihan;
        static List<Semester> daftarSemester = new List<Semester>();

        public TagihanController(List<Mahasiswa> mhs, List<Tagihan> tag, List<Semester> sem)
        {
            daftarMahasiswa = mhs;
            daftarTagihan = tag;
            daftarSemester = sem;
        }

        // Menu untuk admin prodi mengelola tagihan
        public void MenuAdminTagihan()
        {
            string pilih;

            do
            {
                Console.Clear();
                Console.WriteLine("======= MENU ADMIN TAGIHAN  =======");
                Console.WriteLine("| 1. Buat Tagihan Mahasiswa       |");
                Console.WriteLine("| 2. Lihat Semua Tagihan          |");
                Console.WriteLine("| 3. Edit Tagihan                 |");
                Console.WriteLine("| 4. Hapus Tagihan                |");
                Console.WriteLine("| 0. Kembali                      |");
                Console.WriteLine("===================================");
                Console.Write("Pilih: ");
                pilih = Console.ReadLine();

                switch (pilih)
                {
                    case "1": BuatTagihan(); break;
                    case "2": LihatSemuaTagihan(); break;
                    case "3": EditTagihan(); break;
                    case "4": HapusTagihan(); break;
                }

            } while (pilih != "0");
        }

        private void BuatTagihan()
        {
            Console.Clear();
            Console.WriteLine("=== BUAT TAGIHAN BARU ===");

            Console.Write("Masukkan Semester (contoh 20241): ");
            string sem = Console.ReadLine();

            Console.Write("Masukkan Total Pembayaran: ");
            if (!double.TryParse(Console.ReadLine(), out double total))
            {
                Console.WriteLine("Input tidak valid");
                Console.ReadLine();
                return;
            }

            foreach (var mhs in daftarMahasiswa)
            {
                Tagihan t = new Tagihan
                {
                    IDTagihan = "TAG" + mhs.NIM + sem,
                    NIM = mhs.NIM,
                    IDSemester = sem,
                    TotalPembayaran = total,
                    PeriodePembayaran = DateTime.Now.ToString("MM-yyyy"),
                    IsLunas = false
                };

                daftarTagihan.Add(t);
            }

            Console.WriteLine("Tagihan berhasil dibuat.");
            Console.ReadLine();
        }

        private void LihatSemuaTagihan()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR TAGIHAN ===");

            if (daftarTagihan.Count == 0)
            {
                Console.WriteLine("Belum ada tagihan.");
                Console.ReadLine();
                return;
            }

            foreach (var t in daftarTagihan)
                t.TampilkanInfoTagihan();

            Console.ReadLine();
        }

        private void EditTagihan()
        {
            Console.Clear();
            Console.WriteLine("=== EDIT TAGIHAN ===");
            Console.Write("Masukkan ID Tagihan: ");
            string id = Console.ReadLine();

            var t = daftarTagihan.FirstOrDefault(x => x.IDTagihan == id);
            if (t == null)
            {
                Console.WriteLine("Tagihan tidak ditemukan.");
                Console.ReadLine();
                return;
            }

            t.TampilkanInfoTagihan();

            Console.Write("Masukkan Total Pembayaran baru: ");
            if (!double.TryParse(Console.ReadLine(), out double total))
            {
                Console.WriteLine("Input tidak valid");
                Console.ReadLine();
                return;
            }

            t.TotalPembayaran = total;
            Console.WriteLine("Tagihan berhasil diperbarui.");
            Console.ReadLine();
        }

        private void HapusTagihan()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS TAGIHAN ===");
            Console.Write("Masukkan ID Tagihan: ");
            string id = Console.ReadLine();

            var t = daftarTagihan.FirstOrDefault(x => x.IDTagihan == id);
            if (t == null)
            {
                Console.WriteLine("Tagihan tidak ditemukan.");
                Console.ReadLine();
                return;
            }

            daftarTagihan.Remove(t);
            Console.WriteLine("Tagihan berhasil dihapus.");
            Console.ReadLine();
        }

        public void MenuTagihanMahasiswa(string nim)
        {
            string pilih;

            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU TAGIHAN MAHASISWA ===");
                Console.WriteLine("1. Lihat Tagihan");
                Console.WriteLine("2. Bayar Tagihan");
                Console.WriteLine("3. Histori Pembayaran");
                Console.WriteLine("0. Kembali");
                Console.Write("Pilih: ");
                pilih = Console.ReadLine();

                switch (pilih)
                {
                    case "1": LihatTagihanMahasiswa(nim); break;
                    case "2": BayarTagihan(nim); break;
                    case "3": Histori(nim); break;
                }

            } while (pilih != "0");
        }

        private void LihatTagihanMahasiswa(string nim)
        {
            Console.Clear();
            var list = daftarTagihan.Where(t => t.NIM == nim).ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("Tidak ada tagihan.");
                Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            foreach (var t in list)
                t.TampilkanInfoTagihan();

            Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
            Console.ReadLine();
        }

        private void BayarTagihan(string nim)
        {
            Console.Clear();
            Console.WriteLine("=== BAYAR TAGIHAN ===");

            var list = daftarTagihan.Where(t => t.NIM == nim && !t.IsLunas).ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("Tidak ada tagihan yang harus dibayar.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}. {list[i].IDTagihan} - Rp {list[i].TotalPembayaran:N0}");

            Console.Write("Pilih tagihan: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > list.Count)
                return;

            var tag = list[idx - 1];

            Console.WriteLine($"Total: Rp {tag.TotalPembayaran:N0}");
            Console.Write("Metode Pembayaran: ");
            tag.MetodePembayaran = Console.ReadLine();

            tag.IsLunas = true;
            tag.TanggalPembayaran = DateTime.Now;

            Console.WriteLine("Pembayaran berhasil.");
            Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
            Console.ReadLine();
        }

        private void Histori(string nim)
        {
            Console.Clear();
            Console.WriteLine("=== HISTORI PEMBAYARAN ===");

            var list = daftarTagihan.Where(t => t.NIM == nim && t.IsLunas).ToList();

            if (list.Count == 0)
            {
                Console.WriteLine("Belum ada histori pembayaran.");
                Console.ReadLine();
                return;
            }

            foreach (var t in list)
                t.TampilkanInfoTagihan();

            Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
            Console.ReadLine();
        }
    }
}