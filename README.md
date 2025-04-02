*ENGLISH VERSION*
------------
*BenimSalonum - Salon Management System*

Project Description*
------------
BenimSalonum is a comprehensive management system developed for beauty and care salons. 
With its user-friendly interface, multi-device session management, customer tracking, appointment planning, and financial reporting features, it enables salon operators to efficiently manage their daily operations.

*Features*
------------
- User Management and Security
- JWT-based authentication system
- Multi-device session management
- Ability for users to control sessions on other devices
- Two-factor authentication
- Comprehensive user profile management
- Role-based authorization system
- Salon Management
- Multiple branch management
- Staff and working hours tracking
- Stock and inventory management
- Service catalog and pricing control
- Customer Management
- Comprehensive customer information management
- Customer history and preferences tracking
- CRM features and loyalty programs
- Appointment System
- Drag-and-drop appointment scheduling
- Automatic appointment reminders
- Conflict control and availability tracking
- Online appointment integration
- Financial Management
- Sales and revenue tracking
- Cash register management
- E-Invoice integration
- Financial reporting
- System Management
- Automatic backup system
- System settings management
- SMS ande Email notification integration
- Language and localization support
- Project Structure
BenimSalonum is a solution developed using n-tier architecture:

- BenimSalonumDesktop: Windows Forms-based user interface
- BenimSalonumAPI: Web API services (RESTful API)
- BenimSalonum.Entities: Data models, DTOs, and validation rules
- BenimSalonum.Tools: Helper functions and tools
*Technologies*
------------
- .NET Core: Modern and high-performance platform
- Entity Framework Core: ORM (Object Relational Mapper)
- JWT: JSON Web Token based authentication
- Syncfusion: Modern UI controls (optional)
- SQL Server: Database management system
- Newtonsoft.Json: For JSON data Serialization/Deserialization
  
*Installation Guide*
------------
*Requirements*

- Visual Studio 2022 (or above)
- .NET Core 8.0 (or above)
- SQL Server 2019 (or above)
   
*Database Setup*
------------
- Open SQL Server Management Studio
- Create a new database: BenimSalonum
- Apply migrations from the project:
- CopyInsert
* dotnet ef database update --project BenimSalonumAPI *
Running the Project
- Open the BenimSalonumSolution.sln file with Visual Studio
Build the solution
- First run the API project, then the Desktop application
Configuration
- For API connection address and other settings:

- Edit the BenimSalonumDesktop/App.config file

- Configure API settings in the BenimSalonumAPI/appsettings.json file

*Screenshots*
------------
// UI INTERFACE DESIGN IN PROGRESS

*License*
------------
BenimSalonum is licensed for personal and commercial use. All rights reserved © 2025.

*Contact*
------------
+90 543 650 31 69

ak.emredemir@gmail.com

*Development Notes*
------------
- Future Features
- Mobile application integration
- Online appointment system
- Marketing and promotion management
- Detailed analytics and reporting
- Contributing
  
If you would like to contribute to the project, please create a fork and send a pull request. All contributions are valuable.

*Version History*
------------
v1.0.0 (April 2025)

*TURKISH VERSION*
------------

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
- BenimSalonumSolution.sln dosyasını Visual Studio ile açın

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
