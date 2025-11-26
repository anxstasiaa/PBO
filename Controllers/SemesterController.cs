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
    }
}
