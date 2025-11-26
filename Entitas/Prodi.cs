using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class Prodi
    {
        public string KodeProdi { get; set; } //max.5
        public string NamaProdi { get; set; } //min.10
        public string AliasProdi { get; set; } //max.15

        public void InputProdi()
        {
            Console.WriteLine($"Program Studi {NamaProdi} dengan kode {KodeProdi} dan alias {AliasProdi}");
        }

    }
}
