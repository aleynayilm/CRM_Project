# CRM Projesi (MVC)

Bu proje, ASP.NET Core MVC mimarisi kullanılarak geliştirilmiş bir **Müşteri İlişkileri Yönetim Sistemi (CRM)** uygulamasıdır. Proje, şirketleri, sözleşmeleri, iletişim kişilerini ve sistem günlüklerini yönetmek amacıyla tasarlanmıştır. Kullanıcılar, sistemde oturum açarak şirketlere ait bilgileri güncelleyebilir, yeni sözleşmeler ekleyebilir ve sistemde gerçekleşen işlemleri takip edebilirler.

## Özellikler
- **Şirket Yönetimi:** Yeni şirket ekleme, düzenleme ve silme.
- **Sözleşme Takibi:** Sözleşme başlangıç ve bitiş tarihlerini yönetme.
- **Kullanıcı Yetkilendirme:** Kullanıcı girişi ve kimlik doğrulama.
- **Loglama:** Kullanıcı hareketlerini ve sistemdeki değişiklikleri kayıt altına alma.
- **Otomatik Bildirimler:** Sözleşme sürelerinin bitimine yaklaşıldığında e-posta bildirimi gönderme.
- **Filtreleme ve Arama:** Kayıtlar üzerinde dinamik filtreleme ve arama yapabilme.

## Teknolojiler
- **Backend:** ASP.NET Core MVC, Entity Framework Core, C#
- **Frontend:** HTML, CSS, JavaScript, jQuery
- **Veritabanı:** Microsoft SQL Server
- **Kimlik Doğrulama:** ASP.NET Core Identity (Email tabanlı oturum açma)
- **Loglama:** log4net, Serilog
- **Arka Plan Görevleri:** Hangfire
- **E-posta Gönderimi:** SMTP protokolü


