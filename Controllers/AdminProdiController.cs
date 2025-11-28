using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class AdminProdiController
    {
        static List<AdminProdi> daftarAdminProdi = new List<AdminProdi>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
       //static List<Semester> daftarSemester = SemesterController.daftarSemester;
        static List<Semester> daftarSemester = new List<Semester>();


        public AdminProdiController(List<AdminProdi> admp, List<Prodi> prodi, List<KelasKuliah> kelasKuliah, List<MataKuliah> mataKuliah, List<Semester> semester)
        {
            daftarAdminProdi = admp;
            daftarProdi = prodi;
            daftarKelasKuliah = kelasKuliah;
            daftarMataKuliah = mataKuliah;
            daftarSemester = semester;
        }

        // Read: tampilkan semua Prodi atau hanya Prodi milik admin (filter by idProdi)
        public void DaftarProdi(string IDProdi)
        {
            Console.Clear();
            Console.WriteLine("===== Daftar Program Studi =====");
            if (daftarProdi == null || daftarProdi.Count == 0)
            {
                Console.WriteLine("Belum ada data Program Studi.");
                return;
            }

            int idx = 1;
            foreach (var p in daftarProdi)
            {
                Console.WriteLine($"{idx}. Kode: {p.IDProdi}, Nama: {p.NamaProdi}, Alias: {p.AliasProdi}");
                idx++;
            }
        }

        // Create: tambah Prodi baru dengan validasi
        public void TambahProdi()
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Program Studi =====");

            // IDProdi: not empty, max 5, unique
            string kode;
            while (true)
            {
                Console.Write("Kode Prodi (max 5, mis. ILKOM): ");
                kode = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                if (string.IsNullOrWhiteSpace(kode))
                {
                    Console.WriteLine("Kode Prodi tidak boleh kosong. Coba lagi. \n");

                    continue;
                }
                if (kode.Length > 5)
                {
                    Console.WriteLine("Kode Prodi maksimal 5 karakter.");

                    continue;
                }
                if (daftarProdi.Any(x => string.Equals(x.IDProdi, kode, StringComparison.OrdinalIgnoreCase)))
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
                Console.Write("\nAlias Prodi (max 15 karakter): ");
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
                IDProdi = kode,
                NamaProdi = nama,
                AliasProdi = alias
            };

            daftarProdi.Add(prodi);
            Console.WriteLine("Program Studi berhasil ditambahkan.");
            Console.WriteLine("\nTekan ENTER untuk kembali...");
            Console.ReadLine();
        }

        
        

        
    }
}
