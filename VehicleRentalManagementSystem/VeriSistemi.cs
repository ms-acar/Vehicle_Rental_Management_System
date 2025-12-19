using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

public static class VeriSistemi
{
    public static List<Arac> AracListesi = new List<Arac>();
    public static List<Rezervasyon> RezervasyonListesi = new List<Rezervasyon>();
    private const string DosyaYolu = "rezervasyonlar.json"; 
    private static JsonSerializerOptions options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };

    public static void VerileriYukle()
    {
        string[] markalar = { "BMW", "Mercedes", "Audi", "Volkswagen", "Toyota", "Honda", "Fiat", "Renault", "Ford", "Hyundai" };
        string[] modeller = { "M5", "C300", "A3", "Golf", "Corolla", "Civic", "Egea", "Clio", "Focus", "Tucson" };
        string[] siniflar = { "Sedan", "Sedan", "Sedan", "Hatchback", "Sedan", "Sedan", "Sedan", "Hatchback", "Station", "SUV" };
        double[] fiyatlar = { 2500, 3658, 4500, 1800, 1500, 1600, 1200, 1100, 1400, 5000 };

        for (int i = 0; i < 10; i++)
        {
            Arac a = new Arac();
            a.Marka = markalar[i]; a.Model = modeller[i]; a.Sinif = siniflar[i]; a.GunlukFiyat = fiyatlar[i];
            a.Plaka = "34CS" + (i + 100); a.KiralamaSayisi = 0;
            AracListesi.Add(a);
        }

        if (File.Exists(DosyaYolu))
        {
            try
            {
                string kayitliVeri = File.ReadAllText(DosyaYolu);
                var yuklenenler = JsonSerializer.Deserialize<List<Rezervasyon>>(kayitliVeri);
                if (yuklenenler != null) RezervasyonListesi = yuklenenler;
            }
            catch
            {
                RezervasyonListesi = new List<Rezervasyon>();
            }
        }
    }
    public static void VerileriKaydet()
    {
        string jsonString = JsonSerializer.Serialize(RezervasyonListesi);
        File.WriteAllText(DosyaYolu, jsonString);
    }
    public static bool AracMusaitMi(string plaka, DateTime bas, DateTime bit)
    {
        foreach (var rez in RezervasyonListesi)
        {
            if (rez.Plaka == plaka && (bas < rez.Bitis && bit > rez.Baslangic))
                return false;
        }
        return true;
    }
    public static double RezervasyonUcretiHesapla(string plaka, DateTime bas, DateTime bit)
    {
        double gunluk = AracGunlukFiyatiniGetir(plaka);
        int gun = (bit - bas).Days;
        return gun > 0 ? gun * gunluk : 0;
    }
    public static double AracGunlukFiyatiniGetir(string plaka)
    {
        foreach (var a in AracListesi) if (a.Plaka == plaka) return a.GunlukFiyat;
        return 0;
    }


    public static void RezervasyonEkle(string musteri, string plaka, DateTime bas, DateTime bit)
    {
        if (AracMusaitMi(plaka, bas, bit))
        {
            Rezervasyon r = new Rezervasyon();
            r.Musteri = musteri; r.Plaka = plaka; r.Baslangic = bas; r.Bitis = bit;
            r.ToplamUcret = RezervasyonUcretiHesapla(plaka, bas, bit);
            RezervasyonListesi.Add(r);
            VerileriKaydet();

            foreach (var a in AracListesi) if (a.Plaka == plaka) a.KiralamaSayisi++;
            Console.WriteLine("Rezervasyon İşlemi Başarılı!");
            FaturaYazdir(r);
        }
        else Console.WriteLine("Hata: Araç Müsait Değil!");
    }

    public static List<string> MusaitAraclariGetir(DateTime bas, DateTime bit)
    {
        List<string> liste = new List<string>();
        foreach (var a in AracListesi)
            if (AracMusaitMi(a.Plaka, bas, bit)) liste.Add(a.Plaka + " - " + a.Marka);
        return liste;
    }

    public static void RezervasyonIptal(string plaka)
    {
        Rezervasyon sil = null;
        foreach (var r in RezervasyonListesi) if (r.Plaka == plaka) sil = r;
        if (sil != null) RezervasyonListesi.Remove(sil);
    }

    public static double ToplamGelir()
    {
        double t = 0;
        foreach (var r in RezervasyonListesi) t += r.ToplamUcret;
        return t;
    }

    public static List<string> MusteriRezervasyonlariniGetir(string musteri)
    {
        List<string> sonuclar = new List<string>();
        foreach (var r in RezervasyonListesi)
            if (r.Musteri.ToLower() == musteri.ToLower()) sonuclar.Add(r.Plaka);
        return sonuclar;
    }

    public static string EnCokKiralananArac()
    {
        Arac enCok = null; int max = -1;
        foreach (var a in AracListesi)
            if (a.KiralamaSayisi > max) { max = a.KiralamaSayisi; enCok = a; }
        return enCok != null ? enCok.Marka + " " + enCok.Model : "Yok";
    }
    public static Arac AracGetir(string plaka)
    {
        foreach (var a in AracListesi)
            if (a.Plaka == plaka)
                return a;

        return null;
    }
    public static void RezervasyonAra(string aramaKelimesi)
    {
    Console.WriteLine("\n--- Arama Sonuçları ---");
    bool bulundu = false;
        if (string.IsNullOrEmpty(aramaKelimesi)) return;

        foreach (var r in RezervasyonListesi)
        {
            bool isimUyumu = r.Musteri != null && r.Musteri.Contains(aramaKelimesi, StringComparison.OrdinalIgnoreCase);
            bool plakaUyumu = r.Plaka != null && r.Plaka.Contains(aramaKelimesi, StringComparison.OrdinalIgnoreCase);

            if (isimUyumu || plakaUyumu)
            {
                Console.WriteLine($"Müşteri: {r.Musteri} | Plaka: {r.Plaka} | Tutar: {r.ToplamUcret} TL");
                bulundu = true;
            }
        }
        if (!bulundu) Console.WriteLine("Eşleşen kayıt bulunamadı.");
    }
    public static void FaturaYazdir(Rezervasyon r)
    {
        Console.WriteLine("\n========== FATURA ==========");
        Console.WriteLine("Müşteri Adı   : " + r.Musteri);
        r.Arac = AracGetir(r.Plaka);
        if (r.Arac != null) {
            Console.WriteLine("Araç Modeli  : " + r.Arac.Marka + " " + r.Arac.Model);
            Console.WriteLine("Araç Sınıfı  : " + r.Arac.Sinif);
        }
        else
            Console.WriteLine("Lütfen Geçerli Bir Araç Marka-Modeli Giriniz!");
        Console.WriteLine("Araç Plakası  : " + r.Plaka);
        Console.WriteLine("Başlangıç     : " + r.Baslangic.ToShortDateString());
        Console.WriteLine("Bitiş         : " + r.Bitis.ToShortDateString());
        Console.WriteLine("Gün Sayısı    : " + (r.Bitis - r.Baslangic).Days);
        Console.WriteLine("Toplam Tutar  : " + r.ToplamUcret + " TL");
        Console.WriteLine("============================\n");
    }

}