using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class ProdiController
    {
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();

        public ProdiController(List<Mahasiswa> mhs, List<Prodi> prodi, List<MataKuliah> mk)
        {
            daftarProdi = prodi;
            daftarMahasiswa = mhs;
            daftarMataKuliah = mk;
        }

        public void DaftarProdi()
        {
            Console.WriteLine("\n=== Daftar Prodi ===");
            if (daftarProdi.Count == 0)
            {
                Console.WriteLine("Belum ada prodi terdaftar!");
                return;
            }

            int no = 1;
            foreach (var p in daftarProdi)
            {
                Console.WriteLine($"{no}. {p.KodeProdi} || Nama: {p.NamaProdi} || Alias: {p.AliasProdi}");
                no++;
            }
        }
        public void TambahProdi()
        {
            Console.WriteLine("\n=== Tambah Prodi ===");
            Prodi p = new Prodi();

            string kode;
            do
            {
                Console.Write("Masukkan Kode Prodi (5 digit angka): ");
                kode = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(kode))
                {
                    Console.WriteLine("Kode Prodi tidak boleh kosong!");
                    kode = null;
                    continue;
                }
                if (!IsAllDigits(kode) || kode.Length != 5)
                {
                    Console.WriteLine("Kode Prodi harus berupa 5 digit angka!");
                    kode = null;
                    continue;
                }
                if (daftarProdi.Any(x => x.KodeProdi == kode))
                {
                    Console.WriteLine("Kode Prodi sudah terdaftar. Gunakan kode lain.");
                    kode = null;
                }
            } while (string.IsNullOrWhiteSpace(kode));

            string nama;
            do
            {
                Console.Write("Masukkan Nama Prodi (minimal 10 karakter): ");
                nama = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(nama))
                {
                    Console.WriteLine("Nama Prodi tidak boleh kosong!");
                    nama = null;
                    continue;
                }
                if (nama.Length < 10)
                {
                    Console.WriteLine("Nama Prodi minimal 10 karakter!");
                    nama = null;
                    continue;
                }
                if (daftarProdi.Any(x => string.Equals(x.NamaProdi, nama, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Nama Prodi sudah terdaftar. Gunakan nama lain.");
                    nama = null;
                }
            } while (string.IsNullOrWhiteSpace(nama));

            string alias;
            do
            {
                Console.Write("Masukkan Alias Prodi (maks 15 karakter, ENTER untuk default = kode): ");
                alias = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(alias))
                {
                    alias = kode; // default
                    break;
                }
                if (alias.Length > 15)
                {
                    Console.WriteLine("Alias Prodi maksimal 15 karakter!");
                    alias = null;
                }
            } while (alias == null);
            daftarProdi.Add(new Prodi { KodeProdi = kode, NamaProdi = nama.ToUpper(), AliasProdi = alias });
            Console.WriteLine("Prodi berhasil ditambahkan!");
        }

        private bool IsAllDigits(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
