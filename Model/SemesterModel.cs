using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class SemesterModel
    {
        public static List<Semester> daftarSemester;

        public SemesterModel(List<Semester> semester)
        {
            daftarSemester = semester;
        }

        // CREATE - Insert Semester Baru
        public bool InsertSemester(Semester semester)
        {
            try
            {
                // Validasi ID Semester tidak boleh duplikat
                if (daftarSemester.Any(s => s.IDSemester == semester.IDSemester))
                {
                    Console.WriteLine("ID Semester sudah ada!");
                    return false;
                }

                daftarSemester.Add(semester);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // READ - Ambil Semua Semester
        public List<Semester> GetAllSemester()
        {
            try
            {
                return daftarSemester.OrderByDescending(s => s.IDSemester).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Semester>();
            }
        }

        // READ - Cari Semester by ID
        public Semester GetSemesterByID(string idSemester)
        {
            try
            {
                return daftarSemester.FirstOrDefault(s => s.IDSemester == idSemester);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // UPDATE - Ubah Data Semester
        public bool UpdateSemester(Semester semester)
        {
            try
            {
                var semesterLama = daftarSemester.FirstOrDefault(s => s.IDSemester == semester.IDSemester);

                if (semesterLama == null)
                {
                    Console.WriteLine("Semester tidak ditemukan!");
                    return false;
                }

                // Update data
                semesterLama.namaSemester = semester.namaSemester;
                semesterLama.TahunAjaran = semester.TahunAjaran;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // DELETE - Hapus Semester
        public bool DeleteSemester(string idSemester)
        {
            try
            {
                var semester = daftarSemester.FirstOrDefault(s => s.IDSemester == idSemester);

                if (semester == null)
                {
                    Console.WriteLine("Semester tidak ditemukan!");
                    return false;
                }

                daftarSemester.Remove(semester);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
