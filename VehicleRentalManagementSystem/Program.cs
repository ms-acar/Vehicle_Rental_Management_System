using System;

class Program
{
    static void Main(string[] args)
    {
        VeriSistemi.VerileriYukle();
        Console.WriteLine("--- Araç Rezervasyon Sistemine Hoşgeldiniz ---");

        while (true)
        {
            Console.WriteLine("\n1-Araçlar 2-Rezervasyon Yap 3-Raporlar & Firma Bilgisi 4-Kayıt Ara 0-Çıkış");
            string secim = Console.ReadLine();
            if (secim == "0") break;

            if (secim == "1")
            {
                foreach (var a in VeriSistemi.AracListesi)
                    Console.WriteLine($"{a.Plaka}: {a.Marka} {a.Model} - {a.GunlukFiyat} TL");
            }
            else if (secim == "2")
            {
                try
                {
                    Console.Write("Müşteri: "); string m = Console.ReadLine();
                    Console.Write("Plaka: "); string p = Console.ReadLine();
                    Console.Write("Başlangıç (GG.AA.YYYY): "); DateTime b = DateTime.Parse(Console.ReadLine());
                    Console.Write("Bitiş: "); DateTime bit = DateTime.Parse(Console.ReadLine());
                    VeriSistemi.RezervasyonEkle(m, p, b, bit);
                }
                catch { Console.WriteLine("Hatalı tarih formatı"); }
            }
            else if (secim == "3")
            {
                Console.WriteLine($" TOPLAM FİRMA CİROSU: {VeriSistemi.ToplamGelir()} TL");
                Console.WriteLine($" KAYITLI REZERVASYON: {VeriSistemi.RezervasyonListesi.Count}");
                Console.WriteLine("Toplam Gelir: " + VeriSistemi.ToplamGelir());
                Console.WriteLine("En Çok Kiralanan: " + VeriSistemi.EnCokKiralananArac());
            }
            else if (secim == "4")
            {

                Console.Write("Aramak istediğiniz müşteri adını veya plakayı (anahtar kelime) giriniz: ");
                string kelime = Console.ReadLine();

                VeriSistemi.RezervasyonAra(kelime);
            }
        }
    }
}