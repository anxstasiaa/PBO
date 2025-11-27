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

        static string[] prodiValid = { "ILMU KOMPUTER", "BIOLOGI", "FISIKA" };

        public AdminUnivController(List<AdminUniv> admu, List<Mahasiswa> mhs, List<Prodi> prodi)
        {
            daftarAdminUniv = admu;
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
        }

        public void DaftarMahasiswa()
        {
            Console.WriteLine("\n=== Daftar Mahasiswa ===");
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada mahasiswa terdaftar!");
            }
            else
            {
                int no = 1;
                foreach (var mhs in daftarMahasiswa)
                {
                    string jk = (mhs.JenisKelamin == 'L') ? "Laki-laki" : "Perempuan";
                    Console.WriteLine($"{no}. {mhs.NamaMhs} || ({mhs.NIM}) || {mhs.IDProdi} ||  ({jk}) || Angkatan {mhs.Angkatan} || Lahir: {mhs.TanggalLahir:dd-MM-yyyy}");
                    no++;
                }
            }
        }

        public void TambahMahasiswa()
        {
            Console.WriteLine("\n=== Tambah Mahasiswa ===");
            Mahasiswa mhs = new Mahasiswa();

            // Validasi Nama
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

            // Validasi NIM
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

            } while (nim == "");
            mhs.NIM = nim;

            // tanggal lahir
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

            // Input Prodi — validate against daftarProdi
            // Input Prodi — validate against daftarProdi
            Prodi selectedProdi = null;
            do
            {
                Console.Write("Masukkan Prodi (nama / alias / kode): ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input prodi kosong! Coba lagi.");
                    continue;
                }

                selectedProdi = daftarProdi.FirstOrDefault(p =>
                    string.Equals(p.KodeProdi, input, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(p.NamaProdi, input, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(p.AliasProdi, input, StringComparison.OrdinalIgnoreCase)
                );

                if (selectedProdi == null)
                {
                    Console.WriteLine("Prodi tidak ditemukan! Coba lagi.");
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

            Console.WriteLine("\nData mahasiswa berhasil ditambahkan!");
            mhs.InputMahasiswa();
        }

        public void UbahMahasiswa()
        {
            Console.WriteLine("\n=== Ubah Data Mahasiswa ===");
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada mahasiswa untuk diubah.");
                return;
            }
            DaftarMahasiswa();
            Console.Write("Masukkan NIM mahasiswa yang ingin diubah: ");
            string CariNIM = Console.ReadLine();
            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == CariNIM);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan!");
                return;
            }
            Console.WriteLine();

            string nama;
            do
            {
                Console.Write($"Nama lama: {mhs.NamaMhs} ");
                Console.Write("\nMasukkan nama baru (ENTER untuk skip): ");
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

            string prodi = null;
            bool validProdi = false;
            do
            {
                Console.Write($"KodeProdi lama: {mhs.IDProdi} ");
                Console.Write("\nMasukkan prodi baru (ENTER untuk skip): ");
                prodi = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(prodi))
                    break;
                prodi = prodi.ToUpper().Trim();
                validProdi = false;
                foreach (var p in prodiValid)
                {
                    if (prodi == p)
                    {
                        validProdi = true;
                        break;
                    }
                }
                if (!validProdi)
                {
                    Console.WriteLine("KodeProdi tidak valid! Coba lagi.");
                }
            } while (!string.IsNullOrWhiteSpace(prodi) && !validProdi);
            if (!string.IsNullOrWhiteSpace(prodi) && validProdi) mhs.IDProdi = prodi;

            string inputAngkatan;
            int angkatan;
            do
            {
                Console.Write($"Angkatan lama: {mhs.Angkatan} ");
                Console.Write("\nMasukkan Angkatan baru (2018-2025, ENTER untuk skip): ");
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

            string inputJK;
            do
            {
                Console.Write($"Jenis Kelamin lama: {(mhs.JenisKelamin == 'L' ? "Laki-laki" : "Perempuan")}");
                Console.Write("\nMasukkan Jenis Kelamin baru (L/P, ENTER untuk skip): ");
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

            Console.WriteLine("Data mahasiswa berhasil diubah!");
        }

        public void HapusMahasiswa()
        {
            Console.WriteLine("\n=== Hapus Data Mahasiswa ===");
            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada mahasiswa untuk dihapus.");
                return;
            }
            DaftarMahasiswa();
            Console.Write("Masukkan NIM mahasiswa yang ingin dihapus: ");
            string CariNIM = Console.ReadLine();
            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == CariNIM);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan!");
                return;
            }
            Console.WriteLine();

            daftarMahasiswa.Remove(mhs);
            Console.WriteLine("Data mahasiswa berhasil dihapus!");
        }

        // Fungsi cek string hanya angka
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

        // Fungsi cek nama hanya huruf kapital A-Z
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
