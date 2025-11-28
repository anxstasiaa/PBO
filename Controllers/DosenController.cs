using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_PBO
{
    internal class DosenController
    {
        static List<Dosen> daftarDosen;
        static List<Prodi> daftarProdi;
        static List<MataKuliah> daftarMataKuliah;
        static List<Nilai> daftarNilai;
        static List<KelasKuliah> daftarKelasKuliah;
        static List<Mahasiswa> daftarMahasiswa;

        public DosenController(
            List<Dosen> dd,
            List<Prodi> prodi,
            List<MataKuliah> mk,
            List<Nilai> nilai,
            List<KelasKuliah> kk,
            List<Mahasiswa> mhs)
        {
            daftarDosen = dd;
            daftarProdi = prodi;
            daftarMataKuliah = mk;
            daftarNilai = nilai;
            daftarKelasKuliah = kk;
            daftarMahasiswa = mhs;
        }

        private string SemesterAktif()
        {
            return "20241";
        }

        public void DaftarKelasKuliahDosen(string usernameDosen, string IDProdi)
        {
            Console.Clear();
            Console.WriteLine("=== Kelas Kuliah yang Anda Ampu ===");
            Console.WriteLine("Semester: " + SemesterAktif());
            Console.WriteLine();

            var kelasDiampu = daftarKelasKuliah
                .Where(k =>
                    k.DosenPengampu != null &&
                    k.DosenPengampu.Equals(usernameDosen, StringComparison.OrdinalIgnoreCase) &&
                    k.IDProdi.Equals(IDProdi, StringComparison.OrdinalIgnoreCase) &&
                    k.IDSemester == SemesterAktif())
                .ToList();

            if (!kelasDiampu.Any())
            {
                Console.WriteLine("Tidak ada kelas yang Anda ampu pada semester aktif.");
                Console.WriteLine("Tekan Enter untuk kembali...");
                Console.ReadLine();
                return;
            }

            int no = 1;
            foreach (var k in kelasDiampu)
            {
                string namaMK = daftarMataKuliah
                    .FirstOrDefault(m => m.KodeMK == k.KodeMK)?.NamaMK ?? "(Nama MK tidak ditemukan)";

                Console.WriteLine(no + ". " + k.KodeKelas + " | " + k.KodeMK + " - " + namaMK);
                Console.WriteLine("   Kelas: " + k.NamaKelas + " | Kapasitas: " + k.KapasitasKelas);
                no++;
            }

            Console.WriteLine();
            Console.WriteLine("Tekan Enter untuk kembali...");
            Console.ReadLine();
        }
        public void InputNilaiMahasiswa(string usernameDosen, string idProdi)
        {
            Console.Clear();
            Console.WriteLine("=== Input atau Ubah Nilai Mahasiswa ===");
            Console.WriteLine("Semester: " + SemesterAktif());
            Console.WriteLine();

            // ambil kelas dosen
            var kelasDiampu = daftarKelasKuliah
                .Where(k =>
                    k.DosenPengampu != null &&
                    k.DosenPengampu.Equals(usernameDosen, StringComparison.OrdinalIgnoreCase) &&
                    k.IDProdi.Equals(idProdi, StringComparison.OrdinalIgnoreCase) &&
                    k.IDSemester == SemesterAktif())
                .ToList();

            if (!kelasDiampu.Any())
            {
                Console.WriteLine("Anda tidak mengampu kelas apa pun pada semester aktif.");
                Console.WriteLine("Tekan Enter...");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < kelasDiampu.Count; i++)
            {
                var k = kelasDiampu[i];
                string namaMK = daftarMataKuliah
                    .FirstOrDefault(m => m.KodeMK == k.KodeMK)?.NamaMK ?? "(Nama MK tidak ditemukan)";

                Console.WriteLine((i + 1) + ". " + k.KodeKelas + " | " + k.KodeMK + " - " + namaMK);
            }

            Console.Write("\nPilih nomor kelas: ");
            if (!int.TryParse(Console.ReadLine(), out int pilih) || pilih < 1 || pilih > kelasDiampu.Count)
            {
                Console.WriteLine("Pilihan tidak valid.");
                Console.ReadLine();
                return;
            }

            var kelasTerpilih = kelasDiampu[pilih - 1];

            var daftarNilaiKelas = daftarNilai
                .Where(n => n.KodeKelas == kelasTerpilih.KodeKelas)
                .ToList();

            if (!daftarNilaiKelas.Any())
            {
                Console.WriteLine("Belum ada mahasiswa pada kelas ini.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\nDaftar Mahasiswa:");
            Console.WriteLine("NIM        | Nama                     | Nilai Angka | Nilai Huruf");

            foreach (var n in daftarNilaiKelas)
            {
                var m = daftarMahasiswa.FirstOrDefault(x => x.NIM == n.NIM);
                string nama = m?.NamaMhs ?? "(Nama tidak ditemukan)";

                Console.WriteLine(n.NIM.PadRight(10) +
                                  " | " + nama.PadRight(22) +
                                  " | " + n.NilaiAkhir.ToString().PadLeft(11) +
                                  " | " + n.HurufMutu);
            }

            Console.Write("\nMasukkan NIM mahasiswa yang ingin diubah nilainya: ");
            string nim = Console.ReadLine()?.Trim();

            var nilaiObj = daftarNilaiKelas.FirstOrDefault(x => x.NIM == nim);
            if (nilaiObj == null)
            {
                Console.WriteLine("NIM tidak ditemukan.");
                Console.ReadLine();
                return;
            }

            Console.Write("Masukkan nilai angka baru: ");
            if (double.TryParse(Console.ReadLine(), out double nilaiAngkaBaru))
                nilaiObj.NilaiAkhir = nilaiAngkaBaru;

            Console.Write("Masukkan nilai huruf baru (A/B/C/D/E): ");
            string huruf = Console.ReadLine()?.Trim().ToUpper();
            if (!string.IsNullOrEmpty(huruf))
                nilaiObj.HurufMutu = huruf;

            Console.WriteLine("\nNilai berhasil diperbarui.");
            Console.ReadLine();
        }
    }
}
