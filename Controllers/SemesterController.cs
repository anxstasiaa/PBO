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

        public SemesterController(List<Semester> s)
        {
            daftarSemester = s;
        }

        public void TambahSemester(Semester semesterBaru)
        {
            daftarSemester.Add(semesterBaru);
        }

        // READ - ambil semua semester
        public List<Semester> GetSemuaSemester()
        {
            return daftarSemester;
        }

        // READ - cari semester by ID / kode semester
        public Semester CariSemester(string IDSemester)
        {
            return daftarSemester
                .FirstOrDefault(s => s.IDSemester == IDSemester);
        }

        // UPDATE - ubah nama/atribut semester tertentu
        public bool UpdateSemester(string IDSemester, Semester dataBaru)
        {
            var sem = CariSemester(IDSemester);
            if (sem != null)
            {
                sem.IDSemester = dataBaru.IDSemester;
                sem.namaSemester = dataBaru.namaSemester;
                sem.TahunAjaran = dataBaru.TahunAjaran;
                return true;
            }
            return false;
        }

        // DELETE - hapus semester
        public bool HapusSemester(string IDSemester)
        {
            var sem = CariSemester(IDSemester);
            if (sem != null)
            {
                daftarSemester.Remove(sem);
                return true;
            }
            return false;
        }

    }
}
