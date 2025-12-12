# 🐾 PetTag — Pet Takip ve Yönetim Sistemi

> Basit, temiz ve genişletilebilir bir evcil hayvan takip uygulaması. PetTag; sahipler, evcil hayvanlar, sağlık kayıtları ve veteriner randevularını yönetmenizi sağlar.

---

## ✨ Öne Çıkanlar

* ✅ **Katmanlı mimari**: `PetTagApp` (Core), `PetTag.Repo`, `PetTag.Service`, `PetTag` (Web)
* 🌐 **ASP.NET Core MVC** web uygulaması
* 🧾 **Zengin domain modelleri**: Pet, PetOwner, Vet, HealthRecord, VetAppointment, PetChip, Alert, ActivityLog
* 🧩 **SOLID prensiplerine uygun** olarak geliştirilmiş mimari yapı
* ⚙️ **Repository Pattern** ve **Unit of Work** kullanımı
* 🔧 **Validation**, özel exception yapıları ve temiz kod prensipleri
* ♻️ Esnek ve test edilebilir yapı (Dependency Injection destekli)
* 🗄️ **Entity Framework Core 6.0.35** ile SQL Server entegrasyonu

---

## 🛠️ Teknoloji Stack'i

* **.NET 9.0**
* **ASP.NET Core MVC**
* **Entity Framework Core 6.0.35**
* **SQL Server**
* **Dependency Injection**
* **Repository Pattern**
* **Unit of Work Pattern**

---

## 📁 Proje Yapısı

```
PetTagAppDemo/
├── PetTagApp/                    # Domain katmanı (Core)
│   ├── BaseEntities/             # BaseEntity sınıfı
│   ├── Entities/                 # Domain entity'leri
│   │   ├── Pet.cs
│   │   ├── PetOwner.cs
│   │   ├── Vet.cs
│   │   ├── VetAppointment.cs
│   │   ├── HealtRecord.cs
│   │   ├── PetChip.cs
│   │   ├── Alert.cs
│   │   └── ActivityLog.cs
│   ├── Enums/                    # Enum tanımlamaları
│   └── Exceptions/               # Özel exception sınıfları
│
├── PetTag.Repo/                  # Veri erişim katmanı
│   ├── Concreties/               # Repository implementasyonları
│   ├── Configurations/           # EF Core entity konfigürasyonları
│   ├── Contexts/                 # DbContext
│   ├── Interfaces/               # Repository arayüzleri
│   ├── Migrations/               # Veritabanı migration'ları
│   └── UnitOfWork/               # UnitOfWork implementasyonu
│
├── PetTag.Service/               # İş mantığı katmanı
│   ├── Concreties/               # Service implementasyonları
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Interfaces/               # Service arayüzleri
│   └── UnitOfWorks/              # Service UnitOfWork
│
└── PetTag/                       # Web uygulaması (ASP.NET Core MVC)
    ├── Controllers/              # MVC Controller'lar
    ├── Views/                    # Razor view'lar
    ├── Models/                   # View modelleri
    ├── wwwroot/                  # Statik dosyalar
    └── Program.cs                # Uygulama başlangıç noktası
```

---

## 🧭 Özellikler

### Evcil Hayvan Yönetimi
* Evcil hayvan oluşturma / güncelleme / silme
* Pet tipi, yaş, ağırlık bilgileri
* Pet-çip ilişkilendirme

### Sahip Yönetimi
* Pet sahibi bilgileri (ad, soyad, e-posta)
* Sahip-pet ilişkileri

### Veteriner Yönetimi
* Veteriner bilgileri
* Veteriner randevu takibi
* Randevu geçmişi

### Sağlık Kayıtları
* Sağlık kayıtları ve aşı takibi
* Sağlık geçmişi görüntüleme

### Bildirimler ve Loglar
* Alert (uyarı) sistemi
* Aktivite logları
* Sistem olay takibi

---

## 🧱 Yazılım Mimarisi

Bu proje **katmanlı mimari** yapısına sahiptir ve **SOLID prensipleri** gözetilerek tasarlanmıştır:

### ⚙️ SOLID İlkeleri

* **S (Single Responsibility Principle):** Her sınıf tek bir sorumluluğa sahiptir. Örneğin, `PetService` yalnızca evcil hayvan işlemlerini yönetir.
* **O (Open/Closed Principle):** Sınıflar genişletmeye açık, değişikliğe kapalı olacak şekilde tasarlanmıştır.
* **L (Liskov Substitution Principle):** Base sınıflar, türetilmiş sınıflarla sorunsuz şekilde değiştirilebilir.
* **I (Interface Segregation Principle):** Arayüzler küçük ve özelleşmiş tutulmuştur. Her servis kendi görevine uygun arayüzleri uygular.
* **D (Dependency Inversion Principle):** Üst seviye modüller, alt seviye modüllere değil, soyutlamalara bağımlıdır.

> Ayrıca proje genelinde **Dependency Injection**, **Repository Pattern** ve **UnitOfWork** kullanılarak bağımlılıklar yönetilebilir ve test edilebilir bir yapı sağlanmıştır.

---

## 🚀 Hızlı Başlangıç

### Gereksinimler

