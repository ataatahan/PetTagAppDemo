# 🐾 PetTag — Pet Takip ve Yönetim Sistemi

> Basit, temiz ve genişletilebilir bir evcil hayvan takip uygulaması. PetTag; sahipler, evcil hayvanlar, sağlık kayıtları ve veteriner randevularını yönetmenizi sağlar.

---

## ✨ Öne Çıkanlar

* ✅ Katmanlı mimari: `Core`, `Repo`, `Service`, `ConsoleDemo`
* 🧾 Zengin domain modelleri: Pet, PetOwner, HealthRecord, VetAppointment, PetChip vb.
* 🔧 Validation ve özel istisna yapıları
* ♻️ UnitOfWork ve repository pattern desteği

---

## 📁 Proje Yapısı (kısaca)

```
PetTagApp.Core/           # Domain nesneleri, entitiler, base entity
  ├─ BaseEntities/
  ├─ Entities/
  ├─ Enums/
  └─ Exceptions/

PetTag.Repo/               # Veri katmanı, Contexts, Configurations, UnitOfWork
  ├─ Concreties/
  ├─ Configurations/
  ├─ Contexts/
  ├─ Interfaces/
  └─ UnitOfWork/

PetTag.Service/            # Business logic, DTO'lar, Service arayüzleri
  ├─ Concreties/
  ├─ DTOs/
  └─ Interfaces/

PetTag.ConsoleDemo/        # Konsol uygulaması demo (Program.cs)

```

> Görsel notu: `PetTag.Core/Entities` klasöründe `Pet.cs`, `PetOwner.cs`, `Vet.cs`, `HealthRecord.cs`, `PetChip.cs` gibi sınıflar bulunmakta.

---

## 🧭 Özellikler

* Evcil hayvan oluşturma / güncelleme / silme
* Sahip bilgisi yönetimi
* Veteriner randevu takibi
* Sağlık kayıtları ve aşı takibi
* PetChip (çip) bilgisi takibi

---

## 🚀 Hızlı Başlangıç (Geliştirici)

**Gereksinimler**

* .NET 6 veya üzeri
* (İsteğe bağlı) Bir veritabanı (SQLite / SQL Server) — proje repo katmanında sağlayıcıya bağlı olarak ayarlanır

**Projeyi klonla**

```bash
git clone https://github.com/kullaniciadi/PetTagApp.git
cd PetTagApp
```

**Projeyi derle ve çalıştır (Console demo)**

```bash
dotnet build
cd PetTag.ConsoleDemo
dotnet run
```

> Not: Veritabanı bağlantı ayarlarını `PetTag.Repo/Contexts` içindeki `appsettings.json` veya ilgili configuration sınıfından düzenleyin.

---

## 🛠️ Kodlama Standartları & Konvansiyonlar

* Katmanlar arasında **DTO** kullanımı tercih edilir (Entity => DTO => Service).
* `UnitOfWork` ve `Repository` pattern'i ile transaction kontrolü sağlanır.
* Validation için özel `ValidationCheck` ve `CheckValueException` sınıfları kullanılmış.

---

## 📸 Ekran Görüntüleri

*Demo ve sınıf yapısını görmek için proje içindeki `screenshots/` klasörünü kullanabilirsiniz.*

---

## 🤝 Katkıda Bulunmak

1. Repo'yu fork'layın
2. Yeni bir branch açın: `feature/my-new-feature`
3. Değişikliklerinizi commit'leyin
4. Pull request oluşturun

Lütfen temiz commit mesajları ve küçük, anlaşılır PR'lar gönderin. Kod stiline uymaya dikkat edin.

---

## 👥 Geliştirici Ekip

Bu proje bir ekip çalışmasıdır. 💪

* 🧑‍💻 **Atahan Ata**
* 👨‍💻 **Berkay Ceylan**
* 👨‍💻 **Berkay Kurum**
* 👨‍💻 **Efe İkan**
* 👨‍💻 **Mert Sarıel**

> Ekip olarak PetTag uygulamasını birlikte geliştirdik; tasarım, veri modeli ve katmanlı mimari ortak bir plan doğrultusunda oluşturulmuştur.

---

## 🧾 Lisans

Bu proje MIT lisansı ile lisanslanmıştır. (LICENSE dosyası ekleyin.)

---

## ✉️ İletişim

Herhangi bir sorunuz veya öneriniz olursa `ata.han.ata@outlook.com` üzerinden bana ulaşabilirsiniz.

---

### 💡 İpuçları / Geliştirme Fikirleri

* RESTful API ekleyip `PetTag.Service` katmanını bir Web API ile expose edebilirsiniz.
* Unit testler ekleyin (xUnit / NUnit) — `Service` ve `Repo` katmanları için mock'lar kullanın.
* Docker ile veritabanı ve uygulama container'ları oluşturun.