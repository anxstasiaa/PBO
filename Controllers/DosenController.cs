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
            Console.WriteLine("No | NIM        | Nama                     | Nilai Akhir | Huruf");
            Console.WriteLine("---+------------+--------------------------+-------------+------");

            int no = 1;
            foreach (var n in daftarNilaiKelas)
            {
                var m = daftarMahasiswa.FirstOrDefault(x => x.NIM == n.NIM);
                string nama = m?.NamaMhs ?? "(Nama tidak ditemukan)";
                string nilaiAkhir = n.NilaiAkhir > 0 ? n.NilaiAkhir.ToString("0.00") : "-";
                string hurufmutu = n.HurufMutu ?? "-";

                Console.WriteLine($"{no,2} | {n.NIM,-10} | {nama,-24} | {nilaiAkhir,11} | {hurufmutu,-5}");
                no++;
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

            Console.WriteLine("\n=== Input Komponen Nilai (0-100) ===");

            // Input Nilai Tugas
            double tugas;
            do
            {
                Console.Write("Nilai Tugas (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out tugas) || tugas < 0 || tugas > 100)
                {
                    Console.WriteLine("Nilai harus antara 0-100!");
                    tugas = -1;
                }
            } while (tugas < 0);
            nilaiObj.NilaiTugas = tugas;

            // Input Nilai UTS
            double uts;
            do
            {
                Console.Write("Nilai UTS (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out uts) || uts < 0 || uts > 100)
                {
                    Console.WriteLine("Nilai harus antara 0-100!");
                    uts = -1;
                }
            } while (uts < 0);
            nilaiObj.NilaiUTS = uts;

            // Input Nilai UAS
            double uas;
            do
            {
                Console.Write("Nilai UAS (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out uas) || uas < 0 || uas > 100)
                {
                    Console.WriteLine("Nilai harus antara 0-100!");
                    uas = -1;
                }
            } while (uas < 0);
            nilaiObj.NilaiUAS = uas;

            // Input Nilai Soft Skill
            double softskill;
            do
            {
                Console.Write("Nilai Soft Skill (0-100): ");
                if (!double.TryParse(Console.ReadLine(), out softskill) || softskill < 0 || softskill > 100)
                {
                    Console.WriteLine("Nilai harus antara 0-100!");
                    softskill = -1;
                }
            } while (softskill < 0);
            nilaiObj.NilaiSoftSkill = softskill;

            // HITUNG NILAI AKHIR DAN HURUF MUTU
            nilaiObj.HitungNilaiAkhir();

            Console.WriteLine("\n✓ Nilai berhasil diperbarui!");
            Console.WriteLine($"Nilai Akhir: {nilaiObj.NilaiAkhir:0.00}");
            Console.WriteLine($"Huruf Mutu: {nilaiObj.HurufMutu}");
            Console.WriteLine($"Angka Mutu: {nilaiObj.AngkaMutu:0.00}");

            Console.WriteLine("\nTekan Enter untuk kembali...");
            Console.ReadLine();
        }

    }
}
