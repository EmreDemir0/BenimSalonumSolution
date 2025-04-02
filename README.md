BenimSalonum - Salon Yönetim Sistemi

*Proje Açıklaması*
------------
BenimSalonum, güzellik ve bakım salonları için geliştirilmiş kapsamlı bir yönetim sistemidir. 
Kullanıcı dostu arayüzü, çok cihazlı oturum yönetimi, müşteri takibi, randevu planlama ve finans raporlama gibi özelliklerle salon işletmecilerinin günlük operasyonlarını verimli bir şekilde yönetmelerini sağlar.

*Özellikler*
------------
- Kullanıcı Yönetimi ve Güvenlik

- JWT tabanlı kimlik doğrulama sistemi

- Çoklu cihaz oturum yönetimi

- Kullanıcıların diğer cihazlardaki oturumları kontrol etme imkanı

- İki faktörlü kimlik doğrulama

- Kapsamlı kullanıcı profil yönetimi

- Rol tabanlı yetkilendirme sistemi

- Salon Yönetimi

- Birden fazla şube yönetimi

- Personel ve çalışma saatleri takibi

- Stok ve envanter yönetimi

- Hizmet kataloğu ve fiyatlandırma kontrolü

- Müşteri Yönetimi

- Kapsamlı müşteri bilgi yönetimi

- Müşteri geçmişi ve tercihleri takibi

- CRM özellikleri ve sadakat programları

- Randevu Sistemi

- Sürükle-bırak randevu planlama

- Otomatik randevu hatırlatmaları

- Çakışma kontrolü ve uygunluk takibi

- Online randevu entegrasyonu

- Finans Yönetimi

- Satış ve gelir takibi

- Kasa yönetimi

- E-Fatura entegrasyonu

- Finansal raporlama

- Sistem Yönetimi

- Otomatik yedekleme sistemi

- Sistem ayarları yönetimi

- SMS ve e-posta bildirim entegrasyonu

- Dil ve yerelleştirme desteği

*Proje Yapısı*
------------
- BenimSalonum, n-katmanlı mimari kullanılarak geliştirilmiş bir çözümdür:

- BenimSalonumDesktop: Windows Forms tabanlı kullanıcı arayüzü

- BenimSalonumAPI: Web API servisleri (RESTful API)

- BenimSalonum.Entities: Veri modelleri, DTOs ve validasyon kuralları

- BenimSalonum.Tools: Yardımcı fonksiyonlar ve araçlar

*Teknolojiler*
------------
- .NET Core: Modern ve yüksek performanslı platform

- Entity Framework Core: ORM (Nesne İlişkisel Eşleyici)

- JWT: JSON Web Token tabanlı kimlik doğrulama

- Syncfusion: Modern UI kontrolleri (opsiyonel)

- SQL Server: Veritabanı yönetim sistemi

- Newtonsoft.Json: JSON verileri için Serialization/Deserialization

*Kurulum Kılavuzu*
------------

*Gereksinimler*
------------
- Visual Studio 2022 (veya üzeri)

- .NET Core 8.0 (veya üzeri)

- SQL Server 2019 (veya üzeri)

*Veritabanı Kurulumu*
------------

- SQL Server Management Studio'yu açın

- Yeni bir veritabanı oluşturun: BenimSalonum

- Proje içindeki migration'ları uygulayın:

* dotnet ef database update --project BenimSalonumAPI *

*Projeyi Çalıştırma*
------------
-BenimSalonumSolution.sln dosyasını Visual Studio ile açın

Çözümü derleyin

- Önce API projesini, sonra Desktop uygulamasını çalıştırın

Konfigürasyon

- API bağlantı adresi ve diğer ayarlar için:

* BenimSalonumDesktop/App.config dosyasını düzenleyin

BenimSalonumAPI/appsettings.json dosyasında API ayarlarını yapılandırın *

*Ekran Görüntüleri*
------------

// UI ARAYÜZ TASARIM AŞAMASINDA

*Lisans*
------------
BenimSalonum, kişisel ve ticari kullanım için lisanslanmıştır. Tüm hakları saklıdır © 2025.

*İletişim*
------------
+90 543 650 31 69

ak.emredemir@gmail.com

*Geliştirme Notları*
------------


*Gelecek Özellikler*
------------
- Mobil uygulama entegrasyonu

- Online randevu sistemi

- Pazarlama ve promosyon yönetimi

- Detaylı analitik ve raporlama

*Katkıda Bulunma*
------------
Projeye katkıda bulunmak isterseniz, lütfen bir fork oluşturun ve pull request gönderin. Her türlü katkı değerlidir.

*Sürüm Geçmişi*
------------
v1.0.0 (Nisan 2025)
