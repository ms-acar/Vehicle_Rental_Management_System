using System;

public class Rezervasyon
{
    public string Musteri { get; set; }
    public string Plaka { get; set; }

    public Arac Arac;
    public DateTime Baslangic { get; set; }
    public DateTime Bitis { get; set; }
    public double ToplamUcret { get; set; }

}