* **.NET 9.0 SDK** veya üzeri
* **SQL Server** (LocalDB veya SQL Server Express/Full)
* **Visual Studio 2022** veya **Visual Studio Code** (isteğe bağlı)

### Kurulum Adımları

1. **Projeyi klonlayın**

```bash
git clone https://github.com/kullaniciadi/PetTagAppDemo.git
cd PetTagAppDemo
```

2. **Veritabanı bağlantı ayarlarını yapılandırın**

`PetTag/appsettings.json` dosyasındaki connection string'i kendi SQL Server bilgilerinize göre düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=PetTagAppDemo41;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

3. **Veritabanı migration'larını uygulayın**

```bash
cd PetTag.Repo
dotnet ef database update --startup-project ../PetTag
```

veya Visual Studio'da Package Manager Console'dan:

```powershell
cd PetTag.Repo
Update-Database -StartupProject ../PetTag
```

4. **Projeyi derleyin**

```bash
dotnet build
```

5. **Uygulamayı çalıştırın**

```bash
cd PetTag
dotnet run
```

Uygulama `https://localhost:5001` veya `http://localhost:5000` adresinde çalışacaktır.

### Veritabanı Migration Oluşturma

Yeni bir migration oluşturmak için:

```bash
cd PetTag.Repo
dotnet ef migrations add MigrationAdi --startup-project ../PetTag
```

---

## 🛠️ Kodlama Standartları & Konvansiyonlar

* Katmanlar arasında **DTO** kullanımı tercih edilir (Entity => DTO => Service).
* `UnitOfWork` ve `Repository` pattern'i ile transaction kontrolü sağlanır.
* Validation için özel exception sınıfları kullanılmış (`InvalidPetNameException`, `InvalidPetAgeException` vb.).
* Entity'lerde property validation'ları setter'larda yapılmaktadır.
* `BaseEntity` sınıfı ile ortak özellikler (Id, CreateDate, UpdateDate, Status) yönetilmektedir.
* Temiz kod prensipleri ve sürdürülebilirlik ön planda tutulmuştur.
* Dependency Injection ile bağımlılıklar yönetilmektedir.

### Entity İlişkileri

* **Pet** ↔ **PetOwner**: Çoklu-1 (Bir pet'in bir sahibi, bir sahibin birden fazla pet'i)
* **Pet** ↔ **PetChip**: 1-1 (Bir pet'in bir çipi)
* **Pet** ↔ **Vet**: Çoklu-1 (Bir pet'in bir veterineri)
* **Pet** ↔ **VetAppointment**: 1-Çoklu (Bir pet'in birden fazla randevusu)
* **Pet** ↔ **HealtRecord**: 1-Çoklu (Bir pet'in birden fazla sağlık kaydı)
* **Pet** ↔ **Alert**: 1-Çoklu (Bir pet'in birden fazla uyarısı)
* **Pet** ↔ **ActivityLog**: 1-Çoklu (Bir pet'in birden fazla aktivite logu)

---

## 📋 Entity'ler ve Özellikleri

### Pet (Evcil Hayvan)
* Name, Age, Weight
* PetType (enum)
* PetOwner, Vet ilişkileri
* PetChip, VetAppointments, HealtRecords, Alerts, ActivityLogs koleksiyonları

### PetOwner (Pet Sahibi)
* FirstName, LastName, Email
* FullName (computed property)
* Pets koleksiyonu

### Vet (Veteriner)
* Veteriner bilgileri
* Pet ilişkileri

### VetAppointment (Veteriner Randevusu)
* Randevu tarih ve saat bilgileri
* Pet ve Vet ilişkileri

### HealtRecord (Sağlık Kaydı)
* Sağlık kayıt bilgileri
* Pet ilişkisi

### PetChip (Pet Çipi)
* Çip numarası ve durumu
* Pet ile 1-1 ilişki

### Alert (Uyarı)
* Uyarı tipi ve mesajı
* Pet ilişkisi

### ActivityLog (Aktivite Logu)
* Aktivite kayıtları
* Pet ilişkisi

---

## 🤝 Katkıda Bulunmak

1. Repo'yu fork'layın
2. Yeni bir branch açın: `feature/my-new-feature`
3. Değişikliklerinizi commit'leyin
4. Pull request oluşturun

Lütfen temiz commit mesajları ve küçük, anlaşılır PR'lar gönderin. Kod stiline uymaya dikkat edin.

---

## 📝 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

---

## 👥 Geliştirici Ekip

Bu proje bir ekip çalışmasıdır. 💪

* 🧑‍💻 **Atahan Ata**
* 👨‍💻 **Berkay Ceylan**
* 👨‍💻 **Berkay Kurum**
* 👨‍💻 **Efe İkan**
* 👨‍💻 **Mert Sarıel**

> Ekip olarak PetTag uygulamasını birlikte geliştirdik; tasarım, veri modeli, SOLID prensipleri ve katmanlı mimari ortak bir plan doğrultusunda oluşturulmuştur.

---

## ✉️ İletişim

Herhangi bir sorunuz veya öneriniz olursa "ata.han.ata@outlook.com" üzerinden bana ulaşabilirsiniz.

---