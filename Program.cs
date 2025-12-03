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
        static List<Tagihan> daftarTagihan = new List<Tagihan>();
        static List<User> Users = new List<User>
        {
            new User { Username="admin-univ",  Password="123", Role="AdminUniv" },
            new User { Username="admin-ilkom", Password="123", Role="AdminProdi", IDProdi="11111" },
            new User { Username="admin-bio",   Password="123", Role="AdminProdi", IDProdi="22222" },
            new User { Username="admin-fis", Password="123", Role="AdminProdi", IDProdi="33333" },
            new User { Username="dosen_ilkom", Password="123", Role="Dosen", IDProdi="11111" },
            new User { Username="dosen_bio",   Password="123", Role="Dosen", IDProdi="22222" },
            new User { Username="dosen_fis",   Password="123", Role="Dosen", IDProdi="33333" },
            new User { Username="dosen_dok",   Password="123", Role="Dosen", IDProdi="44444" },
            new User { Username="mhs1",        Password="123", Role="Mahasiswa", NIM="2401010001", IDProdi="11111" },
            new User { Username="mhs2",        Password="123", Role="Mahasiswa", NIM="2401010002", IDProdi="11111" },
            new User { Username="mhs3",        Password="123", Role="Mahasiswa", NIM="2301010003", IDProdi="11111" },
            new User { Username="mhs4",        Password="123", Role="Mahasiswa", NIM="2402020001", IDProdi="22222" },
            new User { Username="mhs5",        Password="123", Role="Mahasiswa", NIM="2402020002", IDProdi="22222" },
            new User { Username="mhs6",        Password="123", Role="Mahasiswa", NIM="2403030001", IDProdi="33333" }
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
        static TagihanController TagihanCtrl;


        static void Main()
        {
            if (daftarProdi.Count == 0)
            {
                daftarProdi.Add(new Prodi { IDProdi = "11111", NamaProdi = "Ilmu Komputer", AliasProdi = "ILKOM" });
                daftarProdi.Add(new Prodi { IDProdi = "22222", NamaProdi = "Biologi", AliasProdi = "BIO" });
                daftarProdi.Add(new Prodi { IDProdi = "33333", NamaProdi = "Fisika", AliasProdi = "FIS" });
                daftarProdi.Add(new Prodi { IDProdi = "44444", NamaProdi = "Kedokteran", AliasProdi = "KED" });
            }
           

            if (daftarSemester.Count == 0)
            {
                daftarSemester.Add(new Semester { IDSemester = "20241", namaSemester = "Ganjil", TahunAjaran = "2024/2025" });
                daftarSemester.Add(new Semester { IDSemester = "20242", namaSemester = "Genap", TahunAjaran = "2024/2025" });
                daftarSemester.Add(new Semester { IDSemester = "20251", namaSemester = "Ganjil", TahunAjaran = "2025/2026" });
            }

            if (daftarMataKuliah.Count == 0)
            {
                // Mata Kuliah Ilmu Komputer
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "ILK001", NamaMK = "Pemrograman Berorientasi Objek", SKS = 3, IDProdi = "11111", Semester = 3, DosenPengampu = "Dr. Budi Santoso" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "ILK002", NamaMK = "Struktur Data", SKS = 4, IDProdi = "11111", Semester = 2, DosenPengampu = "Prof. Ani Wijaya" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "ILK003", NamaMK = "Basis Data", SKS = 3, IDProdi = "11111", Semester = 3, DosenPengampu = "Dr. Citra Dewi" });

                // Mata Kuliah Biologi
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "BIO001", NamaMK = "Biologi Molekuler", SKS = 3, IDProdi = "22222", Semester = 4, DosenPengampu = "Dr. Dedi Hartono" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "BIO002", NamaMK = "Ekologi dan Lingkungan", SKS = 3, IDProdi = "22222", Semester = 3, DosenPengampu = "Prof. Eka Sari" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "BIO003", NamaMK = "Genetika Dasar", SKS = 4, IDProdi = "22222", Semester = 2, DosenPengampu = "Dr. Feri Gunawan" });

                // Mata Kuliah Fisika
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "FIS001", NamaMK = "Fisika Kuantum", SKS = 4, IDProdi = "33333", Semester = 5, DosenPengampu = "Prof. Gita Permana" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "FIS002", NamaMK = "Termodinamika", SKS = 3, IDProdi = "33333", Semester = 4, DosenPengampu = "Dr. Hendra Kusuma" });
                daftarMataKuliah.Add(new MataKuliah { KodeMK = "FIS003", NamaMK = "Mekanika Klasik", SKS = 4, IDProdi = "33333", Semester = 3, DosenPengampu = "Prof. Indah Lestari" });
            }

            if (daftarSemester.Count == 0)
            {
                daftarSemester.Add(new Semester
                {
                    IDSemester = "20241",
                    namaSemester = "Ganjil",
                    TahunAjaran = "2023/2024"
                });
                daftarSemester.Add(new Semester
                {
                    IDSemester = "20242",
                    namaSemester = "Genap",
                    TahunAjaran = "2024/2025"
                });
            }

            if (daftarDosen.Count == 0)
            {
                daftarDosen.Add(new Dosen
                {
                    NIDN = "0123456789",
                    NamaDosen = "Dr. Budi Santoso",
                    IDProdi = "11111",
                    JenisKelamin = "L",
                    JabatanFungsional = "Lektor",
                    MataKuliahDiampu = new List<string> { "ILK001" }
                });

                daftarDosen.Add(new Dosen
                {
                    NIDN = "0123456790",
                    NamaDosen = "Prof. Ani Wijaya",
                    IDProdi = "11111",
                    JenisKelamin = "P",
                    JabatanFungsional = "Guru Besar",
                    MataKuliahDiampu = new List<string> { "ILK002" }
                });

                daftarDosen.Add(new Dosen
                {
                    NIDN = "0123456791",
                    NamaDosen = "Dr. Citra Dewi",
                    IDProdi = "11111",
                    JenisKelamin = "P",
                    JabatanFungsional = "Lektor Kepala",
                    MataKuliahDiampu = new List<string> { "ILK003" }
                });

                daftarDosen.Add(new Dosen
                {
                    NIDN = "0223456789",
                    NamaDosen = "Dr. Dedi Hartono",
                    IDProdi = "22222",
                    JenisKelamin = "L",
                    JabatanFungsional = "Lektor",
                    MataKuliahDiampu = new List<string> { "BIO001" }
                });

                daftarDosen.Add(new Dosen
                {
                    NIDN = "0323456789",
                    NamaDosen = "Prof. Gita Permana",
                    IDProdi = "33333",
                    JenisKelamin = "P",
                    JabatanFungsional = "Guru Besar",
                    MataKuliahDiampu = new List<string> { "FIS001" }
                });
            }

            if (daftarMahasiswa.Count == 0)
            {
                // Mahasiswa Ilmu Komputer
                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2401010001",
                    NamaMhs = "AHMAD RIZKI PRASETYO",
                    AlamatMhs = "Jl. Merdeka No. 10, Jakarta",
                    JenisKelamin = 'L',
                    IDProdi = "11111",
                    Angkatan = 2024,
                    TanggalLahir = new DateTime(2006, 5, 15),
                    SemesterAktif = "20241"
                });

                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2401010002",
                    NamaMhs = "SITI NURHALIZA",
                    AlamatMhs = "Jl. Sudirman No. 25, Bandung",
                    JenisKelamin = 'P',
                    IDProdi = "11111",
                    Angkatan = 2024,
                    TanggalLahir = new DateTime(2006, 8, 20),
                    SemesterAktif = "20241"
                });

                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2301010003",
                    NamaMhs = "BUDI SETIAWAN",
                    AlamatMhs = "Jl. Gatot Subroto No. 5, Surabaya",
                    JenisKelamin = 'L',
                    IDProdi = "11111",
                    Angkatan = 2023,
                    TanggalLahir = new DateTime(2005, 3, 10),
                    SemesterAktif = "20241"
                });

                // Mahasiswa Biologi
                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2402020001",
                    NamaMhs = "DEWI KUSUMA WARDHANI",
                    AlamatMhs = "Jl. Diponegoro No. 15, Yogyakarta",
                    JenisKelamin = 'P',
                    IDProdi = "22222",
                    Angkatan = 2024,
                    TanggalLahir = new DateTime(2006, 11, 5),
                    SemesterAktif = "20241"
                });

                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2402020002",
                    NamaMhs = "FIKRI HAMDANI",
                    AlamatMhs = "Jl. Ahmad Yani No. 8, Semarang",
                    JenisKelamin = 'L',
                    IDProdi = "22222",
                    Angkatan = 2024,
                    TanggalLahir = new DateTime(2006, 2, 28),
                    SemesterAktif = "20241"
                });

                // Mahasiswa Fisika
                daftarMahasiswa.Add(new Mahasiswa
                {
                    NIM = "2403030001",
                    NamaMhs = "GALIH PRATAMA",
                    AlamatMhs = "Jl. Thamrin No. 12, Medan",
                    JenisKelamin = 'L',
                    IDProdi = "33333",
                    Angkatan = 2024,
                    TanggalLahir = new DateTime(2006, 7, 18),
                    SemesterAktif = "20241"
                });
            }

            if (daftarKelasKuliah.Count == 0)
            {
                // Kelas Ilmu Komputer
                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "ILK001-A",
                    KodeMK = "ILK001",
                    IDProdi = "11111",
                    IDSemester = "20241",
                    Ruangan = "R301",
                    NamaKelas = "PBO Kelas A",
                    KapasitasKelas = 40,
                    JumlahPeserta = 2,
                    DosenPengampu = "dosen_ilkom"
                });

                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "ILK002-A",
                    KodeMK = "ILK002",
                    IDProdi = "11111",
                    IDSemester = "20241",
                    Ruangan = "R302",
                    NamaKelas = "Strukdat Kelas A",
                    KapasitasKelas = 35,
                    JumlahPeserta = 1,
                    DosenPengampu = "dosen_ilkom"
                });

                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "ILK003-B",
                    KodeMK = "ILK003",
                    IDProdi = "11111",
                    IDSemester = "20241",
                    Ruangan = "LAB-DB",
                    NamaKelas = "Basis Data Kelas B",
                    KapasitasKelas = 30,
                    JumlahPeserta = 0,
                    DosenPengampu = "dosen_ilkom"
                });

                // Kelas Biologi
                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "BIO001-A",
                    KodeMK = "BIO001",
                    IDProdi = "22222",
                    IDSemester = "20241",
                    Ruangan = "LAB-BIO1",
                    NamaKelas = "Biomol Kelas A",
                    KapasitasKelas = 25,
                    JumlahPeserta = 1,
                    DosenPengampu = "dosen_bio"
                });

                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "BIO002-A",
                    KodeMK = "BIO002",
                    IDProdi = "22222",
                    IDSemester = "20241",
                    Ruangan = "R201",
                    NamaKelas = "Ekologi Kelas A",
                    KapasitasKelas = 30,
                    JumlahPeserta = 1,
                    DosenPengampu = "dosen_bio"
                });

                // Kelas Fisika
                daftarKelasKuliah.Add(new KelasKuliah
                {
                    KodeKelas = "FIS001-A",
                    KodeMK = "FIS001",
                    IDProdi = "33333",
                    IDSemester = "20241",
                    Ruangan = "LAB-FIS",
                    NamaKelas = "Fisika Kuantum A",
                    KapasitasKelas = 30,
                    JumlahPeserta = 1,
                    DosenPengampu = "dosen_fis"
                });
            }

            if (daftarNilai.Count == 0)
            {
                // Nilai untuk mahasiswa Ilmu Komputer
                daftarNilai.Add(new Nilai
                {
                    NIM = "2401010001",
                    KodeKelas = "ILK001-A",
                    NilaiTugas = 85,
                    NilaiUTS = 80,
                    NilaiUAS = 88,
                    NilaiSoftSkill = 90,
                    NilaiAkhir = 85.45,
                    HurufMutu = "B+",
                    AngkaMutu = 3.25
                });

                daftarNilai.Add(new Nilai
                {
                    NIM = "2401010002",
                    KodeKelas = "ILK001-A",
                    NilaiTugas = 90,
                    NilaiUTS = 92,
                    NilaiUAS = 95,
                    NilaiSoftSkill = 88,
                    NilaiAkhir = 92.2,
                    HurufMutu = "A-",
                    AngkaMutu = 3.75
                });

                daftarNilai.Add(new Nilai
                {
                    NIM = "2301010003",
                    KodeKelas = "ILK002-A",
                    NilaiTugas = 78,
                    NilaiUTS = 75,
                    NilaiUAS = 80,
                    NilaiSoftSkill = 85,
                    NilaiAkhir = 78.7,
                    HurufMutu = "B-",
                    AngkaMutu = 2.75
                });

                // Nilai untuk mahasiswa Biologi
                daftarNilai.Add(new Nilai
                {
                    NIM = "2402020001",
                    KodeKelas = "BIO001-A",
                    NilaiTugas = 88,
                    NilaiUTS = 85,
                    NilaiUAS = 90,
                    NilaiSoftSkill = 92,
                    NilaiAkhir = 88.15,
                    HurufMutu = "B+",
                    AngkaMutu = 3.25
                });

                daftarNilai.Add(new Nilai
                {
                    NIM = "2402020002",
                    KodeKelas = "BIO002-A",
                    NilaiTugas = 82,
                    NilaiUTS = 80,
                    NilaiUAS = 85,
                    NilaiSoftSkill = 80,
                    NilaiAkhir = 82.05,
                    HurufMutu = "B",
                    AngkaMutu = 3.0
                });

                // Nilai untuk mahasiswa Fisika
                daftarNilai.Add(new Nilai
                {
                    NIM = "2403030001",
                    KodeKelas = "FIS001-A",
                    NilaiTugas = 95,
                    NilaiUTS = 92,
                    NilaiUAS = 96,
                    NilaiSoftSkill = 94,
                    NilaiAkhir = 94.05,
                    HurufMutu = "A-",
                    AngkaMutu = 3.75
                });
            }

            mhsCtrl = new MahasiswaController(daftarMahasiswa, daftarProdi, daftarNilai, daftarKelasKuliah, daftarMataKuliah, daftarTagihan);
            MKCtrl = new MataKuliahController(daftarProdi);
            ProdiCtrl = new ProdiController(daftarMahasiswa, daftarProdi, daftarMataKuliah);
            SemCtrl = new SemesterController(daftarSemester);
            KKCtrl = new KelasKuliahController(daftarKelasKuliah, daftarMataKuliah, daftarProdi, daftarSemester, daftarDosen);
            NilaiCtrl = new NilaiController(daftarNilai, daftarMahasiswa, daftarKelasKuliah, daftarMataKuliah);
            dsnCtrl = new DosenController(daftarDosen, daftarProdi, daftarMataKuliah, daftarNilai, daftarKelasKuliah, daftarMahasiswa);
            TagihanCtrl = new TagihanController(daftarMahasiswa, daftarTagihan, daftarSemester);
            AdmProdiCtrl = new AdminProdiController(daftarAdminProdi, daftarProdi, daftarKelasKuliah, daftarMataKuliah, daftarSemester);
            AdmUnivCtrl = new AdminUnivController(daftarAdminUniv, daftarMahasiswa, daftarProdi, daftarNilai);

            int login;
            do
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("     Sistem Akademik Universitas    ");
                Console.WriteLine("====================================");
                Console.WriteLine("|[1] Login sebagai Admin Universitas|");
                Console.WriteLine("|[2] Login sebagai Admin Prodi      |");
                Console.WriteLine("|[3] Login sebagai Dosen            |");
                Console.WriteLine("|[4] Login sebagai Mahasiswa        |");
                Console.WriteLine("|[5] Keluar                         |");
                Console.WriteLine("====================================");
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
            Console.WriteLine("             Login User           ");
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
                Console.WriteLine("|1. Manajemen Prodi                    |");
                Console.WriteLine("|2. Manajemen Mahasiswa                |");
                Console.WriteLine("|3. Logout                             |");
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

        //case 1 adm univ
        static void MenuUnivProdi()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("======== MANAJEMEN PRODI ========");
                Console.WriteLine("|1. Daftar Prodi                 |");
                Console.WriteLine("|2. Tambah Prodi                 |");
                Console.WriteLine("|3. Manajemen Tagihan Mahasiswa  |");
                Console.WriteLine("|4. Kembali ke Menu Admin Univ   |");
                Console.WriteLine("=================================");
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
                        ProdiCtrl.DaftarProdi();
                        break;
                    case 2:
                        ProdiCtrl.TambahProdi();
                        break;
                    case 3:
                        TagihanCtrl.MenuAdminTagihan();
                        break;
                    case 4:
                        Console.WriteLine("Kembali ke Menu Admin Univ...");
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilihan != 4);
        }

        //case 2 adm univ
        static void MenuMahasiswaAdmin()
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine("======== MANAJEMEN MAHASISWA ========");
                Console.WriteLine("|1. Daftar Mahasiswa               |");
                Console.WriteLine("|2. Tambah Mahasiswa               |");
                Console.WriteLine("|3. Ubah Mahasiswa                 |");
                Console.WriteLine("|4. Hapus Mahasiswa                |");
                Console.WriteLine("|5. Kelola Tagihan Registrasi      |");
                Console.WriteLine("|6. Kembali ke Menu Admin Univ     |");
                Console.WriteLine("=====================================");
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
                        TagihanCtrl.MenuAdminTagihan();
                        Console.WriteLine("\nTekan ENTER untuk kembali...");
                        Console.ReadLine();
                        break;
                    case 6:
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
                Console.WriteLine("\n Username atau password salah. Silakan coba lagi.");
                Console.WriteLine("\nTekan Enter untuk melanjutkan...");
                Console.ReadLine();
                return;
            }

            string roleNormalized = (user.Role ?? string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).ToLowerInvariant();
            if (roleNormalized != "adminprodi")
            {
                Console.WriteLine("\n Akun ini bukan Admin Prodi. Gunakan menu yang sesuai.");
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
            MenuAdminProdi(user);
            
        }


        static void MenuAdminProdi(User user)
        {
            string idProdi = (user.IDProdi ?? string.Empty).Trim().ToUpper();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine($"     Menu Admin Prodi ({user.IDProdi})  ");
                Console.WriteLine("=========================================");
                Console.WriteLine("|[1] Lihat Daftar Mahasiswa             |");
                Console.WriteLine("|[2] Kelola Mata Kuliah                 |");
                Console.WriteLine("|[3] Kelola Kelas Kuliah                |");
                Console.WriteLine("|[4] Kelola Semester                    |");
                Console.WriteLine("|[5] Lihat Daftar Prodi                 |");
                Console.WriteLine("|[6] Keluar                             |");
                Console.WriteLine("=========================================");
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
                        Semester semester = new Semester();
                        SemCtrl.KelolaSemester(semester, user.IDProdi);
                        break;
                    case 5:
                        AdmProdiCtrl.DaftarProdi(user.IDProdi);
                        Console.WriteLine("\nTekan ENTER untuk kembali...");
                        Console.ReadLine();
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

        //case 1 adm prodi
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
                Console.WriteLine("No | NIM        | Nama                   | Prodi | JK | Angkatan");
                Console.WriteLine("---+------------+------------------------+-------+----+---------");

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

        //case 2 adm prodi
        static void KelolaMataKuliah(string IDProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"\n====== Kelola Mata Kuliah ({IDProdi}) ======");
                Console.WriteLine("| 1. Daftar Mata Kuliah                       |");
                Console.WriteLine("| 2. Tambah Mata Kuliah                       |");
                Console.WriteLine("| 3. Ubah Mata Kuliah                         |");
                Console.WriteLine("| 4. Hapus Mata Kuliah                        |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi              |");
                Console.WriteLine("==============================================");
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

        //case 3 adm prodi
        static void KelolaKelasKuliah(string IDProdi)
        {
            int pilihan;
            do
            {
                Console.Clear();
                Console.WriteLine($"====== Kelola Kelas Kuliah ({IDProdi}) ======");
                Console.WriteLine("| 1. Daftar Kelas Kuliah                     |");
                Console.WriteLine("| 2. Tambah Kelas Kuliah                     |");
                Console.WriteLine("| 3. Ubah Kelas Kuliah                       |");
                Console.WriteLine("| 4. Hapus Kelas Kuliah                      |");
                Console.WriteLine("| 5. Kembali ke Menu Admin Prodi             |");
                Console.WriteLine("==============================================");
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

        public void KelolaSemester(Semester semester, string IDProdi)
        {

            bool selesai = false;

            while (!selesai)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║       MENU KELOLA SEMESTER             ║");
                Console.WriteLine("╠════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Tambah Semester Baru                ║");
                Console.WriteLine("║ 2. Lihat Semua Semester                ║");
                Console.WriteLine("║ 3. Cari Semester by Kode               ║");
                Console.WriteLine("║ 4. Update Semester                     ║");
                Console.WriteLine("║ 5. Hapus Semester                      ║");
                Console.WriteLine("║ 0. Kembali ke Menu Utama               ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.Write("Pilih menu: ");

                string pilihan = Console.ReadLine();

                switch (pilihan)
                {
                    case "1":
                        SemCtrl.TambahSemester(IDProdi);
                        break;
                    case "2":
                        SemCtrl.LihatSemuaSemester(IDProdi);
                        break;
                    case "3":
                        SemCtrl.CariSemesterByID(IDProdi);
                        break;
                    case "4":
                        SemCtrl.UpdateSemester(IDProdi);
                        break;
                    case "5":
                        SemCtrl.HapusSemester(IDProdi);
                        break;
                    case "0":
                        selesai = true;
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid!");
                        Console.ReadKey();
                        break;
                }
            }
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
                Console.WriteLine("============== MENU DOSEN =================");
                Console.WriteLine($" Dosen Prodi:  {username}  ||  {IDProdi}  |");
                Console.WriteLine("|1. Lihat Kelas yang Diampu                |");
                Console.WriteLine("|2. Input/Ubah Nilai Mahasiswa             |");
                Console.WriteLine("|3. Keluar                                 |");
                Console.WriteLine("===========================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilih))
                {
                    Console.WriteLine("\n Input tidak valid!");
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
                Console.WriteLine($" Mahasiswa NIM:  {NIM} ||  {IDProdi}  |");
                Console.WriteLine("|1. KRS                                |");
                Console.WriteLine("|2. KHS                                |");
                Console.WriteLine("|3. Lihat & Bayar Tagihan              |");
                Console.WriteLine("|4. Keluar                             |");
                Console.WriteLine("=======================================");
                Console.Write("Pilih menu: ");
                if (!int.TryParse(Console.ReadLine(), out pilih))
                {
                    Console.WriteLine("\n Input tidak valid!");
                    Thread.Sleep(1000);
                    pilih = 0;
                    continue;
                }
                switch (pilih)
                {
                    case 1:
                        mhsCtrl.MenuKRS(user.NIM, IDProdi);
                        Console.ReadLine();
                        break;
                    case 2:
                        mhsCtrl.MenuKHS(user.NIM, IDProdi);
                        Console.ReadLine();
                        break;
                    case 3:
                        mhsCtrl.LihatTagihan(user.NIM);
                        
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("Logout...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        Thread.Sleep(1000);
                        break;
                }
            } while (pilih != 4);

        }

    }
}