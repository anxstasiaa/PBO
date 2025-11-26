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
            new User { Username="admin-ilkom", Password="123", Role="AdminProdi", IDProdi="ILKOM" },
            new User { Username="mhs-ilkom-rani", Password="123", Role="Mahasiswa", NIM="2406", IDProdi="ILKOM" },
            new User { Username="dosen-ilkom-kerien", Password="123", Role="Dosen", IDProdi="ILKOM" }
        };

        static MahasiswaController mhsCtrl;
        static DosenController dsnCtrl;
        static ProdiController ProdiCtrl;
        static AdminProdiController AdmUCtrl;
        static AdminUnivController AdmPCtrl;
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
            AdminProdiController AdmPCtrl = new AdminProdiController(daftarAdminProdi, daftarProdi);
            AdminUnivController AdmUCtrl = new AdminUnivController(daftarAdminUniv);

            int login;
            do
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("     Sistem Akademik Universitas   ");
                Console.WriteLine("===================================");
                Console.WriteLine("[1] Login                          ");
                Console.WriteLine("[2] Keluar                         ");
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
                        PilihUser();
                        break;
                    case 2:
                        Console.WriteLine("Terima kasih telah menggunakan sistem ini.");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                        break;
                }
            } while (login != 2);
        }

        static void PilihUser()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("             Login User            ");
            Console.WriteLine("===================================");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            User user = Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                Console.WriteLine($"Login berhasil! Selamat datang, {user.Role}.");
                
            }
            else
            {
                Console.WriteLine("Username atau password salah. Silakan coba lagi.");
            }
            Console.WriteLine("Tekan Enter untuk melanjutkan...");
            Console.ReadLine();

           //tes terus
        }
    }

    }

