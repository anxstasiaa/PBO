using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class Semester
    {
        public string kodeSemester { get; set; } //20241,20242, dsb
        public string namaSemester { get; set; } //ganjil/genap -> tahunajaran (2024,2025, dsb)
        public string TahunAjaran { get; set; } //ganjil/genap -> tahunajaran (2024,2025, dsb)

        public Semester()
        {
            kodeSemester = string.Empty;
            namaSemester = string.Empty;
            TahunAjaran = string.Empty;
        }
        public void InputSemester()
        {
            Console.WriteLine($"Semester {namaSemester} dengan kode {kodeSemester} dan tahun ajaran {TahunAjaran}");
        }
    }
}
