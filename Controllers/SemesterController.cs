using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class SemesterController
    {
        static List<Semester> daftarSemester = new List<Semester>();

        // CREATE - Tambah Semester Baru
        private void TambahSemester()
        {
            Console.Clear();
            Console.WriteLine("=== TAMBAH SEMESTER BARU ===\n");

            Semester semesterBaru = new Semester();

            Console.Write("ID Semester (contoh: 20241, 20242): ");
            semesterBaru.IDSemester = Console.ReadLine();

            Console.Write("Nama Semester (contoh: Ganjil, Genap): ");
            semesterBaru.namaSemester = Console.ReadLine();

            Console.Write("Tahun Ajaran (contoh: 2024, 2025): ");
            semesterBaru.TahunAjaran = Console.ReadLine();

            // Panggil method dari SemesterModel untuk insert ke database
            bool berhasil = SemModel.InsertSemester(semesterBaru);

            if (berhasil)
            {
                Console.WriteLine("\n✓ Semester berhasil ditambahkan!");
            }
            else
            {
                Console.WriteLine("\n✗ Gagal menambahkan semester!");
            }

            Console.WriteLine("\nTekan tombol apapun untuk kembali...");
            Console.ReadKey();
        }

        // READ - Lihat Semua Semester
        private void LihatSemuaSemester()
        {
            Console.Clear();
            Console.WriteLine("=== DAFTAR SEMUA SEMESTER ===\n");

            List<Semester> semuaSemester = SemModel.GetAllSemester();

            if (semuaSemester == null || semuaSemester.Count == 0)
            {
                Console.WriteLine("Tidak ada data semester.");
            }
            else
            {
                Console.WriteLine("┌──────────────┬─────────────────┬──────────────┐");
                Console.WriteLine("│ ID Semester  │ Nama Semester   │ Tahun Ajaran │");
                Console.WriteLine("├──────────────┼─────────────────┼──────────────┤");

                foreach (var sem in semuaSemester)
                {
                    Console.WriteLine($"│ {sem.IDSemester,-12} │ {sem.namaSemester,-15} │ {sem.TahunAjaran,-12} │");
                }

                Console.WriteLine("└──────────────┴─────────────────┴──────────────┘");
            }

            Console.WriteLine("\nTekan tombol apapun untuk kembali...");
            Console.ReadKey();
        }

        // READ - Cari Semester by ID
        private void CariSemesterByID()
        {
            Console.Clear();
            Console.WriteLine("=== CARI SEMESTER BY ID ===\n");

            Console.Write("Masukkan ID Semester: ");
            string idSemester = Console.ReadLine();

            Semester semester = SemModel.GetSemesterByID(idSemester);

            if (semester == null)
            {
                Console.WriteLine("\n✗ Semester tidak ditemukan!");
            }
            else
            {
                Console.WriteLine("\n=== DATA SEMESTER ===");
                Console.WriteLine($"ID Semester   : {semester.IDSemester}");
                Console.WriteLine($"Nama Semester : {semester.namaSemester}");
                Console.WriteLine($"Tahun Ajaran  : {semester.TahunAjaran}");
            }

            Console.WriteLine("\nTekan tombol apapun untuk kembali...");
            Console.ReadKey();
        }

        // UPDATE - Ubah Semester
        private void UpdateSemester()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE SEMESTER ===\n");

            Console.Write("Masukkan ID Semester yang akan diupdate: ");
            string idSemester = Console.ReadLine();

            Semester semester = SemModel.GetSemesterByID(idSemester);

            if (semester == null)
            {
                Console.WriteLine("\n✗ Semester tidak ditemukan!");
                Console.WriteLine("\nTekan tombol apapun untuk kembali...");
                Console.ReadKey();
                return;
            }

            // Tampilkan data lama
            Console.WriteLine("\n=== DATA SEMESTER SAAT INI ===");
            Console.WriteLine($"ID Semester   : {semester.IDSemester}");
            Console.WriteLine($"Nama Semester : {semester.namaSemester}");
            Console.WriteLine($"Tahun Ajaran  : {semester.TahunAjaran}");

            Console.WriteLine("\n=== INPUT DATA BARU ===");
            Console.WriteLine("(Tekan Enter untuk tidak mengubah)");

            Console.Write($"Nama Semester [{semester.namaSemester}]: ");
            string namaBaru = Console.ReadLine();
            if (!string.IsNullOrEmpty(namaBaru))
                semester.namaSemester = namaBaru;

            Console.Write($"Tahun Ajaran [{semester.TahunAjaran}]: ");
            string tahunBaru = Console.ReadLine();
            if (!string.IsNullOrEmpty(tahunBaru))
                semester.TahunAjaran = tahunBaru;

            Console.Write("\nYakin ingin mengupdate? (y/n): ");
            string konfirmasi = Console.ReadLine();

            if (konfirmasi.ToLower() == "y")
            {
                bool berhasil = SemModel.UpdateSemester(semester);

                if (berhasil)
                {
                    Console.WriteLine("\n✓ Semester berhasil diupdate!");
                }
                else
                {
                    Console.WriteLine("\n✗ Gagal mengupdate semester!");
                }
            }
            else
            {
                Console.WriteLine("\nUpdate dibatalkan.");
            }

            Console.WriteLine("\nTekan tombol apapun untuk kembali...");
            Console.ReadKey();
        }

        // DELETE - Hapus Semester
        private void HapusSemester()
        {
            Console.Clear();
            Console.WriteLine("=== HAPUS SEMESTER ===\n");

            Console.Write("Masukkan ID Semester yang akan dihapus: ");
            string idSemester = Console.ReadLine();

            Semester semester = SemModel.GetSemesterByID(idSemester);

            if (semester == null)
            {
                Console.WriteLine("\n✗ Semester tidak ditemukan!");
                Console.WriteLine("\nTekan tombol apapun untuk kembali...");
                Console.ReadKey();
                return;
            }

            // Tampilkan data yang akan dihapus
            Console.WriteLine("\n=== DATA SEMESTER YANG AKAN DIHAPUS ===");
            Console.WriteLine($"ID Semester   : {semester.IDSemester}");
            Console.WriteLine($"Nama Semester : {semester.namaSemester}");
            Console.WriteLine($"Tahun Ajaran  : {semester.TahunAjaran}");

            Console.Write("\nYakin ingin menghapus? (y/n): ");
            string konfirmasi = Console.ReadLine();

            if (konfirmasi.ToLower() == "y")
            {
                bool berhasil = SemModel.DeleteSemester(idSemester);

                if (berhasil)
                {
                    Console.WriteLine("\n✓ Semester berhasil dihapus!");
                }
                else
                {
                    Console.WriteLine("\n✗ Gagal menghapus semester!");
                }
            }
            else
            {
                Console.WriteLine("\nPenghapusan dibatalkan.");
            }

            Console.WriteLine("\nTekan tombol apapun untuk kembali...");
            Console.ReadKey();
        }
       

    }
}
