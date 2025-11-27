using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminUnivController
    {
        static List<AdminUniv> daftarAdminUniv = new List<AdminUniv>();
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Nilai> daftarNilai = new List<Nilai>();

        static string[] prodiValid = { "ILMU KOMPUTER", "BIOLOGI", "FISIKA" };

        public AdminUnivController(List<AdminUniv> admu, List<Mahasiswa> mhs, List<Prodi> prodi, List<Nilai> nilai)
        {
            daftarAdminUniv = admu;
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
            daftarNilai = nilai;
        }

        public void TambahProdi()
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Program Studi =====");

            // KodeProdi: not empty, max 5, unique
            string kode;
            while (true)
            {
                Console.Write("Kode Prodi (max 5 karakter, contoh: ILKOM): ");
                kode = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(kode))
                {
                    Console.WriteLine("Kode Prodi tidak boleh kosong.");
                    continue;
                }
                if (kode.Length > 5)
                {
                    Console.WriteLine("Kode Prodi maksimal 5 karakter.");
                    continue;
                }
                if (daftarProdi.Any(x => string.Equals(x.KodeProdi, kode, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Kode Prodi sudah ada. Gunakan kode lain.");
                    continue;
                }
                break;
            }

            // NamaProdi: not empty, min 10
            string nama;
            while (true)
            {
                Console.Write("Nama Prodi (min 10 karakter): ");
                nama = (Console.ReadLine() ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(nama) || nama.Length < 10)
                {
                    Console.WriteLine("Nama Prodi harus minimal 10 karakter.");
                    continue;
                }
                break;
            }

            // AliasProdi: not empty, max 15
            string alias;
            while (true)
            {
                Console.Write("Alias Prodi (max 15 karakter): ");
                alias = (Console.ReadLine() ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(alias))
                {
                    Console.WriteLine("Alias Prodi tidak boleh kosong.");
                    continue;
                }
                if (alias.Length > 15)
                {
                    Console.WriteLine("Alias Prodi maksimal 15 karakter.");
                    continue;
                }
                break;
            }

            var prodi = new Prodi
            {
                KodeProdi = kode,
                NamaProdi = nama,
                AliasProdi = alias
            };

            daftarProdi.Add(prodi);
            Console.WriteLine("\n✓ Program Studi berhasil ditambahkan!");
            prodi.InputProdi();
        }

        // READ: Lihat Daftar Semua Prodi
        public void DaftarProdi()
        {
            Console.Clear();
            Console.WriteLine("===== Daftar Semua Program Studi =====");
            if (daftarProdi == null || daftarProdi.Count == 0)
            {
                Console.WriteLine("Belum ada data Program Studi.");
                return;
            }

            Console.WriteLine("No | Kode  | Nama Program Studi            | Alias");
            Console.WriteLine("---+-------+-------------------------------+---------------");
            int idx = 1;
            foreach (var p in daftarProdi)
            {
                Console.WriteLine($"{idx,2} | {p.KodeProdi,-5} | {p.NamaProdi,-29} | {p.AliasProdi}");
                idx++;
            }
        }


        public void DaftarMahasiswa(string filterProdi = null, int? filterAngkatan = null)
        {
            Console.Clear();
            Console.WriteLine("===== Daftar Mahasiswa =====");

            // Apply filters
            var mahasiswaFiltered = daftarMahasiswa.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(filterProdi))
            {
                mahasiswaFiltered = mahasiswaFiltered.Where(m =>
                    string.Equals(m.IDProdi, filterProdi, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"Filter: Prodi = {filterProdi}");
            }

            if (filterAngkatan.HasValue)
            {
                mahasiswaFiltered = mahasiswaFiltered.Where(m => m.Angkatan == filterAngkatan.Value);
                Console.WriteLine($"Filter: Angkatan = {filterAngkatan.Value}");
            }

            var hasil = mahasiswaFiltered.ToList();

            if (hasil.Count == 0)
            {
                Console.WriteLine("Tidak ada mahasiswa ditemukan dengan filter tersebut.");
                return;
            }

            Console.WriteLine("\nNo | NIM        | Nama                    | Prodi | JK | Angkatan | Tanggal Lahir");
            Console.WriteLine("---+------------+-------------------------+-------+----+----------+---------------");

            int no = 1;
            foreach (var mhs in hasil)
            {
                string jk = (mhs.JenisKelamin == 'L') ? "L" : "P";
                Console.WriteLine($"{no,2} | {mhs.NIM,-10} | {mhs.NamaMhs,-23} | {mhs.IDProdi,-5} | {jk}  | {mhs.Angkatan,8} | {mhs.TanggalLahir:dd-MM-yyyy}");
                no++;
            }
            Console.WriteLine($"\nTotal: {hasil.Count} mahasiswa");
        }

        // Menu Filter untuk Daftar Mahasiswa
        public void MenuDaftarMahasiswa()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("===== Lihat Daftar Mahasiswa =====");
                Console.WriteLine("[1] Semua Mahasiswa");
                Console.WriteLine("[2] Filter berdasarkan Prodi");
                Console.WriteLine("[3] Filter berdasarkan Angkatan");
                Console.WriteLine("[4] Filter berdasarkan Prodi dan Angkatan");
                Console.WriteLine("[5] Kembali");
                Console.WriteLine("==================================");
                Console.Write("Pilih menu: ");

                if (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Input tidak valid!");
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        DaftarMahasiswa();
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;

                    case 2:
                        Console.Write("\nMasukkan Kode Prodi: ");
                        string prodi = Console.ReadLine()?.Trim().ToUpper();
                        DaftarMahasiswa(filterProdi: prodi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;

                    case 3:
                        Console.Write("\nMasukkan Angkatan (2018-2025): ");
                        if (int.TryParse(Console.ReadLine(), out int angkatan))
                        {
                            DaftarMahasiswa(filterAngkatan: angkatan);
                        }
                        else
                        {
                            Console.WriteLine("Angkatan tidak valid!");
                        }
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;

                    case 4:
                        Console.Write("\nMasukkan Kode Prodi: ");
                        string prodiFilter = Console.ReadLine()?.Trim().ToUpper();
                        Console.Write("Masukkan Angkatan (2018-2025): ");
                        if (int.TryParse(Console.ReadLine(), out int angkatanFilter))
                        {
                            DaftarMahasiswa(filterProdi: prodiFilter, filterAngkatan: angkatanFilter);
                        }
                        else
                        {
                            Console.WriteLine("Angkatan tidak valid!");
                        }
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;

                    case 5:
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                }
            } while (pilihan != 5);
        }

        // CREATE: Tambah Mahasiswa Baru
        public void TambahMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Mahasiswa =====");
            Mahasiswa mhs = new Mahasiswa();

            // Validasi Nama (KAPITAL, min 5 huruf)
            string nama;
            do
            {
                Console.Write("Masukkan Nama (HURUF KAPITAL, minimal 5 huruf): ");
                nama = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nama) || nama.Length < 5)
                {
                    Console.WriteLine("Nama minimal 5 huruf!");
                    nama = "";
                }
                else if (!IsAllUppercaseLetters(nama))
                {
                    Console.WriteLine("NAMA HARUS KAPITAL TANPA ANGKA/SIMBOL!");
                    nama = "";
                }
            } while (nama == "");
            mhs.NamaMhs = nama;

            // Validasi NIM (10 digit, unique)
            string nim;
            do
            {
                Console.Write("Masukkan NIM (harus 10 digit angka): ");
                nim = Console.ReadLine();

                if (!IsAllDigits(nim))
                {
                    Console.WriteLine("NIM hanya boleh berisi angka!");
                    nim = "";
                }
                else if (nim.Length != 10)
                {
                    Console.WriteLine("NIM harus tepat 10 digit!");
                    nim = "";
                }
                else if (daftarMahasiswa.Any(m => m.NIM == nim))
                {
                    Console.WriteLine("NIM sudah terdaftar! Gunakan NIM lain.");
                    nim = "";
                }
            } while (nim == "");
            mhs.NIM = nim;

            // Tanggal Lahir
            DateTime tglLahir;
            do
            {
                Console.Write("Masukkan Tanggal Lahir (dd-MM-yyyy): ");
                string tglInput = Console.ReadLine();
                if (!DateTime.TryParseExact(tglInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out tglLahir))
                {
                    Console.WriteLine("Format tanggal tidak valid! Gunakan format dd-MM-yyyy.");
                }
            } while (tglLahir == default(DateTime));
            mhs.TanggalLahir = tglLahir;

            // Input Prodi (validate against daftarProdi)
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

            mhs.IDProdi = selectedProdi.KodeProdi;

            // Angkatan
            int angkatan;
            do
            {
                Console.Write("Masukkan Angkatan (2018 - 2025): ");
                if (!int.TryParse(Console.ReadLine(), out angkatan) || angkatan < 2018 || angkatan > 2025)
                {
                    Console.WriteLine("Input angkatan tidak valid!");
                    angkatan = -1;
                }
            } while (angkatan == -1);
            mhs.Angkatan = angkatan;

            // Jenis Kelamin
            char jk;
            do
            {
                Console.Write("Masukkan Jenis Kelamin (L/P): ");
                string input = Console.ReadLine();
                jk = string.IsNullOrWhiteSpace(input) ? '\0' : Char.ToUpper(input[0]);
            }
            while (jk != 'L' && jk != 'P');
            mhs.JenisKelamin = jk;

            daftarMahasiswa.Add(mhs);

            Console.WriteLine("\n✓ Data mahasiswa berhasil ditambahkan!");
            mhs.InputMahasiswa();
        }

        // UPDATE: Ubah Data Mahasiswa
        public void UbahMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Ubah Data Mahasiswa =====");
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada mahasiswa untuk diubah.");
                return;
            }

            Console.Write("Masukkan NIM mahasiswa yang ingin diubah: ");
            string CariNIM = Console.ReadLine();
            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == CariNIM);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan!");
                return;
            }

            Console.WriteLine($"\nData mahasiswa ditemukan: {mhs.NamaMhs} ({mhs.NIM})");
            Console.WriteLine("(Tekan ENTER untuk skip/tidak mengubah)\n");

            // Ubah Nama
            string nama;
            do
            {
                Console.Write($"Nama lama: {mhs.NamaMhs}\nNama baru: ");
                nama = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nama))
                    break;
                if (nama.Length < 5)
                {
                    Console.WriteLine("Nama minimal 5 huruf!");
                    nama = null;
                }
                else if (!IsAllUppercaseLetters(nama))
                {
                    Console.WriteLine("Nama harus huruf kapital A-Z tanpa angka/simbol!");
                    nama = null;
                }
            } while (nama == null);
            if (!string.IsNullOrWhiteSpace(nama)) mhs.NamaMhs = nama;

            // Ubah Prodi
            Console.WriteLine($"\nProdi lama: {mhs.IDProdi}");
            Console.WriteLine("--- Daftar Prodi ---");
            for (int i = 0; i < daftarProdi.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].KodeProdi})");
            }
            Console.Write("Pilih Prodi baru (nomor, ENTER untuk skip): ");
            string prodiInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(prodiInput))
            {
                if (int.TryParse(prodiInput, out int pilihanProdi) &&
                    pilihanProdi >= 1 && pilihanProdi <= daftarProdi.Count)
                {
                    mhs.IDProdi = daftarProdi[pilihanProdi - 1].KodeProdi;
                }
            }

            // Ubah Angkatan
            string inputAngkatan;
            int angkatan;
            do
            {
                Console.Write($"\nAngkatan lama: {mhs.Angkatan}\nAngkatan baru (2018-2025, ENTER untuk skip): ");
                inputAngkatan = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputAngkatan))
                    break;
                if (!int.TryParse(inputAngkatan, out angkatan) || angkatan < 2018 || angkatan > 2025)
                {
                    Console.WriteLine("Angkatan harus diantara 2018-2025!");
                    inputAngkatan = null;
                }
            } while (inputAngkatan == null);
            if (!string.IsNullOrWhiteSpace(inputAngkatan)) mhs.Angkatan = int.Parse(inputAngkatan);

            // Ubah Jenis Kelamin
            string inputJK;
            do
            {
                Console.Write($"\nJenis Kelamin lama: {(mhs.JenisKelamin == 'L' ? "Laki-laki" : "Perempuan")}\nJenis Kelamin baru (L/P, ENTER untuk skip): ");
                inputJK = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputJK))
                    break;
                inputJK = inputJK.ToUpper();
                if (inputJK != "L" && inputJK != "P")
                {
                    Console.WriteLine("Input tidak valid! Masukkan L atau P.");
                    inputJK = null;
                }
            } while (inputJK == null);
            if (!string.IsNullOrWhiteSpace(inputJK)) mhs.JenisKelamin = inputJK[0];

            Console.WriteLine("\n✓ Data mahasiswa berhasil diubah!");
        }

        // DELETE: Hapus Mahasiswa (jika belum ada nilai)
        public void HapusMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Hapus Data Mahasiswa =====");
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada mahasiswa untuk dihapus.");
                return;
            }

            Console.Write("Masukkan NIM mahasiswa yang ingin dihapus: ");
            string CariNIM = Console.ReadLine();
            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == CariNIM);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan!");
                return;
            }

            // CEK: Apakah mahasiswa sudah memiliki nilai?
            bool adaNilai = daftarNilai.Any(n => n.NIM == CariNIM);
            if (adaNilai)
            {
                Console.WriteLine($"\n✗ Mahasiswa {mhs.NamaMhs} ({CariNIM}) tidak dapat dihapus!");
                Console.WriteLine("Alasan: Mahasiswa sudah memiliki data nilai.");
                Console.WriteLine("Hapus data nilai terlebih dahulu sebelum menghapus mahasiswa.");
                return;
            }

            // Konfirmasi penghapusan
            Console.WriteLine($"\nData mahasiswa:");
            Console.WriteLine($"NIM: {mhs.NIM}");
            Console.WriteLine($"Nama: {mhs.NamaMhs}");
            Console.WriteLine($"Prodi: {mhs.IDProdi}");
            Console.WriteLine($"Angkatan: {mhs.Angkatan}");
            Console.Write("\nApakah Anda yakin ingin menghapus? (Y/N): ");
            string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

            if (konfirmasi == "Y")
            {
                daftarMahasiswa.Remove(mhs);
                Console.WriteLine("\n✓ Data mahasiswa berhasil dihapus!");
            }
            else
            {
                Console.WriteLine("\nPenghapusan dibatalkan.");
            }
        }

        // Helper functions
        static bool IsAllDigits(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        static bool IsAllUppercaseLetters(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (char c in s)
            {
                if (!(c >= 'A' && c <= 'Z') && c != ' ')
                    return false;
            }
            return true;
        }
    }
}
