using System;

namespace Project_PBO
{
    internal class Tagihan
    {
        public string NIM { get; set; }
        public string IDTagihan { get; set; }
        public string IDSemester { get; set; }
        public string PeriodePembayaran { get; set; }
        public double TotalPembayaran { get; set; }
        public string MetodePembayaran { get; set; }
        public System.DateTime? TanggalPembayaran { get; set; }
        public bool IsLunas { get; set; } = false;

        public void TampilkanInfoTagihan()
        {
            string status = IsLunas ? "Lunas" : "Belum Lunas";

            string tanggalBayar;

            if (TanggalPembayaran.HasValue)
                tanggalBayar = TanggalPembayaran.Value.ToString("dd/MM/yyyy");
            else
                tanggalBayar = "Belum Dibayar";

            string metodeBayar = string.IsNullOrWhiteSpace(MetodePembayaran)
                ? "N/A"
                : MetodePembayaran;

            Console.WriteLine(
                "ID Tagihan: " + IDTagihan +
                " | NIM: " + NIM +
                " | Semester: " + IDSemester +
                " | Jumlah: Rp " + TotalPembayaran.ToString("N0") +
                " | Status: " + status +
                " | Tgl Bayar: " + tanggalBayar +
                " | Metode: " + metodeBayar
            );
        }
    }
}
