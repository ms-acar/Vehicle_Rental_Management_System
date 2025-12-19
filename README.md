# Vehicle Rental Management System
MUHAMMED SALİH ACAR - TECHCAREER.NET C# KURS TAMAMLAMA PROJESİ - ARAÇ KİRALAMA REZERVASYON SİSTEMİ

Bu .NET konsol uygulaması, C# Console Application kullanılarak geliştirilmiş, gerçek hayat iş akışına uygun bir araç kiralama yönetim yazılımıdır. Proje, modüler bir yapı üzerine kurulu olup nesne yönelimli programlama prensiplerini ve kalıcı veri depolama özelliklerini içermektedir.

👥 Grup Üyeleri

Muhammed Salih Acar

🛠️ Teknik Özellikler:

Bu proje, bir araç kiralama firmasının ihtiyaç duyabileceği tüm temel ve ileri düzey fonksiyonları bünyesinde barındırır:

1. Temel Fonksiyonlar (Zorunlu Set)

Araç Yönetimi: 10 farklı araç (Sedan, SUV, Hatchback) marka, model ve günlük fiyat bilgileriyle sistemde tanımlıdır.


Müsaitlik Kontrolü: Tarih çakışmalarını engelleyen akıllı algoritma.



Ücret Hesaplama: Kiralama süresine göre otomatik fiyatlandırma.



Rezervasyon Yönetimi: Yeni rezervasyon ekleme ve mevcut rezervasyonları iptal etme yeteneği.



Raporlama: Toplam firma geliri ve en çok kiralanan araç raporları.


2. Bonus Özellikler (Ekstra Puan)

💾 JSON Kalıcı Kayıt: Program kapatılsa bile tüm rezervasyonlar rezervasyonlar.json dosyasına kaydedilir ve açılışta geri yüklenir.


🔍 Gelişmiş Arama: Müşteri adı veya araç plakasına göre geçmiş rezervasyonlar içinde hızlı arama.


📊 Firma Cirosu: Program açılış ekranında güncel toplam gelir ve toplam rezervasyon sayısı anlık olarak raporlanır.

📑 Fatura Sistemi: Her başarılı rezervasyon sonrası detaylı bir kiralama faturası ekrana yazdırılır.


🚙 Araç Sınıflandırması: Araçlar SUV, Sedan ve Hatchback gibi sınıflara ayrılarak yönetilir.

📁 Proje Yapısı (Modüler Mimari)
Kodlar, okunabilirliği artırmak ve bakımı kolaylaştırmak için ayrı .cs dosyalarında organize edilmiştir:


Program.cs: Menü tabanlı kullanıcı arayüzü ve ana döngü.


VeriSistemi.cs: Tüm iş mantığının, hesaplamaların ve dosya işlemlerinin yapıldığı beyin katmanı.


Arac.cs: Araç verilerini temsil eden model sınıfı.


Rezervasyon.cs: Rezervasyon verilerini temsil eden model sınıfı.

🚀 Nasıl Çalıştırılır?
Projeyi bilgisayarınıza indirin veya Visual Studio ile açın.

Newtonsoft.Json veya standart System.Text.Json kütüphanesinin referans edildiğinden emin olun.

Projeyi derleyin ve çalıştırın.

Ana menüdeki yönlendirmeleri takip ederek araçları listeleyebilir, rezervasyon yapabilir veya rapor alabilirsiniz.


Not: Bu proje, C# programlama eğitimindeki bitirme ödevi kapsamında geliştirilmiştir.
