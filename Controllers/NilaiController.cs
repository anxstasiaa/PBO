using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Project_PBO
{
    internal class NilaiController
    {
        List<Nilai> daftarNilai;
        List<Mahasiswa> daftarMahasiswa;
        List<KelasKuliah> daftarKelasKuliah;
        List<MataKuliah> daftarMataKuliah;

        public NilaiController(List<Nilai> n, List<Mahasiswa> mhs, List<KelasKuliah> kk, List<MataKuliah> mk)
        {
            daftarNilai = n;
            daftarMahasiswa = mhs;
            daftarKelasKuliah = kk;
            daftarMataKuliah = mk;
        }

        private string SemesterAktif()
        {
            return "20241";
        }

        public void MenuNilaiDosen(string usernameDosen)
        {
            int pilih;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU NILAI DOSEN ===");
                Console.WriteLine("1. Input Nilai Mahasiswa");
                Console.WriteLine("2. Ubah Nilai Mahasiswa");
                Console.WriteLine("3. Lihat Nilai pada Kelas");
                Console.WriteLine("0. Kembali");
                Console.Write("Pilih: ");

                int.TryParse(Console.ReadLine(), out pilih);
                switch (pilih)
                {
                    case 1: InputNilai(usernameDosen); break;
                    case 2: EditNilai(usernameDosen); break;
                    case 3: LihatNilaiKelas(usernameDosen); break;
                }

            } while (pilih != 0);
        }

        // ================== INPUT NILAI BARU ========================
        public void InputNilai(string usernameDosen)
        {
            var kelas = PilihKelasDosen(usernameDosen);
            if (kelas == null) return;

             var peserta = daftarNilai
             .Where(n => n.KodeKelas == kelas.KodeKelas)
             .Join(
                 daftarMahasiswa,
                 n => n.NIM,
                 m => m.NIM,
                 (n, m) => m
             )
             .ToList();


            if (peserta.Count == 0)
            {
                Console.WriteLine("Tidak ada mahasiswa di kelas ini.");
                Console.ReadLine(); return;
            }

            Console.WriteLine("\nDaftar Mahasiswa:");
            for (int i = 0; i < peserta.Count; i++)
                Console.WriteLine($"{i + 1}. {peserta[i].NIM} - {peserta[i].NamaMhs}");

            Console.Write("\nPilih mahasiswa: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) || idx < 1 || idx > peserta.Count)
                return;

            var mhs = peserta[idx - 1];

            if (daftarNilai.Any(n => n.NIM == mhs.NIM && n.KodeKelas == kelas.KodeKelas))
            {
                Console.WriteLine("Mahasiswa sudah memiliki nilai! Gunakan menu Edit.");
                Console.ReadLine();
                return;
            }

            var mk = daftarMataKuliah.First(m => m.KodeMK == kelas.KodeMK);

            Console.WriteLine("\nInput nilai:");
            double tugas = InputAngka("Tugas: ");
            double uts = InputAngka("UTS: ");
            double uas = InputAngka("UAS: ");
            double soft = InputAngka("Softskill: ");

            Nilai nilai = new Nilai(
                mhs.NIM,
                kelas.KodeKelas,
                mhs.IDProdi,
                SemesterAktif(),
                mk.KodeMK,
                mk.NamaMK,
                mk.SKS,
                tugas, uts, uas, soft
            );

            daftarNilai.Add(nilai);

            Console.WriteLine("Nilai berhasil ditambahkan!");
            Console.ReadLine();
        }

        // ================== EDIT NILAI =============================
        public void EditNilai(string usernameDosen)
        {
            var kelas = PilihKelasDosen(usernameDosen);
            if (kelas == null) return;

            var nilaiKelas = daftarNilai.Where(n => n.KodeKelas == kelas.KodeKelas).ToList();

            if (nilaiKelas.Count == 0)
            {
                Console.WriteLine("\nBelum ada nilai di kelas ini.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nDaftar Nilai:");
            for (int i = 0; i < nilaiKelas.Count; i++)
                Console.WriteLine($"{i + 1}. {nilaiKelas[i].NIM} - {nilaiKelas[i].NamaMK} : {nilaiKelas[i].NilaiAkhir}");

            Console.Write("\nPilih mahasiswa: ");
            int idx;
            if (!int.TryParse(Console.ReadLine(), out idx) || idx < 1 || idx > nilaiKelas.Count)
                return;

            var nilai = nilaiKelas[idx - 1];

            nilai.NilaiTugas = InputUbah("Tugas", nilai.NilaiTugas);
            nilai.NilaiUTS = InputUbah("UTS", nilai.NilaiUTS);
            nilai.NilaiUAS = InputUbah("UAS", nilai.NilaiUAS);
            nilai.NilaiSoftSkill = InputUbah("Softskill", nilai.NilaiSoftSkill);

            nilai.HitungNilaiAkhir();

            Console.WriteLine("\nNilai berhasil diperbarui!");
            Console.ReadLine();
        }

        // ================== LIHAT NILAI PER KELAS =================
        public void LihatNilaiKelas(string usernameDosen)
        {
            var kelas = PilihKelasDosen(usernameDosen);
            if (kelas == null) return;

            var nilaiKelas = daftarNilai.Where(n => n.KodeKelas == kelas.KodeKelas).ToList();

            Console.WriteLine("\nNIM     | NA   | HM");
            Console.WriteLine("--------------------------");
            foreach (var n in nilaiKelas)
                Console.WriteLine($"{n.NIM,-8} | {n.NilaiAkhir,4:F1} | {n.HurufMutu}");

            Console.ReadLine();
        }


        // ============================================================
        // =============== FITUR UNTUK MAHASISWA ======================
        // ============================================================

        public void LihatNilaiMahasiswa(string nim)
        {
            var nilai = daftarNilai.Where(n => n.NIM == nim).ToList();

            Console.WriteLine("\nKODEMK | NAMA MK        | NA  | HM");
            Console.WriteLine("-------------------------------------");

            foreach (var n in nilai)
                Console.WriteLine($"{n.KodeMK,-6} | {n.NamaMK,-13} | {n.NilaiAkhir,4:F1} | {n.HurufMutu}");

            Console.ReadLine();
        }


        // ============================================================
        // =============== FUNGSI BANTUAN =============================
        // ============================================================

        private KelasKuliah PilihKelasDosen(string usernameDosen)
        {
            var kelasDosen = daftarKelasKuliah
                .Where(k => k.DosenPengampu == usernameDosen && k.IDSemester == SemesterAktif())
                .ToList();

            if (kelasDosen.Count == 0)
            {
                Console.WriteLine("Anda tidak mengampu kelas semester ini.");
                Console.ReadLine();
                return null;
            }

            Console.WriteLine("\nKelas yang Anda ampu:");
            for (int i = 0; i < kelasDosen.Count; i++)
                Console.WriteLine($"{i + 1}. {kelasDosen[i].KodeKelas} | MK: {kelasDosen[i].KodeMK}");

            Console.Write("\nPilih kelas: ");
            int pilih;
            if (!int.TryParse(Console.ReadLine(), out pilih) || pilih < 1 || pilih > kelasDosen.Count)
                return null;

            return kelasDosen[pilih - 1];
        }

        private double InputAngka(string label)
        {
            Console.Write(label);
            double val;
            while (!double.TryParse(Console.ReadLine(), out val) || val < 0 || val > 100)
                Console.Write("Input angka 0–100: ");
            return val;
        }

        private double InputUbah(string nama, double lama)
        {
            Console.Write($"{nama} lama {lama}, baru (ENTER = tidak diubah): ");
            string input = Console.ReadLine();

            return double.TryParse(input, out double v) ? v : lama;
        }
    }
}
