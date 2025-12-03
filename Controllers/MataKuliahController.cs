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

        public void DaftarMataKuliah()
        {
            Console.Clear();
            Console.WriteLine("\n=== Daftar Mata Kuliah ===");
            if (daftarMataKuliah.Count == 0)
            {
                Console.WriteLine("Belum ada mata kuliah terdaftar!");
            }
            else
            {
                int no = 1;
                foreach (var mk in daftarMataKuliah)
                {
                    Console.WriteLine($"{no}. {mk.NamaMK} || ({mk.KodeMK}) || {mk.SKS} SKS || Semester {mk.Semester} || Dosen Pengampu: {mk.DosenPengampu}");
                    no++;
                }
            }
        }

        public void TambahMataKuliah()
        {
            Console.WriteLine("\n=== Tambah Mata Kuliah ===");
            MataKuliah mk = new MataKuliah();

            // KodeMK (unique, non-empty)
            do
            {
                Console.Write("Masukkan Kode Mata Kuliah (contoh: MK101): ");
                mk.KodeMK = Console.ReadLine()?.ToUpper().Trim();
                if (string.IsNullOrWhiteSpace(mk.KodeMK))
                {
                    Console.WriteLine("Kode tidak boleh kosong!");
                    mk.KodeMK = null;
                }
                else if (daftarMataKuliah.Any(x => string.Equals(x.KodeMK, mk.KodeMK, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Kode sudah terdaftar. Gunakan kode lain.");
                    mk.KodeMK = null;
                }
            } while (string.IsNullOrWhiteSpace(mk.KodeMK));

            // NamaMK
            do
            {
                Console.Write("Masukkan Nama Mata Kuliah: ");
                mk.NamaMK = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(mk.NamaMK))
                {
                    Console.WriteLine("Nama Mata Kuliah tidak boleh kosong!");
                    mk.NamaMK = null;
                }
            } while (string.IsNullOrWhiteSpace(mk.NamaMK));

            //IDProdi
            string IDProdi;
            do
            {
                Console.Write("Masukkan Kode Prodi: ");
                IDProdi = Console.ReadLine()?.Trim().ToUpper();

                // validasi prodi harus ada di daftarProdi
                if (!daftarProdi.Any(p => string.Equals(p.IDProdi, IDProdi, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Kode Prodi tidak ditemukan! Coba lagi.");
                    continue;
                }
                break;
            } while (true);
            mk.IDProdi = IDProdi;


            // SKS (1-6)
            int SKSVal;
            do
            {
                Console.Write("Masukkan SKS (1-6): ");
                if (!int.TryParse(Console.ReadLine(), out SKSVal) || SKSVal < 1 || SKSVal > 6)
                {
                    Console.WriteLine("SKS harus antara 1 sampai 6!");
                    SKSVal = 0;
                }
            } while (SKSVal == 0);
            mk.SKS = SKSVal;

            // Semester (1-8) 
            int SemVal;
            do
            {
                Console.Write("Masukkan Semester (1-8): ");
                if (!int.TryParse(Console.ReadLine(), out SemVal) || SemVal < 1 || SemVal > 8)
                {
                    Console.WriteLine("Semester harus antara 1 sampai 8!");
                    SemVal = 0;
                }
            } while (SemVal == 0);
            mk.Semester = SemVal;

            // DosenPengampu
            do
            {
                Console.Write("Masukkan Nama Dosen Pengampu: ");
                mk.DosenPengampu = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(mk.DosenPengampu))
                {
                    Console.WriteLine("Nama Dosen Pengampu tidak boleh kosong!");
                    mk.DosenPengampu = null;
                }
            } while (string.IsNullOrWhiteSpace(mk.DosenPengampu));

            daftarMataKuliah.Add(mk);
            Console.WriteLine("\n Data mata kuliah berhasil ditambahkan!");
            mk.InputMataKuliah();
        }

        public void UbahMataKuliah()
        {
            Console.WriteLine("\n=== Ubah Mata Kuliah ===");
            if (daftarMataKuliah.Count == 0)
            {
                Console.WriteLine("Belum ada mata kuliah.");
                return;
            }
            DaftarMataKuliah();
            Console.Write("Masukkan Kode Mata Kuliah yang ingin diubah: ");
            string kode = Console.ReadLine()?.ToUpper().Trim();
            var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kode);
            if (mk == null)
            {
                Console.WriteLine("Kode tidak ditemukan.");
                return;
            }

            Console.Write($"Nama lama: {mk.NamaMK}. Masukkan nama baru (ENTER untuk skip): ");
            var nama = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nama) && nama.Trim().Length >= 3) mk.NamaMK = nama.Trim();

            Console.Write($"SKS lama: {mk.SKS}. Masukkan SKS baru (ENTER untuk skip): ");
            var sksIn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(sksIn) && int.TryParse(sksIn, out int sksNew) && sksNew >= 1 && sksNew <= 6) mk.SKS = sksNew;

            Console.Write($"Semester lama: {mk.Semester}. Masukkan semester baru (ENTER untuk skip): ");
            var semIn = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(semIn) && int.TryParse(semIn, out int semNew) && semNew >= 1 && semNew <= 8) mk.Semester = semNew;

            Console.Write($"Dosen lama: {mk.DosenPengampu}. Masukkan dosen baru (ENTER untuk skip): ");
            var dosen = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dosen)) mk.DosenPengampu = dosen.Trim();

            Console.WriteLine("Data mata kuliah berhasil diubah!");
        }

        public void HapusMataKuliah()
        {
            Console.WriteLine("\n=== Hapus Mata Kuliah ===");
            if (daftarMataKuliah.Count == 0)
            {
                Console.WriteLine("Belum ada mata kuliah.");
                return;
            }

            DaftarMataKuliah();
            Console.Write("Masukkan Kode Mata Kuliah yang ingin dihapus: ");

            string kode = Console.ReadLine()?.ToUpper().Trim();
            var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kode);
            if (mk == null)
            {
                Console.WriteLine("Kode tidak ditemukan.");
                return;
            }
            daftarMataKuliah.Remove(mk);
            Console.WriteLine("Mata kuliah berhasil dihapus!");
        }
    }
}
