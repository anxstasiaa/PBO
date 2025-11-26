using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PBO
{
    internal class Dosen
    {
        public string NIDN { get; set; }
        public string NamaDosen { get; set; }
        public string IDProdi { get; set; }    
        public string JenisKelamin { get; set; }
        public string JabatanFungsional { get; set; }

        // List of KodeMK this dosen is assigned to (store codes only)
        public List<string> MataKuliahDiampu { get; set; } = new List<string>();

        // Backwards-compatible parameterless method: falls back to code-only listing
        public void InputDosen()
        {
            // No full mata kuliah list provided — fall back to code-only output
            InputDosen(null);
        }

        // Integrated method: filter provided mata kuliah list by this Dosen.IDProdi and the KodeMK list
        public void InputDosen(IEnumerable<MataKuliah> semuaMataKuliah)
        {
            Console.WriteLine($"Dosen dengan NIDN {NIDN}, Nama {NamaDosen}, Prodi {IDProdi}, Jenis Kelamin {JenisKelamin}, Jabatan {JabatanFungsional}");
            Console.WriteLine("Mata Kuliah Diampu:");

            var kodeDiampu = new HashSet<string>(MataKuliahDiampu ?? Enumerable.Empty<string>(), StringComparer.OrdinalIgnoreCase);

            if (!kodeDiampu.Any())
            {
                Console.WriteLine("- (tidak ada mata kuliah diampu)");
                return;
            }

            if (semuaMataKuliah == null)
            {
                // No master list provided — show codes only
                foreach (var kode in kodeDiampu)
                    Console.WriteLine($"- {kode}");
                Console.WriteLine("(Tip: panggil InputDosen dengan daftar semua MataKuliah untuk melihat nama dan filter berdasarkan Prodi.)");
                return;
            }

            // Filter mata kuliah by kode yang diampu AND by same IDProdi as this Dosen
            var filtered = semuaMataKuliah
                .Where(m => kodeDiampu.Contains(m.KodeMK) && string.Equals(m.IDProdi, IDProdi, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filtered.Any())
            {
                Console.WriteLine("- (tidak ditemukan mata kuliah pada Prodi ini yang sesuai dengan daftar KodeMK)");
                // Optionally show codes that were in the list but belong to other prodi or missing
                var missingOrOtherProdi = kodeDiampu
                    .Except(filtered.Select(m => m.KodeMK), StringComparer.OrdinalIgnoreCase)
                    .ToList();
                if (missingOrOtherProdi.Any())
                {
                    Console.WriteLine("Kode yang tidak cocok atau berada di prodi lain:");
                    foreach (var kode in missingOrOtherProdi)
                        Console.WriteLine($"- {kode}");
                }
                return;
            }

            foreach (var mk in filtered)
            {
                Console.WriteLine($"- {mk.KodeMK} : {mk.NamaMK}");
            }
        }
    }
}
