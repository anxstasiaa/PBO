using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project_PBO
{
    class Program
    {
        static List<Mahasiswa> daftarMahasiswa = new List<Mahasiswa>();
        static List<Dosen> daftarDosen = new List<Dosen>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<AdminProdi> daftarAdminProdi = new List<AdminProdi>();
        static List<AdminUniv> daftarAdminUniv = new List<AdminUniv>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<Semester> daftarSemester = new List<Semester>();
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<Nilai> daftarNilai = new List<Nilai>();
        static List<User> Users = new List<User>
        {
            new User { Username="admin-univ", Password="123", Role="AdminUniv" },
            new User { Username="admin-prodi", Password="123", Role="AdminProdi", IDProdi="ILKOM" },
            new User { Username="mhs", Password="123", Role="Mahasiswa", NIM="240608", IDProdi="ILKOM" },
            new User { Username="dosen", Password="123", Role="Dosen", IDProdi="ILKOM" }
        };

        static MahasiswaController mhsCtrl;
        static DosenController dsnCtrl;
        static ProdiController ProdiCtrl;
        static AdminProdiController AdmProdiCtrl;
        static AdminUnivController AdmUnivCtrl;
        static MataKuliahController MKCtrl;
        static SemesterController SemCtrl;
        static KelasKuliahController KKCtrl;
        static NilaiController NilaiCtrl;

        static void Main()
        {
            if (daftarProdi.Count == 0)
            {
                daftarProdi.Add(new Prodi { KodeProdi = "11111", NamaProdi = "Ilmu Komputer", AliasProdi = "ILKOM" });
                daftarProdi.Add(new Prodi { KodeProdi = "22222", NamaProdi = "Biologi", AliasProdi = "BIO" });
                daftarProdi.Add(new Prodi { KodeProdi = "33333", NamaProdi = "Fisika", AliasProdi = "FIS" });
            }

            mhsCtrl = new MahasiswaController(daftarMahasiswa, daftarProdi);
            MKCtrl = new MataKuliahController(daftarProdi);
            ProdiCtrl = new ProdiController(daftarMahasiswa, daftarProdi, daftarMataKuliah);
            SemCtrl = new SemesterController(daftarSemester);
            KKCtrl = new KelasKuliahController(daftarKelasKuliah, daftarMataKuliah, daftarProdi, daftarSemester, daftarDosen);
            NilaiCtrl = new NilaiController(daftarNilai, daftarMahasiswa, daftarKelasKuliah, daftarMataKuliah);
            dsnCtrl = new DosenController(daftarDosen, daftarProdi);

            AdmProdiCtrl = new AdminProdiController(daftarAdminProdi, daftarProdi);
            AdmUnivCtrl = new AdminUnivController(daftarAdminUniv, daftarMahasiswa, daftarProdi, daftarNilai);

            int login;
            do
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("     Sistem Akademik Universitas   ");
                Console.WriteLine("===================================");
                Console.WriteLine("[1] Login sebagai Admin Universitas");
                Console.WriteLine("[2] Login sebagai Admin Prodi      ");
                Console.WriteLine("[3] Login sebagai Dosen            ");
                Console.WriteLine("[4] Login sebagai Mahasiswa        ");
                Console.WriteLine("[5] Keluar                         ");
                Console.WriteLine("===================================");
                Console.Write("Pilih menu: ");
                while (!int.TryParse(Console.ReadLine(), out login))
                {
                    Console.WriteLine("Input tidak valid. Silakan masukkan angka.");
                    Console.WriteLine("Pilih menu: ");
                }
                switch (login)
                {
                    case 1:
                        LoginAdmUniv();
                        break;
                    case 2:
                        LoginAdmProdi();
                        break;
                    case 3:
                        LoginDosen();
                        break;
                    case 4:
                        LoginMahasiswa();
                        break;
                    case 5:
                        Console.WriteLine("Terima kasih telah menggunakan sistem ini.");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        Console.ReadLine();
                        break;
                }
            } while (login != 5);
        }

        static void LoginAdmUniv()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("             Login User            ");
            Console.WriteLine("==================================");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "adminuniv")
            {
                Console.WriteLine("Akun ini bukan Admin Universitas. Gunakan menu yang sesuai.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            MenuAdminUniv();
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }

        static void MenuAdminUniv()
        {
            int pilih;
            do 
            {
                Console.Clear();
                Console.WriteLine("======== MENU ADMIN UNIVERSITAS ========");
                Console.WriteLine("1. Manajemen Prodi");
                Console.WriteLine("2. Manajemen Mahasiswa");
                Console.WriteLine("3. Logout");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");

                if (!int.TryParse(Console.ReadLine(), out pilih))
                    pilih = 0;

                switch (pilih)
                {
                    case 1:
                        MenuUnivProdi();
                        break;

                    case 2:
                        MenuMahasiswaAdmin();
                        break;

                    case 3:
                        Console.WriteLine("Logout...");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Console.ReadLine();
                        break;
                }

            } while (pilih != 3);
        }

        static void MenuUnivProdi()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("======== MANAGEMEN PRODI ========");
                Console.WriteLine("1. Daftar Prodi");
                Console.WriteLine("2. Tambah Prodi");
                Console.WriteLine("3. Kembali ke Menu Admin Univ");
                Console.WriteLine("=================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilihan))
                    pilihan = 0;
                switch (pilihan)
                {
                    case 1:
                        ProdiCtrl.DaftarProdi();
                        break;
                    case 2:
                        ProdiCtrl.TambahProdi();
                        break;
                    case 3:
                        Console.WriteLine("Kembali ke Menu Admin Univ...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Console.ReadLine();
                        break;
                }
            } while (pilihan != 3);
        }

        static void MenuMahasiswaAdmin()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("======== MANAGEMEN MAHASISWA ========");
                Console.WriteLine("1. Daftar Mahasiswa");
                Console.WriteLine("2. Tambah Mahasiswa");
                Console.WriteLine("3. Ubah Mahasiswa");
                Console.WriteLine("4. Hapus Mahasiswa");
                Console.WriteLine("5. Kembali ke Menu Admin Univ");
                Console.WriteLine("=====================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilihan))
                    pilihan = 0;
                switch (pilihan)
                {
                    case 1:
                        AdmUnivCtrl.DaftarMahasiswa();
                        break;
                    case 2:
                        AdmUnivCtrl.TambahMahasiswa();
                        break;
                    case 3:
                        AdmUnivCtrl.UbahMahasiswa();
                        break;
                    case 4:
                        AdmUnivCtrl.HapusMahasiswa();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke Menu Admin Univ...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Console.ReadLine();
                        break;
                }
            } while (pilihan != 5);
        }

        static void LoginAdmProdi()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("           Login Admin Prodi      ");
            Console.WriteLine("==================================");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "adminprodi")
            {
                Console.WriteLine("Akun ini bukan Admin Prodi. Gunakan menu yang sesuai.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("       Pilih Program Studi        ");
            Console.WriteLine("==================================");
            if (daftarProdi.Count == 0)
            {
                Console.WriteLine("Belum ada prodi terdaftar!");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }
            // Tampilkan daftar prodi
            for (int i = 0; i < daftarProdi.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].KodeProdi})");
            }
            Console.WriteLine("==================================");

            int pilihanProdi;
            do
            {
                Console.Write("Pilih prodi (masukkan nomor): ");
                if (!int.TryParse(Console.ReadLine(), out pilihanProdi) || pilihanProdi < 1 || pilihanProdi > daftarProdi.Count)
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    pilihanProdi = 0;
                }
            } while (pilihanProdi == 0);

            user.IDProdi = daftarProdi[pilihanProdi - 1].KodeProdi;

            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            // pass user so MenuAdminProdi can restrict to user.IDProdi
            MenuAdminProdi(user);
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }


        static void MenuAdminProdi(User user)
        {
            string idProdi = (user.IDProdi ?? string.Empty).Trim().ToUpper();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine($"     Menu Admin Prodi ({user.IDProdi})   ");
                Console.WriteLine("========================================");
                Console.WriteLine("[1] Lihat Daftar Mahasiswa");
                Console.WriteLine("[2] Kelola Mata Kuliah");
                Console.WriteLine("[3] Kelola Kelas Kuliah");
                Console.WriteLine("[4] Lihat Daftar Prodi");
                Console.WriteLine("[5] Tambah Prodi");
                Console.WriteLine("[6] Keluar");
                Console.WriteLine("========================================");
                Console.Write("Pilih menu: ");
                while (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Input tidak valid. Silakan masukkan angka.");
                    Console.Write("Pilih menu: ");
                }
                switch (choice)
                {
                    case 1:
                        DaftarMahasiswaByProdi(user.IDProdi);
                        break;
                    case 2:
                        KelolaMataKuliah(user.IDProdi);
                        break;
                    case 3:
                        KelolaKelasKuliah(user.IDProdi);
                        break;
                    case 4:
                        AdmProdiCtrl.DaftarProdi(user.IDProdi);
                        break;
                    case 5:
                        AdmProdiCtrl.TambahProdi();
                        break;
                    case 6:
                        Console.WriteLine("Keluar dari menu Admin Prodi.");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != 6);
        }

        static void DaftarMahasiswaByProdi(string idProdi)
        {
            Console.Clear();
            Console.WriteLine($"Daftar Mahasiswa ({idProdi})");
            Console.WriteLine("==============================");
            var mahasiswaInProdi = daftarMahasiswa.Where(m => (m.IDProdi ?? string.Empty).Trim().ToUpper() == idProdi).ToList();
            if (mahasiswaInProdi.Count == 0)
            {
                Console.WriteLine("Tidak ada mahasiswa di prodi ini.");
            }
            else
            {
                int no = 1;
                foreach (var mhs in mahasiswaInProdi)
                {
                    string jk = (mhs.JenisKelamin == 'L') ? "Laki-laki" : "Perempuan";
                    Console.WriteLine($"[{no}] NIM: {mhs.NIM}, Nama: {mhs.NamaMhs}, Prodi: {mhs.IDProdi}");
                    no++;
                }
            }
            Console.WriteLine("==============================");
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }
        static void KelolaMataKuliah(string idProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"\n====== Kelola Mata Kuliah ({idProdi}) ======");
                Console.WriteLine("| 1. Daftar Mata Kuliah                  |");
                Console.WriteLine("| 2. Tambah Mata Kuliah                  |");
                Console.WriteLine("| 3. Ubah Mata Kuliah                    |");
                Console.WriteLine("| 4. Hapus Mata Kuliah                   |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi         |");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");
                while (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Input Anda tidak valid! Masukkan angka sesuai menu.");
                    Console.Write("Pilih menu: ");
                }
                Console.Clear();
                switch (pilihan)
                {
                    case 1:
                        MKCtrl.DaftarMataKuliah();
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        MKCtrl.TambahMataKuliah();
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        MKCtrl.UbahMataKuliah();
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 4:
                        MKCtrl.HapusMataKuliah();
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu Admin Prodi...");
                        break;
                    default:
                        Console.WriteLine("Pilihan Anda tidak valid!");
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                }
            } while (pilihan != 5);
        }
        
        static void KelolaKelasKuliah(string idProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"====== Kelola Kelas Kuliah ({idProdi}) ======");
                Console.WriteLine("| 1. Daftar Kelas Kuliah                 |");
                Console.WriteLine("| 2. Tambah Kelas Kuliah                 |");
                Console.WriteLine("| 3. Ubah Kelas Kuliah                   |");
                Console.WriteLine("| 4. Hapus Kelas Kuliah                  |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi         |");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");
                while (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("Input tidak valid! Masukkan angka sesuai menu.");
                    Console.Write("Pilih menu: ");
                }

                switch (pilihan)
                {
                    case 1:
                        KKCtrl.DaftarKelasKuliah(idProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        KKCtrl.TambahKelasKuliah(idProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        KKCtrl.UbahKelasKuliah(idProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 4:
                        KKCtrl.HapusKelasKuliah(idProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu Admin Prodi...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        Console.WriteLine("Tekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                }
            } while (pilihan != 5);
        }


        static void LoginDosen()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("              Login Dosen         ");
            Console.WriteLine("==================================");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "dosen")
            {
                Console.WriteLine("Akun ini bukan Dosen. Gunakan menu yang sesuai.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            MenuDosen(user);
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }
        static void LoginMahasiswa()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("           Login Mahasiswa        ");
            Console.WriteLine("==================================");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "mahasiswa")
            {
                Console.WriteLine("Akun ini bukan Mahasiswa. Gunakan menu yang sesuai.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            MenuMahasiswa(user);
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }

        
        static void MenuDosen(User user)
        {
            Console.WriteLine($"(Dosen menu) Prodi: {user?.IDProdi}");
        }

        static void MenuMahasiswa(User user)
        {
            Console.WriteLine($"(Mahasiswa menu) NIM: {user?.NIM}, Prodi: {user?.IDProdi}");

        }

    }
}

