using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class KelasKuliahController
    {
        static List<KelasKuliah> daftarKelasKuliah = new List<KelasKuliah>();
        static List<MataKuliah> daftarMataKuliah = new List<MataKuliah>();
        static List<Prodi> daftarProdi = new List<Prodi>();
        static List<Semester> daftarSemester = new List<Semester>();
        static List<Dosen> daftarDosen = new List<Dosen>();

        public KelasKuliahController(List<KelasKuliah> kk, List<MataKuliah> mk, List<Prodi> prodi, List<Semester> s, List<Dosen> d)
        {
            daftarKelasKuliah = kk;
            daftarMataKuliah = mk;
            daftarProdi = prodi;
            daftarSemester = s;
            daftarDosen = d;

        }

        public void DaftarKelasKuliah(string idProdi = null)
        {
            Console.Clear();
            Console.WriteLine("===== Daftar Kelas Kuliah =====");

            var kelasFiltered = string.IsNullOrWhiteSpace(idProdi)
                ? daftarKelasKuliah
                : daftarKelasKuliah.Where(k => string.Equals(k.IDProdi, idProdi, StringComparison.OrdinalIgnoreCase)).ToList();

            if (kelasFiltered.Count == 0)
            {
                Console.WriteLine("Belum ada kelas kuliah terdaftar.");
                return;
            }

            int no = 1;
            foreach (var kelas in kelasFiltered)
            {
                // Cari nama mata kuliah
                var mk = daftarMataKuliah.FirstOrDefault(m => m.KodeMK == kelas.KodeMK);
                string namaMK = mk != null ? mk.NamaMK : "Unknown";

                // Cari nama semester
                var sem = daftarSemester.FirstOrDefault(s => s.kodeSemester == kelas.IDSemester);
                string namaSem = sem != null ? sem.namaSemester : "Unknown";

                Console.WriteLine($"{no}. Kode Kelas: {kelas.KodeKelas}");
                Console.WriteLine($"   Nama: {kelas.NamaKelas}");
                Console.WriteLine($"   Mata Kuliah: {kelas.KodeMK} - {namaMK}");
                Console.WriteLine($"   Semester: {namaSem} ({kelas.IDSemester})");
                Console.WriteLine($"   Ruangan: {kelas.Ruangan}");
                Console.WriteLine($"   Kapasitas: {kelas.JumlahPeserta}/{kelas.KapasitasKelas}");
                Console.WriteLine($"   Prodi: {kelas.IDProdi}");
                Console.WriteLine();
                no++;
            }
        }

        // CREATE: Menambah kelas kuliah baru
        public void TambahKelasKuliah(string idProdi)
        {
            Console.Clear();
            Console.WriteLine("===== Tambah Kelas Kuliah =====");

            KelasKuliah kelas = new KelasKuliah();
            kelas.IDProdi = idProdi;

            // 1. Pilih Semester Aktif
            Console.WriteLine("\n--- Pilih Semester ---");
            if (daftarSemester.Count == 0)
            {
                Console.WriteLine("Belum ada semester terdaftar! Hubungi Admin Universitas.");
                return;
            }

            for (int i = 0; i < daftarSemester.Count; i++)
            {
                var sem = daftarSemester[i];
                Console.WriteLine($"{i + 1}. {sem.namaSemester} - {sem.TahunAjaran} ({sem.kodeSemester})");
            }

            int pilihanSem;
            do
            {
                Console.Write("Pilih semester (nomor): ");
                if (!int.TryParse(Console.ReadLine(), out pilihanSem) || pilihanSem < 1 || pilihanSem > daftarSemester.Count)
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    pilihanSem = 0;
                }
            } while (pilihanSem == 0);

            kelas.IDSemester = daftarSemester[pilihanSem - 1].kodeSemester;

            // 2. Pilih Mata Kuliah dari prodi yang sama
            Console.WriteLine("\n--- Pilih Mata Kuliah ---");
            var mkProdi = daftarMataKuliah.Where(m => string.Equals(m.IDProdi, idProdi, StringComparison.OrdinalIgnoreCase)).ToList();

            if (mkProdi.Count == 0)
            {
                Console.WriteLine("Belum ada mata kuliah di prodi ini! Tambahkan mata kuliah terlebih dahulu.");
                return;
            }

            for (int i = 0; i < mkProdi.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {mkProdi[i].KodeMK} - {mkProdi[i].NamaMK} ({mkProdi[i].SKS} SKS)");
            }

            int pilihanMK;
            do
            {
                Console.Write("Pilih mata kuliah (nomor): ");
                if (!int.TryParse(Console.ReadLine(), out pilihanMK) || pilihanMK < 1 || pilihanMK > mkProdi.Count)
                {
                    Console.WriteLine("Pilihan tidak valid!");
                    pilihanMK = 0;
                }
            } while (pilihanMK == 0);

            kelas.KodeMK = mkProdi[pilihanMK - 1].KodeMK;

            // 3. Kode Kelas (unique)
            string kodeKelas;
            do
            {
                Console.Write("\nMasukkan Kode Kelas (contoh: A, B, C): ");
                kodeKelas = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(kodeKelas))
                {
                    Console.WriteLine("Kode kelas tidak boleh kosong!");
                    kodeKelas = null;
                }
                else if (daftarKelasKuliah.Any(k => k.KodeKelas == kodeKelas && k.KodeMK == kelas.KodeMK && k.IDSemester == kelas.IDSemester))
                {
                    Console.WriteLine("Kode kelas sudah ada untuk mata kuliah dan semester ini!");
                    kodeKelas = null;
                }
            } while (string.IsNullOrWhiteSpace(kodeKelas));

            kelas.KodeKelas = kodeKelas;

            // 4. Nama Kelas
            string namaKelas;
            do
            {
                Console.Write("Masukkan Nama Kelas (contoh: Kelas A Pagi): ");
                namaKelas = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(namaKelas))
                {
                    Console.WriteLine("Nama kelas tidak boleh kosong!");
                    namaKelas = null;
                }
            } while (string.IsNullOrWhiteSpace(namaKelas));

            kelas.NamaKelas = namaKelas;

            // 5. Ruangan
            string ruangan;
            do
            {
                Console.Write("Masukkan Ruangan (contoh: R101, LAB-A): ");
                ruangan = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(ruangan))
                {
                    Console.WriteLine("Ruangan tidak boleh kosong!");
                    ruangan = null;
                }
            } while (string.IsNullOrWhiteSpace(ruangan));

            kelas.Ruangan = ruangan;

            // 6. Kapasitas Kelas
            int kapasitas;
            do
            {
                Console.Write("Masukkan Kapasitas Kelas (1-100): ");
                if (!int.TryParse(Console.ReadLine(), out kapasitas) || kapasitas < 1 || kapasitas > 100)
                {
                    Console.WriteLine("Kapasitas harus antara 1-100!");
                    kapasitas = 0;
                }
            } while (kapasitas == 0);

            kelas.KapasitasKelas = kapasitas;
            kelas.JumlahPeserta = 0; // Default 0 peserta

            // 7. Pilih Dosen Pengampu (Optional - bisa ditambahkan nanti)
            Console.WriteLine("\n(Info: Dosen pengampu dapat diatur nanti)");

            daftarKelasKuliah.Add(kelas);
            Console.WriteLine("\n✓ Kelas kuliah berhasil ditambahkan!");
            Console.WriteLine($"  Kode Kelas: {kelas.KodeKelas}");
            Console.WriteLine($"  Nama: {kelas.NamaKelas}");
            Console.WriteLine($"  Mata Kuliah: {kelas.KodeMK}");
        }

        // UPDATE: Mengubah data kelas kuliah
        public void UbahKelasKuliah(string idProdi)
        {
            Console.Clear();
            Console.WriteLine("===== Ubah Kelas Kuliah =====");

            if (daftarKelasKuliah.Count == 0)
            {
                Console.WriteLine("Belum ada kelas kuliah untuk diubah.");
                return;
            }

            DaftarKelasKuliah(idProdi);

            Console.Write("\nMasukkan Kode Kelas yang ingin diubah: ");
            string kodeKelas = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

            var kelas = daftarKelasKuliah.FirstOrDefault(k =>
                k.KodeKelas == kodeKelas &&
                string.Equals(k.IDProdi, idProdi, StringComparison.OrdinalIgnoreCase));

            if (kelas == null)
            {
                Console.WriteLine("Kode kelas tidak ditemukan di prodi ini!");
                return;
            }

            Console.WriteLine("\n(Tekan ENTER untuk skip/tidak mengubah)");

            // Ubah Nama Kelas
            Console.Write($"Nama lama: {kelas.NamaKelas}\nNama baru: ");
            string namaBaru = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(namaBaru))
                kelas.NamaKelas = namaBaru.Trim();

            // Ubah Ruangan
            Console.Write($"Ruangan lama: {kelas.Ruangan}\nRuangan baru: ");
            string ruanganBaru = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ruanganBaru))
                kelas.Ruangan = ruanganBaru.Trim().ToUpper();

            // Ubah Kapasitas
            Console.Write($"Kapasitas lama: {kelas.KapasitasKelas}\nKapasitas baru (1-100): ");
            string kapasitasInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(kapasitasInput))
            {
                if (int.TryParse(kapasitasInput, out int kapasitasBaru) && kapasitasBaru >= 1 && kapasitasBaru <= 100)
                {
                    if (kapasitasBaru >= kelas.JumlahPeserta)
                    {
                        kelas.KapasitasKelas = kapasitasBaru;
                    }
                    else
                    {
                        Console.WriteLine($"Kapasitas tidak boleh kurang dari jumlah peserta saat ini ({kelas.JumlahPeserta})!");
                    }
                }
            }

            Console.WriteLine("\n✓ Data kelas kuliah berhasil diubah!");
        }

        // DELETE: Menghapus kelas kuliah
        public void HapusKelasKuliah(string idProdi)
        {
            Console.Clear();
            Console.WriteLine("===== Hapus Kelas Kuliah =====");

            if (daftarKelasKuliah.Count == 0)
            {
                Console.WriteLine("Belum ada kelas kuliah untuk dihapus.");
                return;
            }

            DaftarKelasKuliah(idProdi);

            Console.Write("\nMasukkan Kode Kelas yang ingin dihapus: ");
            string kodeKelas = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();

            var kelas = daftarKelasKuliah.FirstOrDefault(k =>
                k.KodeKelas == kodeKelas &&
                string.Equals(k.IDProdi, idProdi, StringComparison.OrdinalIgnoreCase));

            if (kelas == null)
            {
                Console.WriteLine("Kode kelas tidak ditemukan di prodi ini!");
                return;
            }

            // Konfirmasi penghapusan
            Console.WriteLine($"\nAnda akan menghapus kelas: {kelas.NamaKelas} ({kelas.KodeKelas})");
            Console.WriteLine($"Mata Kuliah: {kelas.KodeMK}");
            Console.WriteLine($"Peserta saat ini: {kelas.JumlahPeserta}");
            Console.Write("\nApakah Anda yakin? (Y/N): ");
            string konfirmasi = Console.ReadLine()?.Trim().ToUpper();

            if (konfirmasi == "Y")
            {
                daftarKelasKuliah.Remove(kelas);
                Console.WriteLine("\n✓ Kelas kuliah berhasil dihapus!");
            }
            else
            {
                Console.WriteLine("\nPenghapusan dibatalkan.");
            }
        }
       
    }
}
