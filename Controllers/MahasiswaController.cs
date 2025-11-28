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

        public MahasiswaController(List<Mahasiswa> mhs, List<Prodi> prodi)
        {
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
        }

        public void TambahMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Mahasiswa =====");

            Mahasiswa mhs = new Mahasiswa();

            string nim;
            do
            {
                Console.Write("Masukkan NIM (10 digit angka): ");
                nim = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nim) || nim.Length != 10 || !nim.All(char.IsDigit))
                {
                    Console.WriteLine("NIM harus 10 digit angka.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                    nim = "";
                }
                else if (daftarMahasiswa.Any(a => a.NIM == nim))
                {
                    Console.WriteLine("NIM sudah terdaftar.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                    nim = "";
                }
            } while (nim == "");

            mhs.NIM = nim;

            string nama;
            do
            {
                Console.Write("Masukkan Nama (huruf kapital, minimal 5 huruf): ");
                nama = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(nama) || nama.Length < 5)
                {
                    Console.WriteLine("Nama minimal 5 huruf.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                    nama = "";
                }
                else if (!nama.All(c => c == ' ' || (c >= 'A' && c <= 'Z')))
                {
                    Console.WriteLine("Nama harus huruf kapital.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                    nama = "";
                }
            } while (nama == "");

            mhs.NamaMhs = nama;

            Console.Write("Masukkan Alamat: ");
            mhs.AlamatMhs = Console.ReadLine();

            char jk;
            do
            {
                Console.Write("Jenis Kelamin (L/P): ");
                string input = Console.ReadLine()?.Trim().ToUpper();
                jk = string.IsNullOrWhiteSpace(input) ? '\0' : input[0];

                if (jk != 'L' && jk != 'P')
                {
                    Console.WriteLine("Masukkan L atau P.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                }
            } while (jk != 'L' && jk != 'P');

            mhs.JenisKelamin = jk;

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
                    Console.WriteLine("Pilihan tidak valid.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                }
            } while (selectedProdi == null);

            mhs.IDProdi = selectedProdi.KodeProdi;

            int angkatan;
            do
            {
                Console.Write("Masukkan Angkatan (2018 - 2025): ");
                if (!int.TryParse(Console.ReadLine(), out angkatan) ||
                    angkatan < 2018 || angkatan > 2025)
                {
                    Console.WriteLine("Input angkatan tidak valid.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                    angkatan = -1;
                }
            } while (angkatan == -1);

            mhs.Angkatan = angkatan;

            Console.Write("Masukkan Semester Aktif: ");
            mhs.SemesterAktif = Console.ReadLine();

            DateTime tglLahir;
            do
            {
                Console.Write("Masukkan Tanggal Lahir (dd-MM-yyyy): ");
                string tglInput = Console.ReadLine();
                if (!DateTime.TryParseExact(tglInput, "dd-MM-yyyy", null,
                    System.Globalization.DateTimeStyles.None, out tglLahir))
                {
                    Console.WriteLine("Format tanggal tidak valid.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Console.WriteLine("===== Tambah Mahasiswa =====");
                }
            } while (tglLahir == default(DateTime));

            mhs.TanggalLahir = tglLahir;

            daftarMahasiswa.Add(mhs);

            Console.WriteLine("\nData mahasiswa berhasil ditambahkan.");
            mhs.InputMahasiswa();
            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }

        public void DaftarMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Daftar Mahasiswa =====");

            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }

            int no = 1;
            foreach (var m in daftarMahasiswa)
            {
                Console.Write($"{no}. ");
                m.InputMahasiswa();
                no++;
            }

            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }

        public void UbahMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Ubah Data Mahasiswa =====");

            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }

            Console.Write("Masukkan NIM mahasiswa: ");
            string nim = Console.ReadLine();

            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == nim);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan.");
                return;
            }

            Console.WriteLine("Tekan ENTER untuk melewati perubahan.");

            Console.Write($"Nama lama: {mhs.NamaMhs}\nNama baru: ");
            string nama = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nama)) mhs.NamaMhs = nama;

            Console.Write($"Alamat lama: {mhs.AlamatMhs}\nAlamat baru: ");
            string alamat = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(alamat)) mhs.AlamatMhs = alamat;

            Console.Write($"Semester lama: {mhs.SemesterAktif}\nSemester baru: ");
            string sem = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(sem)) mhs.SemesterAktif = sem;

            Console.WriteLine("\nData mahasiswa berhasil diubah.");
        }

        public void HapusMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("===== Hapus Mahasiswa =====");

            if (daftarMahasiswa.Count == 0)
            {
                Console.WriteLine("Belum ada data mahasiswa.");
                return;
            }

            Console.Write("Masukkan NIM mahasiswa yang akan dihapus: ");
            string nim = Console.ReadLine();

            var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == nim);
            if (mhs == null)
            {
                Console.WriteLine("NIM tidak ditemukan.");
                return;
            }

            Console.Write("Yakin ingin menghapus? (Y/N): ");
            string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

            if (konfirmasi == "Y")
            {
                daftarMahasiswa.Remove(mhs);
                Console.WriteLine("Data mahasiswa berhasil dihapus.");
            }
            else
            {
                Console.WriteLine("Penghapusan dibatalkan.");
            }
        }
    }
}