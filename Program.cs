using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            new User { Username="dosen_ilkom", Password="123", Role="Dosen", IDProdi="11111" },
            new User { Username="dosen_bio",   Password="123", Role="Dosen", IDProdi="22222" },
            new User { Username="dosen_fis",   Password="123", Role="Dosen", IDProdi="33333" },
            new User { Username="dosen_dok",   Password="123", Role="Dosen", IDProdi="44444" },
            new User { Username="mhs", Password="123", Role="Mahasiswa", NIM="240608", IDProdi="ILKOM" }
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
                daftarProdi.Add(new Prodi { IDProdi = "11111", NamaProdi = "Ilmu Komputer", AliasProdi = "ILKOM" });
                daftarProdi.Add(new Prodi { IDProdi = "22222", NamaProdi = "Biologi", AliasProdi = "BIO" });
                daftarProdi.Add(new Prodi { IDProdi = "33333", NamaProdi = "Fisika", AliasProdi = "FIS" });
                daftarProdi.Add(new Prodi { IDProdi = "44444", NamaProdi = "Kedokteran", AliasProdi = "KED" });
            }

            mhsCtrl = new MahasiswaController(daftarMahasiswa, daftarProdi, daftarNilai, daftarKelasKuliah, daftarMataKuliah);
            MKCtrl = new MataKuliahController(daftarProdi);
            ProdiCtrl = new ProdiController(daftarMahasiswa, daftarProdi, daftarMataKuliah);
            SemCtrl = new SemesterController(daftarSemester);
            KKCtrl = new KelasKuliahController(daftarKelasKuliah, daftarMataKuliah, daftarProdi, daftarSemester, daftarDosen);
            NilaiCtrl = new NilaiController(daftarNilai, daftarMahasiswa, daftarKelasKuliah, daftarMataKuliah);
            dsnCtrl = new DosenController(daftarDosen, daftarProdi, daftarMataKuliah, daftarNilai, daftarKelasKuliah, daftarMahasiswa);

            AdmProdiCtrl = new AdminProdiController(daftarAdminProdi, daftarProdi, daftarKelasKuliah, daftarMataKuliah, daftarSemester);
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
                    login = 0;
                    continue;
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
                        Thread.Sleep(1500);
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
                Console.WriteLine("\nUsername atau password salah.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? "").Replace("-", "").Replace(" ", "").ToLower();

            if (roleNormalized != "adminuniv")
            {
                Console.WriteLine("\nAkun ini bukan Admin Universitas.");
                Console.WriteLine("Tekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            Thread.Sleep(1500);
            Console.Clear(); 
            MenuAdminUniv();
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
                Console.WriteLine("========================================");
                Console.Write("Pilih menu: ");

                if (!int.TryParse(Console.ReadLine(), out pilih))
                {
                    Console.WriteLine("\nInput tidak valid!");
                    Thread.Sleep(1000);
                    continue;
                }

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
                        Thread.Sleep(1000);
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }

                Console.Clear();  
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
                {
                    Console.WriteLine("\n❌ Input tidak valid!");
                    Thread.Sleep(1000);
                    pilihan = 0;
                    continue;
                }

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
                        Thread.Sleep(1000);
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
                {
                    Console.WriteLine("\n❌ Input tidak valid!");
                    Thread.Sleep(1000);
                    pilihan = 0;
                    continue;
                }

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
                        Console.WriteLine("\nTekan ENTER untuk kembali...");
                        Console.ReadLine();
                        break;
                    case 4:
                        AdmUnivCtrl.HapusMahasiswa();
                        Console.WriteLine("\nTekan ENTER untuk kembali...");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke Menu Admin Univ...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilihan != 5);
        }

        static void LoginAdmProdi()
        {
           
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
                Console.WriteLine("\n❌ Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "adminprodi")
            {
                Console.WriteLine("\n❌ Akun ini bukan Admin Prodi. Gunakan menu yang sesuai.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
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
                Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].IDProdi})");
            }
            Console.WriteLine("==================================");

            int pilihanProdi;
            do
            {
                Console.Write("Pilih prodi (masukkan nomor): ");
                if (!int.TryParse(Console.ReadLine(), out pilihanProdi) || pilihanProdi < 1 || pilihanProdi > daftarProdi.Count)
                {
                    Console.WriteLine("Pilihan tidak valid!\n");
                    pilihanProdi = 0;
                }
            } while (pilihanProdi == 0);

            user.IDProdi = daftarProdi[pilihanProdi - 1].IDProdi;

            Console.WriteLine($"\nLogin berhasil! Selamat datang, {user.Role}.");
            Thread.Sleep(1500);
            // pass user so MenuAdminProdi can restrict to user.IDProdi
            MenuAdminProdi(user);
            
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
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\n Input tidak valid!");
                    Thread.Sleep(1000);
                    choice = 0;
                    continue;
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
                        Console.WriteLine("\nTekan ENTER untuk kembali...");
                        Console.ReadLine();
                        break;
                    case 5:
                        AdmProdiCtrl.TambahProdi();
                        break;
                    case 6:
                        Console.WriteLine("\nKeluar dari menu Admin Prodi.");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("\nPilihan tidak valid. Tekan Enter untuk melanjutkan...");
                        Thread.Sleep(1000);
                        
                        break;
                }
            } while (choice != 6);
        }

        static void DaftarMahasiswaByProdi(string IDProdi)
        {
            Console.Clear();
            Console.WriteLine($"Daftar Mahasiswa ({IDProdi})");
            Console.WriteLine("==============================");
            var mahasiswaInProdi = daftarMahasiswa.Where(m => (m.IDProdi ?? string.Empty).Trim().ToUpper() == IDProdi).ToList();
            if (mahasiswaInProdi.Count == 0)
            {
                Console.WriteLine("Tidak ada mahasiswa di prodi ini.");
            }
            else
            {
                Console.WriteLine("No | NIM        | Nama                    | Prodi | JK | Angkatan");
                Console.WriteLine("---+------------+-------------------------+-------+----+---------");

                int no = 1;
                foreach (var mhs in mahasiswaInProdi)
                {
                    string jk = (mhs.JenisKelamin == 'L') ? "Laki-laki" : "Perempuan";
                    Console.WriteLine($"[{no, 2}] NIM: {mhs.NIM, -10}, Nama: {mhs.NamaMhs, -23}, Prodi: {mhs.IDProdi, -5}, JK: {jk, -10}, Angkatan: {mhs.Angkatan}");
                    no++;
                }
                Console.WriteLine($"\nTotal: {mahasiswaInProdi.Count} mahasiswa");
            }
            
            Console.WriteLine("\nTekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }
        static void KelolaMataKuliah(string IDProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"\n====== Kelola Mata Kuliah ({IDProdi}) ======");
                Console.WriteLine("| 1. Daftar Mata Kuliah                  |");
                Console.WriteLine("| 2. Tambah Mata Kuliah                  |");
                Console.WriteLine("| 3. Ubah Mata Kuliah                    |");
                Console.WriteLine("| 4. Hapus Mata Kuliah                   |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi         |");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");
                if(!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("\n Input tidak valid!");
                    Thread.Sleep(1000);
                    pilihan = 0;
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        MKCtrl.DaftarMataKuliah();
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        MKCtrl.TambahMataKuliah();
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        MKCtrl.UbahMataKuliah();
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 4:
                        MKCtrl.HapusMataKuliah();
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu Admin Prodi...");
                        break;
                    default:
                        Console.WriteLine("Pilihan Anda tidak valid!");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilihan != 5);
        }
        
        static void KelolaKelasKuliah(string IDProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"====== Kelola Kelas Kuliah ({IDProdi}) ======");
                Console.WriteLine("| 1. Daftar Kelas Kuliah                 |");
                Console.WriteLine("| 2. Tambah Kelas Kuliah                 |");
                Console.WriteLine("| 3. Ubah Kelas Kuliah                   |");
                Console.WriteLine("| 4. Hapus Kelas Kuliah                  |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi         |");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilihan))
                {
                    Console.WriteLine("\n Input tidak valid!");
                    Thread.Sleep(1000);
                    pilihan = 0;
                    continue;
                }

                switch (pilihan)
                {
                    case 1:
                        KKCtrl.DaftarKelasKuliah(IDProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        KKCtrl.TambahKelasKuliah(IDProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        KKCtrl.UbahKelasKuliah(IDProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 4:
                        KKCtrl.HapusKelasKuliah(IDProdi);
                        Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Kembali ke menu Admin Prodi...");
                        break;
                    default:
                        Console.WriteLine("\nPilihan tidak valid!");
                        Thread.Sleep(1000);
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
                Console.WriteLine("\nUsername atau password salah. Silakan coba lagi.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "dosen")
            {
                Console.WriteLine("\nAkun ini bukan Dosen. Gunakan menu yang sesuai.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
            Thread.Sleep(1500);
            MenuDosen(user);
            
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
                Console.WriteLine("\nUsername atau password salah. Silakan coba lagi.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "mahasiswa")
            {
                Console.WriteLine("\nAkun ini bukan Mahasiswa. Gunakan menu yang sesuai.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }
            Console.WriteLine($"\nLogin berhasil! Selamat datang, {user.Role}.");
            Thread.Sleep(1500);
            MenuMahasiswa(user);
            
        }


        static void MenuDosen(User user)
        {
            string username = (user.Username ?? string.Empty).Trim().ToUpper();
            string IDProdi = (user.IDProdi ?? string.Empty).Trim().ToUpper();
            int pilih = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("============== MENU DOSEN ================");
                Console.WriteLine($"Dosen Prodi: {username}  ||  {IDProdi}   ");
                Console.WriteLine("1. Lihat Kelas yang Diampu                ");
                Console.WriteLine("2. Input/Ubah Nilai Mahasiswa             ");
                Console.WriteLine("3. Keluar                                 ");
                Console.WriteLine("==========================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilih))
                {
                    Console.WriteLine("\n❌ Input tidak valid!");
                    Thread.Sleep(1000);
                    pilih = 0;
                    continue;
                }
                switch (pilih)
                {
                    case 1:
                        dsnCtrl.DaftarKelasKuliahDosen(user.Username, user.IDProdi);  
                        Console.WriteLine("\nTekan ENTER untuk melanjutkan..."); 
                        Console.ReadLine();
                        break;
                    case 2:
                        dsnCtrl.InputNilaiMahasiswa(user.Username, user.IDProdi);
                        Thread.Sleep(1000);
                        break;
                    case 3:
                        Console.WriteLine("Logout...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilih != 3);
        }

        static void MenuMahasiswa(User user)
        {
            string NIM = (user.NIM ?? string.Empty).Trim().ToUpper();
            string IDProdi = (user.IDProdi ?? string.Empty).Trim().ToUpper();

            int pilih = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("=========== MENU MAHASISWA ============");
                Console.WriteLine($"Mahasiswa NIM: {NIM} || {IDProdi}     ");
                Console.WriteLine("1. Lihat Kelas yang Diikuti            ");
                Console.WriteLine("2. Lihat Nilai Mata Kuliah             ");
                Console.WriteLine("3. Keluar");
                Console.WriteLine("=======================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilih))
                {
                    Console.WriteLine("\n❌ Input tidak valid!");
                    Thread.Sleep(1000);
                    pilih = 0;
                    continue;
                }
                switch (pilih)
                {
                    case 1:
                        mhsCtrl.MenuKRS(user.NIM, IDProdi);
                        Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 2:
                        mhsCtrl.MenuKHS(user.NIM, IDProdi);
                        Console.WriteLine("\nTekan ENTER untuk melanjutkan...");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("Logout...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilih != 3);

        }

    }
}