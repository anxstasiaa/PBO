using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class Nilai
    {
        public string NIM { get; set; }
        public string KodeMK { get; set; }
        public string KodeKelas { get; set; }
        public string IDSemester { get; set; }
        public string IDProdi { get; set; }
        public string NamaMK { get; set; }
        public int SKS { get; set; }
        public double NilaiTugas { get; set; }
        public double NilaiUTS { get; set; }
        public double NilaiUAS { get; set; }
        public double NilaiSoftSkill { get; set; }
        public double NilaiAkhir { get; set; }
        public string HurufMutu { get; set; }
        public double? AngkaMutu { get; set; }
        public Nilai() { }


        public Nilai(string nim, string kodeKelas, string IDprodi, string IDsemester,
                     string kodeMK, string namaMK, int sks,
                     double tugas, double uts, double uas, double softskill)
        {
            NIM = nim;
            KodeKelas = kodeKelas;
            IDProdi = IDprodi;
            IDSemester = IDsemester;
            KodeMK = kodeMK;
            NamaMK = namaMK;
            SKS = sks;
            NilaiTugas = tugas;
            NilaiUTS = uts;
            NilaiUAS = uas;
            NilaiSoftSkill = softskill;

            HitungNilaiAkhir(); // langsung proses nilai akhir & huruf mutu
        }

        public void HitungNilaiAkhir()
        {
            NilaiAkhir = (NilaiTugas * 0.15) + (NilaiUTS * 0.35) + (NilaiUAS * 0.35) + (NilaiSoftSkill * 0.15);
            TentukanHurufDanAngkaMutu();
        }

        private void TentukanHurufDanAngkaMutu()
        {
            if (NilaiAkhir >= 95)
            {
                HurufMutu = "A";
                AngkaMutu = 4.0;
            }
            else if (NilaiAkhir >= 95)
            {
                HurufMutu = "A-";
                AngkaMutu = 3.75;
            }
            else if (NilaiAkhir >= 85)
            {
                HurufMutu = "B+";
                AngkaMutu = 3.25;
            }
            else if (NilaiAkhir >= 80)
            {
                HurufMutu = "B";
                AngkaMutu = 3.0;
            }
            else if (NilaiAkhir >= 75)
            {
                HurufMutu = "B-";
                AngkaMutu = 2.75;
            }
            else if (NilaiAkhir >= 60)
            {
                HurufMutu = "C";
                AngkaMutu = 2.0;
            }
            else if (NilaiAkhir >= 55)
            {
                HurufMutu = "D";
                AngkaMutu = 1.0;
            }
            else
            {
                HurufMutu = "E";
                AngkaMutu = 0.0;
            }
        }

    }
}
