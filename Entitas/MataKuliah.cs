using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class MataKuliah
    {
        public string KodeMK { get; set; }
        public string NamaMK { get; set; }
        public int SKS { get; set; }
        public string IDProdi { get; set; }
        public int Semester { get; set; }
        public string DosenPengampu { get; set; }

        public void InputMataKuliah()
        {
            Console.WriteLine($"Mata Kuliah {NamaMK} dengan kode {KodeMK}, {SKS} SKS, semester {Semester}, dosen pengampu {DosenPengampu}, kode  prodi {IDProdi}");
        }

        
    }
}
