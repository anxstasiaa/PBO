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
        static List<Nilai> daftarNilai = new List<Nilai>();
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();

        public MahasiswaController(List<Mahasiswa> mhs, List<Prodi> prodi, List<Nilai> nilai, List<KelasKuliah> kelasKuliah, List<MataKuliah> mataKuliah)
        {
            daftarMahasiswa = mhs;
            daftarProdi = prodi;
            daftarNilai = nilai;
            daftarKelasKuliah = kelasKuliah;
            daftarMataKuliah = mataKuliah;
        }

        public void MenuKRS(string nim, string idProdi)
        {
            Console.Clear();
            Console.WriteLine("===== KRS - Kartu Rencana Studi =====");

            // Cari semester aktif
            string semesterAktif = "20241"; // bisa kamu ubah pakai SemesterController nanti

            // Ambil semua kelas dari prodi mahasiswa
            var kelasProdi = daftarKelasKuliah
                .Where(k => k.IDProdi == idProdi && k.IDSemester == semesterAktif)
                .ToList();

            if (kelasProdi.Count == 0)
            {
                Console.WriteLine("Tidak ada kelas ditawarkan untuk semester ini.");
                Console.ReadLine();
                return;
            }

            int no = 1;
            foreach (var k in kelasProdi)
            {
                var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == k.KodeMK);
                string namaMK = mk?.NamaMK ?? "(Nama MK tidak ditemukan)";
                int sks = mk?.SKS ?? 0;

                Console.WriteLine($"{no}. {k.KodeKelas} | {k.KodeMK} - {namaMK} ({sks} SKS)");
                Console.WriteLine($"   Dosen : {k.DosenPengampu}");
                Console.WriteLine($"   Kapasitas : {k.KapasitasKelas}, Terisi : {k.JumlahPeserta}");
                no++;
            }

            Console.Write("\nAmbil kelas nomor berapa? (0 untuk batal): ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 1 || pilih > kelasProdi.Count)
                return;

            var kelasDipilih = kelasProdi[pilih - 1];

            // VALIDASI KAPASITAS
            if (kelasDipilih.JumlahPeserta >= kelasDipilih.KapasitasKelas)
            {
                Console.WriteLine("\n❌ Kelas penuh! Tidak bisa mengambil.");
                Console.ReadLine();
                return;
            }

            // CEK SUDAH AMBIL BELUM?
            if (daftarNilai.Any(n => n.NIM == nim && n.KodeKelas == kelasDipilih.KodeKelas))
            {
                Console.WriteLine("\n Kamu sudah mengambil kelas ini sebelumnya!");
                Console.ReadLine();
                return;
            }

            // BUAT RECORD NILAI BARU
            daftarNilai.Add(new Nilai
            {
                NIM = nim,
                KodeKelas = kelasDipilih.KodeKelas,
                AngkaMutu = null,
                HurufMutu = null
            });


            // UPDATE JUMLAH PESERTA
            kelasDipilih.JumlahPeserta++;

            Console.WriteLine("\n✅ Kelas berhasil diambil!");
            Console.ReadLine();
        }

        public void MenuKHS(string nim, string IDProdi)
        {
            Console.Clear();
            Console.WriteLine("===== KHS - Kartu Hasil Studi =====");
            Console.Write("Masukkan semester (misal 20241): ");
            string sem = Console.ReadLine()?.Trim();

            var nilaiMhs = daftarNilai
                .Where(n => n.NIM == nim)
                .Where(n =>
                {
                    var kelas = daftarKelasKuliah.FirstOrDefault(k => k.KodeKelas == n.KodeKelas);
                    return kelas != null && kelas.IDSemester == sem;
                })
                .ToList();

            if (nilaiMhs.Count == 0)
            {
                Console.WriteLine("Tidak ada KHS untuk semester ini.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nMK  |  Nama Mata Kuliah          | SKS | NH | NA");

            foreach (var n in nilaiMhs)
            {
                var kelas = daftarKelasKuliah.First(k => k.KodeKelas == n.KodeKelas);
                var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kelas.KodeMK);

                Console.WriteLine($"{mk.KodeMK,-4}| {mk.NamaMK,-27} | {mk.SKS,3} | {n.HurufMutu ?? "-"} | {n.AngkaMutu?.ToString() ?? "-"}");
            }

            Console.ReadLine();
        }


        //public void TambahMahasiswa()
        //{
        //    Console.Clear();
        //    Console.WriteLine("===== Tambah Mahasiswa =====");

        //    Mahasiswa mhs = new Mahasiswa();

        //    string nim;
        //    do
        //    {
        //        Console.Write("Masukkan NIM (10 digit angka): ");
        //        nim = Console.ReadLine()?.Trim();

        //        if (string.IsNullOrWhiteSpace(nim) || nim.Length != 10 || !nim.All(char.IsDigit))
        //        {
        //            Console.WriteLine("NIM harus 10 digit angka.");

        //            nim = "";
        //        }
        //        else if (daftarMahasiswa.Any(a => a.NIM == nim))
        //        {
        //            Console.WriteLine("NIM sudah terdaftar.");

        //            nim = "";
        //        }
        //    } while (nim == "");

        //    mhs.NIM = nim;

        //    string nama;
        //    do
        //    {
        //        Console.Write("Masukkan Nama (huruf kapital, minimal 5 huruf): ");
        //        nama = Console.ReadLine()?.Trim();

        //        if (string.IsNullOrWhiteSpace(nama) || nama.Length < 5)
        //        {
        //            Console.WriteLine("Nama minimal 5 huruf.");

        //            nama = "";
        //        }
        //        else if (!nama.All(c => c == ' ' || (c >= 'A' && c <= 'Z')))
        //        {
        //            Console.WriteLine("Nama harus huruf kapital.");

        //            nama = "";
        //        }
        //    } while (nama == "");

        //    mhs.NamaMhs = nama;

        //    Console.Write("Masukkan Alamat: ");
        //    mhs.AlamatMhs = Console.ReadLine();

        //    char jk;
        //    do
        //    {
        //        Console.Write("Jenis Kelamin (L/P): ");
        //        string input = Console.ReadLine()?.Trim().ToUpper();
        //        jk = string.IsNullOrWhiteSpace(input) ? '\0' : input[0];

        //        if (jk != 'L' && jk != 'P')
        //        {
        //            Console.WriteLine("Masukkan L atau P. Coba lagi. \n");

        //        }
        //    } while (jk != 'L' && jk != 'P');

        //    mhs.JenisKelamin = jk;

        //    Prodi selectedProdi = null;
        //    do
        //    {
        //        Console.WriteLine("\n--- Daftar Prodi ---");
        //        for (int i = 0; i < daftarProdi.Count; i++)
        //        {
        //            Console.WriteLine($"[{i + 1}] {daftarProdi[i].NamaProdi} ({daftarProdi[i].KodeProdi})");
        //        }

        //        Console.Write("Pilih Prodi (nomor): ");
        //        if (int.TryParse(Console.ReadLine(), out int pilihanProdi) &&
        //            pilihanProdi >= 1 && pilihanProdi <= daftarProdi.Count)
        //        {
        //            selectedProdi = daftarProdi[pilihanProdi - 1];
        //        }
        //        else
        //        {
        //            Console.WriteLine("Pilihan tidak valid. Coba lagi. \n");

        //        }
        //    } while (selectedProdi == null);

        //    mhs.IDProdi = selectedProdi.KodeProdi;

        //    int angkatan;
        //    do
        //    {
        //        Console.Write("Masukkan Angkatan (2018 - 2025): ");
        //        if (!int.TryParse(Console.ReadLine(), out angkatan) ||
        //            angkatan < 2018 || angkatan > 2025)
        //        {
        //            Console.WriteLine("Input angkatan tidak valid.");

        //            angkatan = -1;
        //        }
        //    } while (angkatan == -1);

        //    mhs.Angkatan = angkatan;

        //    Console.Write("Masukkan Semester Aktif: ");
        //    mhs.SemesterAktif = Console.ReadLine();

        //    DateTime tglLahir;
        //    do
        //    {
        //        Console.Write("Masukkan Tanggal Lahir (dd-MM-yyyy): ");
        //        string tglInput = Console.ReadLine();
        //        if (!DateTime.TryParseExact(tglInput, "dd-MM-yyyy", null,
        //            System.Globalization.DateTimeStyles.None, out tglLahir))
        //        {
        //            Console.WriteLine("Format tanggal tidak valid.");

        //        }
        //    } while (tglLahir == default(DateTime));

        //    mhs.TanggalLahir = tglLahir;

        //    daftarMahasiswa.Add(mhs);

        //    Console.WriteLine("\nData mahasiswa berhasil ditambahkan.");
        //    mhs.InputMahasiswa();
        //    Console.WriteLine("\nTekan ENTER untuk kembali...");
        //    Console.ReadLine();
        //}

        //public void DaftarMahasiswa()
        //{
        //    Console.Clear();
        //    Console.WriteLine("===== Daftar Mahasiswa =====");

        //    if (daftarMahasiswa.Count == 0)
        //    {
        //        Console.WriteLine("Belum ada data mahasiswa.");
        //        Console.WriteLine("\nTekan ENTER untuk kembali...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    int no = 1;
        //    foreach (var m in daftarMahasiswa)
        //    {
        //        Console.Write($"{no}. ");
        //        m.InputMahasiswa();
        //        no++;
        //    }

        //    Console.WriteLine("\nTekan ENTER untuk kembali...");
        //    Console.ReadLine();
        //}

        //public void UbahMahasiswa()
        //{
        //    Console.Clear();
        //    Console.WriteLine("===== Ubah Data Mahasiswa =====");

        //    if (daftarMahasiswa.Count == 0)
        //    {
        //        Console.WriteLine("Belum ada data mahasiswa.");
        //        Console.WriteLine("\nTekan ENTER untuk kembali...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    Console.Write("Masukkan NIM mahasiswa: ");
        //    string nim = Console.ReadLine();

        //    var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == nim);
        //    if (mhs == null)
        //    {
        //        Console.WriteLine("NIM tidak ditemukan.");
        //        Console.WriteLine("\nTekan ENTER untuk kembali...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    Console.WriteLine("Tekan ENTER untuk melewati perubahan.");

        //    Console.Write($"Nama lama: {mhs.NamaMhs}\nNama baru: ");
        //    string nama = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(nama)) mhs.NamaMhs = nama;

        //    Console.Write($"Alamat lama: {mhs.AlamatMhs}\nAlamat baru: ");
        //    string alamat = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(alamat)) mhs.AlamatMhs = alamat;

        //    Console.Write($"Semester lama: {mhs.SemesterAktif}\nSemester baru: ");
        //    string sem = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(sem)) mhs.SemesterAktif = sem;

        //    Console.WriteLine("\nData mahasiswa berhasil diubah.");
        //    Console.WriteLine("\nTekan ENTER untuk kembali...");
        //    Console.ReadLine();
        //}

        //public void HapusMahasiswa()
        //{
        //    Console.Clear();
        //    Console.WriteLine("===== Hapus Mahasiswa =====");

        //    if (daftarMahasiswa.Count == 0)
        //    {
        //        Console.WriteLine("Belum ada data mahasiswa.");
        //        Console.WriteLine("\nTekan ENTER untuk kembali...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    Console.Write("Masukkan NIM mahasiswa yang akan dihapus: ");
        //    string nim = Console.ReadLine();

        //    var mhs = daftarMahasiswa.FirstOrDefault(m => m.NIM == nim);
        //    if (mhs == null)
        //    {
        //        Console.WriteLine("NIM tidak ditemukan.");
        //        Console.WriteLine("\nTekan ENTER untuk kembali...");
        //        Console.ReadLine();
        //        return;
        //    }

        //    Console.Write("Yakin ingin menghapus? (Y/N): ");
        //    string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

        //    if (konfirmasi == "Y")
        //    {
        //        daftarMahasiswa.Remove(mhs);
        //        Console.WriteLine("Data mahasiswa berhasil dihapus.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Penghapusan dibatalkan.");
        //    }
        //Console.WriteLine("\nTekan ENTER untuk kembali...");
        //Console.ReadLine();
        //}
    }
}