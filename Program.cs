using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        static List<Nilai> NilaiSaya = new List<Nilai>();
        static List<User> Users = new List<User>
        {
            new User { Username="admin-univ", Password="123", Role="AdminUniv" },
            new User { Username="admin-prodi", Password="123", Role="AdminProdi", IDProdi="ILKOM" },
            new User { Username="mhs-ilkom-rani", Password="123", Role="Mahasiswa", NIM="240608", IDProdi="ILKOM" },
            new User { Username="dosen-ilkom-kerien", Password="123", Role="Dosen", IDProdi="ILKOM" }
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
            mhsCtrl = new MahasiswaController(daftarMahasiswa, daftarProdi);
            MKCtrl = new MataKuliahController(daftarProdi);
            ProdiCtrl = new ProdiController(daftarMahasiswa, daftarProdi, daftarMataKuliah);
            SemCtrl = new SemesterController(daftarSemester);
            KKCtrl = new KelasKuliahController(daftarKelasKuliah, daftarMataKuliah, daftarProdi, daftarSemester);
            NilaiCtrl = new NilaiController(NilaiSaya, daftarMahasiswa, daftarKelasKuliah, daftarMataKuliah);
            dsnCtrl = new DosenController(daftarDosen, daftarProdi);

            AdmProdiCtrl = new AdminProdiController(daftarAdminProdi, daftarProdi);
            AdmUnivCtrl = new AdminUnivController(daftarAdminUniv, daftarMahasiswa, daftarProdi);

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
                        DaftarMahasiswa(user.IDProdi);
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

        static void DaftarMahasiswa(string idProdi)
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
                foreach (var mhs in mahasiswaInProdi)
                {
                    Console.WriteLine($"NIM: {mhs.NIM}, Nama: {mhs.Nama}, Prodi: {mhs.IDProdi}");
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
                        MKCtrl.DaftarMataKuliah(idProdi);
                        break;
                    case 2:
                        MKCtrl.TambahMataKuliah(idProdi);
                        break;
                    case 3:
                        MKCtrl.UbahMataKuliah(idProdi);
                        break;
                    case 4:
                        MKCtrl.HapusMataKuliah();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu Admin Prodi...");
                        break;
                    default:
                        Console.WriteLine("Pilihan Anda tidak valid!");
                        break;
                }
            } while (pilihan != 5);
        }
        
        static void KelolaKelasKuliah(string idProdi)
        {
                AdmProdiCtrl.KelolaKelasKuliah(idProdi);
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

        static void MenuAdminUniv()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("\n====== Menu Mahasiswa ======");
                Console.WriteLine("| 1. Daftar Mahasiswa      |");
                Console.WriteLine("| 2. Tambah Mahasiswa      |");
                Console.WriteLine("| 3. Ubah Mahasiswa        |");
                Console.WriteLine("| 4. Hapus Mahasiswa       |");
                Console.WriteLine("| 5. Kembali ke Menu Utama |");
                Console.WriteLine("============================");
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
                        DaftarMahasiswa();
                        break;
                    case 2:
                        TambahMahasiswa();
                        break;
                    case 3:
                        UbahMahasiswa();
                        break;
                    case 4:
                        HapusMahasiswa();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu utama...");
                        break;
                    default:
                        Console.WriteLine("Pilihan Anda tidak valid!");
                        break;
                }
            } while (pilihan != 5);
        }

        static void DaftarMahasiswa()
        {
            AdmUnivCtrl.DaftarMahasiswa();
        }
        static void TambahMahasiswa()
        {
            AdmUnivCtrl.TambahMahasiswa();
        }
        static void UbahMahasiswa()
        {
            AdmUnivCtrl.UbahMahasiswa();
        }
        static void HapusMahasiswa()
        {
            AdmUnivCtrl.HapusMahasiswa();
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

