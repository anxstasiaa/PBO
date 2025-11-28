using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Project_PBO;

namespace Project_PBO
{
    internal class DosenController
    {
        static List<Dosen> daftarDosen = new List<Dosen>();
        static List<Prodi> daftarProdi = new List<Prodi>();

        public DosenController(List<Dosen> dd, List<Prodi> prodi)
        {
            daftarDosen = dd;
            daftarProdi = prodi;
        }
        public void TambahDosen()
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Dosen =====");

            Dosen dsn = new Dosen();
            string nidn;
            do
            {
                Console.Write("Masukkan NIDN (10 digit angka): ");
                nidn = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nidn) || nidn.Length != 10 || !nidn.All(char.IsDigit))
                {
                    Console.WriteLine("NIDN harus 10 digit angka!");
                    
                    nidn = "";
                }
            } while (nidn == "");

            dsn.NIDN = nidn;

            string nama;
            do
            {
                Console.Write("Masukkan Nama (HURUF KAPITAL, min 5 huruf): ");
                nama = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nama) || nama.Length < 5)
                {
                    Console.WriteLine("Nama minimal 5 huruf!");
                    
                    nama = "";
                }
                else if (!nama.All(c => c == ' ' || (c >= 'A' && c <= 'Z')))
                {
                    Console.WriteLine("Nama harus huruf KAPITAL tanpa angka/simbol!");
                    
                    nama = "";
                }
            } while (nama == "");

            dsn.NamaDosen = nama;


            char jk;
            do
            {
                Console.Write("Masukkan Jenis Kelamin (L/P): ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                jk = string.IsNullOrWhiteSpace(input) ? '\0' : input[0];

                if (jk != 'L' && jk != 'P')
                {
                    Console.WriteLine("Input tidak valid! Masukkan L atau P.");
                    
                    jk = '\0';
                }

            } while (jk == '\0');

            dsn.JenisKelamin = jk.ToString();


            Prodi selectedProdi = null;
            do
            {
                Console.WriteLine("\n--- Daftar Prodi ---");
                for (int i = 0; i < daftarProdi.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].KodeProdi})");
                }

                Console.Write("Pilih Prodi (nomor): ");
                if (int.TryParse(Console.ReadLine(), out int pilihanProdi) &&
                    pilihanProdi >= 1 && pilihanProdi <= daftarProdi.Count)
                {
                    selectedProdi = daftarProdi[pilihanProdi - 1];
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    
                }

            } while (selectedProdi == null);

            dsn.IDProdi = selectedProdi.KodeProdi;


            string[] daftarJabatan = {
                "ASISTEN AHLI", "LEKTOR", "LEKTOR KEPALA", "GURU BESAR"
            };

            string jabatan = null;
            do
            {
                Console.WriteLine("\n--- Pilih Jabatan Fungsional ---");
                for (int i = 0; i < daftarJabatan.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] {daftarJabatan[i]}");
                }

                Console.Write("Pilih Jabatan (nomor): ");
                if (int.TryParse(Console.ReadLine(), out int pilihJabatan) &&
                    pilihJabatan >= 1 && pilihJabatan <= daftarJabatan.Length)
                {
                    jabatan = daftarJabatan[pilihJabatan - 1];
                }
                else
                {
                    Console.WriteLine("Pilihan jabatan tidak valid!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Dosen =====");
                }

            } while (jabatan == null);

            dsn.JabatanFungsional = jabatan;

            daftarDosen.Add(dsn);

            Console.WriteLine("\n✓ Dosen berhasil ditambahkan!");
            dsn.InputDosen();
            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }
        public void DaftarDosen()
        {
            Console.Clear();
            Console.WriteLine("========== DAFTAR DOSEN ==========\n");

            if (daftarDosen.Count == 0)
            {
                Console.WriteLine("Belum ada data dosen yang tersimpan.");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
                return;
            }

            // Header tabel
            Console.WriteLine(
                $"{"No",-4} {"NIDN",-12} {"Nama Dosen",-25} {"JK",-5} {"Prodi",-10} {"Jabatan Fungsional"}");
            Console.WriteLine(new string('-', 80));

            int no = 1;
            foreach (var d in daftarDosen)
            {
                Console.WriteLine(
                    $"{no,-4} {d.NIDN,-12} {d.NamaDosen,-25} {d.JenisKelamin,-5} {d.IDProdi,-10} {d.JabatanFungsional}");
                no++;
            }

            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }
        public void EditDosen()
        {
            Console.Clear();
            Console.WriteLine("===== Edit Data Dosen =====");

            if (daftarDosen.Count == 0)
            {
                Console.WriteLine("Belum ada data dosen untuk diedit.");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
                return;
            }

            Console.Write("Masukkan NIDN dosen yang ingin diedit: ");
            string cariNIDN = Console.ReadLine()?.Trim();

            var dsn = daftarDosen.FirstOrDefault(d => d.NIDN == cariNIDN);
            if (dsn == null)
            {
                Console.WriteLine("NIDN tidak ditemukan!");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"\nData ditemukan: {dsn.NamaDosen} ({dsn.NIDN})");
            Console.WriteLine("Tekan ENTER untuk melewati bagian yang tidak ingin diubah.\n");

            // ===================== Ubah Nama =====================
            Console.Write($"Nama Lama: {dsn.NamaDosen}\nNama Baru: ");
            string namaBaru = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(namaBaru))
            {
                if (namaBaru.Length < 5 || !namaBaru.All(c => c == ' ' || (c >= 'A' && c <= 'Z')))
                {
                    Console.WriteLine("Nama harus huruf kapital dan minimal 5 huruf! (Perubahan dibatalkan)");
                }
                else
                {
                    dsn.NamaDosen = namaBaru;
                }
            }

            // ===================== Ubah Jenis Kelamin =====================
            Console.Write($"\nJenis Kelamin Lama: {dsn.JenisKelamin}\nJenis Kelamin Baru (L/P): ");
            string jkInput = Console.ReadLine()?.Trim().ToUpper();

            if (!string.IsNullOrWhiteSpace(jkInput))
            {
                if (jkInput == "L" || jkInput == "P")
                {
                    dsn.JenisKelamin = jkInput;
                }
                else
                {
                    Console.WriteLine("Input JK tidak valid! (Perubahan dibatalkan)");
                }
            }

            // ===================== Ubah Prodi =====================
            Console.WriteLine($"\nProdi Lama: {dsn.IDProdi}");
            Console.WriteLine("--- Daftar Prodi ---");
            for (int i = 0; i < daftarProdi.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].KodeProdi})");
            }

            Console.Write("Pilih Prodi Baru (nomor / ENTER untuk skip): ");
            string prodiInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(prodiInput))
            {
                if (int.TryParse(prodiInput, out int pilihProdi) &&
                    pilihProdi >= 1 && pilihProdi <= daftarProdi.Count)
                {
                    dsn.IDProdi = daftarProdi[pilihProdi - 1].KodeProdi;
                }
                else
                {
                    Console.WriteLine("Pilihan prodi tidak valid! (Perubahan dibatalkan)");
                }
            }

            // ===================== Ubah Jabatan Fungsional =====================
            string[] daftarJabatan =
            {
                    "ASISTEN AHLI",
                    "LEKTOR",
                    "LEKTOR KEPALA",
                    "GURU BESAR"
            };

            Console.WriteLine($"\nJabatan Lama: {dsn.JabatanFungsional}");
            Console.WriteLine("--- Daftar Jabatan Fungsional ---");

            for (int i = 0; i < daftarJabatan.Length; i++)
            {
                Console.WriteLine($"[{i + 1}] {daftarJabatan[i]}");
            }

            Console.Write("Pilih Jabatan Baru (nomor / ENTER untuk skip): ");
            string jbtInput = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(jbtInput))
            {
                if (int.TryParse(jbtInput, out int pilihJbt) &&
                    pilihJbt >= 1 && pilihJbt <= daftarJabatan.Length)
                {
                    dsn.JabatanFungsional = daftarJabatan[pilihJbt - 1];
                }
                else
                {
                    Console.WriteLine("Pilihan jabatan tidak valid! (Perubahan dibatalkan)");
                }
            }

            Console.WriteLine("\n✓ Data dosen berhasil diperbarui!");
            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }
        public void HapusDosen()
        {
            Console.Clear();
            Console.WriteLine("===== Hapus Data Dosen =====");

            if (daftarDosen.Count == 0)
            {
                Console.WriteLine("Belum ada data dosen untuk dihapus.");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
                return;
            }

            Console.Write("Masukkan NIDN dosen yang ingin dihapus: ");
            string cariNIDN = Console.ReadLine()?.Trim();

            var dsn = daftarDosen.FirstOrDefault(d => d.NIDN == cariNIDN);
            if (dsn == null)
            {
                Console.WriteLine("NIDN tidak ditemukan!");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
                return;
            }

            // Tampilkan data sebelum menghapus
            Console.WriteLine("\nData Dosen:");
            Console.WriteLine($"NIDN     : {dsn.NIDN}");
            Console.WriteLine($"Nama     : {dsn.NamaDosen}");
            Console.WriteLine($"Prodi    : {dsn.IDProdi}");
            Console.WriteLine($"JK       : {dsn.JenisKelamin}");
            Console.WriteLine($"Jabatan  : {dsn.JabatanFungsional}");
            Console.WriteLine($"Mengampu : {(dsn.MataKuliahDiampu.Count == 0 ? "-" : string.Join(", ", dsn.MataKuliahDiampu))}");

            // Konfirmasi hapus
            Console.Write("\nApakah Anda yakin ingin menghapus dosen ini? (Y/N): ");
            string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

            if (konfirmasi == "Y")
            {
                daftarDosen.Remove(dsn);

                Console.WriteLine("\n✓ Data dosen berhasil dihapus!");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nPenghapusan dibatalkan.");
                Console.WriteLine("\nTekan ENTER untuk kembali...");
                Console.ReadLine();
            }
        }
    }
}
