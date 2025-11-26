using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class Mahasiswa
    {
        public string NIM { get; set; }
        public string NamaMhs { get; set; }
        public string AlamatMhs { get; set; }
        public char JenisKelamin { get; set; }
        public string IDProdi { get; set; }
        public int Angkatan { get; set; }
        public string SemesterAktif { get; set; }
        public DateTime TanggalLahir { get; set; }

        public void InputMahasiswa()
            { 
                string jk = (JenisKelamin == 'L') ? "Laki-laki" : "Perempuan";
                Console.WriteLine($"Mahasiswwa dengan NIM {NIM}, Nama {NamaMhs}, Alamat {AlamatMhs}, Jenis Kelamin {jk}, Prodi {IDProdi}, Angkatan {Angkatan}, Tanggal Lahir {TanggalLahir.ToShortDateString()}, Semester {SemesterAktif}");
            }
        }
}
