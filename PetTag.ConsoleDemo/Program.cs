using System;
using System.Linq;
using System.Collections.Generic;
using PetTag.Repo.Contexts;
using PetTag.Repo.UnitOfWork;
using PetTag.Service.UnitOfWorks;
using PetTag.Service.DTOs;
using PetTag.Core.Enums;

namespace PetTag.ConsoleDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = CreateDbContext();
            context.Database.EnsureCreated();

            var uowRepo = new UnitOfWork(context);
            var svcs = new UnitOfWorkService(uowRepo);

            SeedIfEmpty(svcs);

            MainMenu(svcs);
        }

        // ------------------------ DB CONTEXT ------------------------
        private static PetTagAppDbContext CreateDbContext()
        {
            return new PetTagAppDbContext(); // Projende OnConfiguring ile bağlantı kurulmalı
        }

        private static void SeedIfEmpty(IUnitOfWorkService s)
        {
            if (!s.PetOwners.GetAll().Any())
                s.PetOwners.Add(new PetOwnerCreateDto { FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet@example.com" });

            if (!s.Vets.GetAll().Any())
                s.Vets.Add(new VetCreateDto { FirstName = "Ayşe", LastName = "Kara", PhoneNumber = "05551234567" });

            if (!s.Pets.GetAll().Any())
            {
                var ownerId = s.PetOwners.GetAll().First().Id;
                var vetId = s.Vets.GetAll().First().Id;
                var petType = FirstEnumValue<PetType>();
                s.Pets.Add(new PetCreateDto { Name = "Boncuk", Type = petType, Age = 2, Weight = 12.3, PetOwnerId = ownerId, VetId = vetId });
            }
        }

        // ------------------------ MENÜ ------------------------
        private static void MainMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n=== PetTag Console ===");
                Console.WriteLine("1) PetOwner İşlemleri");
                Console.WriteLine("2) Vet İşlemleri");
                Console.WriteLine("3) Pet İşlemleri");
                Console.WriteLine("4) PetChip İşlemleri");
                Console.WriteLine("5) ActivityLog İşlemleri");
                Console.WriteLine("6) HealthRecord İşlemleri");
                Console.WriteLine("7) Alert İşlemleri");
                Console.WriteLine("8) VetAppointment İşlemleri");
                Console.WriteLine("0) Çıkış");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();

                try
                {
                    switch (c)
                    {
                        case "1": OwnersMenu(s); break;
                        case "2": VetsMenu(s); break;
                        case "3": PetsMenu(s); break;
                        case "4": ChipsMenu(s); break;
                        case "5": ActivityMenu(s); break;
                        case "6": HealthMenu(s); break;
                        case "7": AlertsMenu(s); break;
                        case "8": AppointmentsMenu(s); break;
                        case "0": return;
                        default: Console.WriteLine("Geçersiz seçim."); break;
                    }
                }
                catch (Exception ex)
                {
                    Yellow($"Hata: {ex.Message}");
                }
            }
        }

        // ------------------------ OWNERS ------------------------
        private static void OwnersMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[Owners]");
                Console.WriteLine("1) Listele");
                Console.WriteLine("2) Ekle");
                Console.WriteLine("3) Güncelle");
                Console.WriteLine("4) Sil (Hard)");
                Console.WriteLine("5) Soft Delete");
                Console.WriteLine("6) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        foreach (var o in s.PetOwners.GetAll()) Console.WriteLine($"{o.Id} | {o.FullName} | {o.Email}");
                        break;
                    case "2":
                        var on = Read("Ad");
                        var ol = Read("Soyad");
                        var oe = Read("Email");
                        s.PetOwners.Add(new PetOwnerCreateDto { FirstName = on, LastName = ol, Email = oe });
                        Green("Owner eklendi.");
                        break;
                    case "3":
                        var oid = ReadInt("Owner Id");
                        s.PetOwners.Update(oid, new PetOwnerUpdateDto
                        {
                            FirstName = ReadOptional("Yeni Ad"),
                            LastName = ReadOptional("Yeni Soyad"),
                            Email = ReadOptional("Yeni Email")
                        });
                        Green("Owner güncellendi.");
                        break;
                    case "4":
                        s.PetOwners.Delete(ReadInt("Owner Id"));
                        Green("Owner silindi (hard).");
                        break;
                    case "5":
                        s.PetOwners.SoftDelete(ReadInt("Owner Id"));
                        Green("Owner soft delete.");
                        break;
                    case "6":
                        s.PetOwners.UndoDelete(ReadInt("Owner Id"));
                        Green("Owner undo delete.");
                        break;
                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ VETS ------------------------
        private static void VetsMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[Vets]");
                Console.WriteLine("1) Listele");
                Console.WriteLine("2) Ekle");
                Console.WriteLine("3) Güncelle");
                Console.WriteLine("4) Sil (Hard)");
                Console.WriteLine("5) Soft Delete");
                Console.WriteLine("6) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        foreach (var v in s.Vets.GetAll()) Console.WriteLine($"{v.Id} | {v.FullName} | {v.PhoneNumber}");
                        break;
                    case "2":
                        var vf = Read("Ad");
                        var vl = Read("Soyad");
                        var vp = Read("Telefon (11 hane)");
                        s.Vets.Add(new VetCreateDto { FirstName = vf, LastName = vl, PhoneNumber = vp });
                        Green("Vet eklendi.");
                        break;
                    case "3":
                        var vid = ReadInt("Vet Id");
                        s.Vets.Update(vid, new VetUpdateDto
                        {
                            FirstName = ReadOptional("Yeni Ad"),
                            LastName = ReadOptional("Yeni Soyad"),
                            PhoneNumber = ReadOptional("Yeni Telefon")
                        });
                        Green("Vet güncellendi.");
                        break;
                    case "4": s.Vets.Delete(ReadInt("Vet Id")); Green("Vet silindi."); break;
                    case "5": s.Vets.SoftDelete(ReadInt("Vet Id")); Green("Soft delete."); break;
                    case "6": s.Vets.UndoDelete(ReadInt("Vet Id")); Green("Undo delete."); break;
                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ PETS ------------------------
        private static void PetsMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[Pets]");
                Console.WriteLine("1) Listele");
                Console.WriteLine("2) Ekle");
                Console.WriteLine("3) Güncelle");
                Console.WriteLine("4) Sil (Hard)");
                Console.WriteLine("5) Soft Delete");
                Console.WriteLine("6) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        foreach (var p in s.Pets.GetAll())
                            Console.WriteLine($"{p.Id} | {p.Name} | {p.Type} | Age={p.Age} | Weight={p.Weight} | HasChip={p.HasChip}");
                        break;
                    case "2":
                        var name = Read("Ad");
                        var type = ReadEnumChoice<PetType>("PetType");
                        var age = ReadInt("Yaş");
                        var wt = ReadDoubleOptional("Kilo (boş geçilebilir)");
                        var ownerId = PickId("Owner", s.PetOwners.GetAll().Select(o => (o.Id, o.FullName)));
                        var vetId = PickId("Vet", s.Vets.GetAll().Select(v => (v.Id, v.FullName)));
                        s.Pets.Add(new PetCreateDto { Name = name, Type = type, Age = age, Weight = wt, PetOwnerId = ownerId, VetId = vetId });
                        Green("Pet eklendi.");
                        break;
                    case "3":
                        var pid = ReadInt("Pet Id");
                        s.Pets.Update(pid, new PetUpdateDto
                        {
                            Name = ReadOptional("Yeni Ad"),
                            Type = ReadEnumChoiceOptional<PetType>("PetType (boş geçilebilir)"),
                            Age = ReadIntOptional("Yeni Yaş"),
                            Weight = ReadDoubleOptional("Yeni Kilo"),
                            PetOwnerId = ReadIntOptional("Yeni OwnerId"),
                            VetId = ReadIntOptional("Yeni VetId")
                        });
                        Green("Pet güncellendi.");
                        break;
                    case "4": s.Pets.Delete(ReadInt("Pet Id")); Green("Pet silindi."); break;
                    case "5": s.Pets.SoftDelete(ReadInt("Pet Id")); Green("Soft delete."); break;
                    case "6": s.Pets.UndoDelete(ReadInt("Pet Id")); Green("Undo delete."); break;
                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ PET CHIPS ------------------------
        private static void ChipsMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[PetChips]");
                Console.WriteLine("1) Pet için Chip Oluştur");
                Console.WriteLine("2) PetId ile Chip Göster");
                Console.WriteLine("3) Chip Id ile Durum Değiştir (Aktif/Pasif)");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        var petId = PickId("Pet", s.Pets.GetAll().Select(p => (p.Id, p.Name)));
                        s.PetChips.Add(new PetChipCreateDto { PetId = petId });
                        var chip1 = s.PetChips.GetByPetId(petId);
                        if (chip1.HasValue)
                            Console.WriteLine($"Chip oluşturuldu: #{chip1.Value.ChipNumber} | Status={chip1.Value.ChipStatus}");
                        else
                            Yellow("Chip oluşturulamadı / bulunamadı.");
                        break;

                    case "2":
                        var petId2 = ReadInt("PetId");
                        var chip2 = s.PetChips.GetByPetId(petId2);
                        if (chip2.HasValue)
                            Console.WriteLine($"Chip: Id={chip2.Value.Id} | #{chip2.Value.ChipNumber} | Status={chip2.Value.ChipStatus}");
                        else
                            Yellow("Chip yok.");
                        break;

                    case "3":
                        var chipEntityId = ReadInt("Chip Entity Id (PetChip.Id)");
                        var chipOpt = s.PetChips.Get(chipEntityId);
                        if (!chipOpt.HasValue) { Yellow("Chip bulunamadı."); break; }
                        var chip = chipOpt.Value;
                        if (chip.ChipStatus == ChipStatus.Active)
                        {
                            s.PetChips.Deactivate(chipEntityId);
                            Green("Pasif yapıldı.");
                        }
                        else
                        {
                            s.PetChips.Activate(chipEntityId);
                            Green("Aktif yapıldı.");
                        }
                        break;

                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ ACTIVITY LOGS ------------------------
        private static void ActivityMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[ActivityLogs]");
                Console.WriteLine("1) Pet'e Log Ekle");
                Console.WriteLine("2) Pet'e Ait Logları Listele");
                Console.WriteLine("3) Tarih Aralığına Göre Listele");
                Console.WriteLine("4) Log Güncelle");
                Console.WriteLine("5) Sil (Hard)");
                Console.WriteLine("6) Soft Delete");
                Console.WriteLine("7) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        var petId = PickId("Pet", s.Pets.GetAll().Select(p => (p.Id, p.Name)));
                        var dtoC = new ActivityLogCreateDto
                        {
                            PetId = petId,
                            WalkingMinutes = ReadDoubleOptional("Walking dk"),
                            RunningMinutes = ReadDoubleOptional("Running dk"),
                            SleepingMinutes = ReadDoubleOptional("Sleeping saat (0-24)"),
                            Temperature = ReadDoubleOptional("Sıcaklık"),
                            Distance = ReadDoubleOptional("Mesafe (km)"),
                            LogDate = ReadDateOptional("LogDate (yyyy-MM-dd HH:mm)")
                        };
                        s.ActivityLogs.Add(dtoC);
                        Green("Log eklendi.");
                        break;

                    case "2":
                        var petId2 = ReadInt("PetId");
                        var logs = s.ActivityLogs.GetAllByPet(petId2);
                        foreach (var l in logs)
                            Console.WriteLine($"{l.Id} | {l.LogDate:g} | Walk={l.WalkingMinutes} | Run={l.RunningMinutes} | Temp={l.Temperature} | Dist={l.Distance}");
                        break;

                    case "3":
                        var start = ReadDate("Başlangıç (yyyy-MM-dd)");
                        var end = ReadDate("Bitiş (yyyy-MM-dd)");
                        var range = s.ActivityLogs.GetByDateRange(start, end);
                        foreach (var l in range)
                            Console.WriteLine($"{l.Id} | PetId={l.PetId} | {l.LogDate:g}");
                        break;

                    case "4":
                        var idU = ReadInt("Log Id");
                        s.ActivityLogs.Update(idU, new ActivityLogUpdateDto
                        {
                            WalkingMinutes = ReadDoubleOptional("Yeni Walking dk"),
                            RunningMinutes = ReadDoubleOptional("Yeni Running dk"),
                            SleepingMinutes = ReadDoubleOptional("Yeni Sleeping saat"),
                            Temperature = ReadDoubleOptional("Yeni Sıcaklık"),
                            Distance = ReadDoubleOptional("Yeni Mesafe"),
                            LogDate = ReadDateOptional("Yeni LogDate")
                        });
                        Green("Log güncellendi.");
                        break;

                    case "5": s.ActivityLogs.Delete(ReadInt("Log Id")); Green("Silindi."); break;
                    case "6": s.ActivityLogs.SoftDelete(ReadInt("Log Id")); Green("Soft delete."); break;
                    case "7": s.ActivityLogs.UndoDelete(ReadInt("Log Id")); Green("Undo delete."); break;

                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ HEALTH RECORDS ------------------------
        private static void HealthMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[HealthRecords]");
                Console.WriteLine("1) Ekle");
                Console.WriteLine("2) Pet'e Göre Listele");
                Console.WriteLine("3) Tarih Aralığı Listele");
                Console.WriteLine("4) Aşı Kayıtlarını Listele");
                Console.WriteLine("5) Güncelle");
                Console.WriteLine("6) Sil (Hard)");
                Console.WriteLine("7) Soft Delete");
                Console.WriteLine("8) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        var petId = PickId("Pet", s.Pets.GetAll().Select(p => (p.Id, p.Name)));
                        s.HealthRecords.Add(new HealtRecordCreateDto
                        {
                            PetId = petId,
                            Description = Read("Açıklama"),
                            RecordDate = ReadDate("Tarih (yyyy-MM-dd)"),
                            IsVaccination = YesNo("Aşı kaydı mı? (E/H)"),
                            Treatment = ReadOptional("Tedavi (ops.)")
                        });
                        Green("HealthRecord eklendi.");
                        break;

                    case "2":
                        var pid = ReadInt("PetId");
                        var list = s.HealthRecords.GetAllByPet(pid);
                        foreach (var hr in list)
                            Console.WriteLine($"{hr.Id} | {hr.RecordDate:d} | Vacc={hr.IsVaccination} | Desc={hr.Description}");
                        break;

                    case "3":
                        var start = ReadDate("Başlangıç (yyyy-MM-dd)");
                        var end = ReadDate("Bitiş (yyyy-MM-dd)");
                        foreach (var hr in s.HealthRecords.GetByDateRange(start, end))
                            Console.WriteLine($"{hr.Id} | PetId={hr.PetId} | {hr.RecordDate:d}");
                        break;

                    case "4":
                        foreach (var hr in s.HealthRecords.GetVaccinations())
                            Console.WriteLine($"{hr.Id} | PetId={hr.PetId} | {hr.RecordDate:d} | Vacc=YES");
                        break;

                    case "5":
                        var idU = ReadInt("HR Id");
                        s.HealthRecords.Update(idU, new HealtRecordUpdateDto
                        {
                            Description = ReadOptional("Yeni Açıklama"),
                            RecordDate = ReadDateOptional("Yeni Tarih"),
                            IsVaccination = ReadBoolOptional("Yeni Vacc? (E/H)"),
                            Treatment = ReadOptional("Yeni Tedavi")
                        });
                        Green("Güncellendi.");
                        break;

                    case "6": s.HealthRecords.Delete(ReadInt("HR Id")); Green("Silindi."); break;
                    case "7": s.HealthRecords.SoftDelete(ReadInt("HR Id")); Green("Soft delete."); break;
                    case "8": s.HealthRecords.UndoDelete(ReadInt("HR Id")); Green("Undo delete."); break;

                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ ALERTS ------------------------
        private static void AlertsMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[Alerts]");
                Console.WriteLine("1) Ekle");
                Console.WriteLine("2) Pet'e Göre Listele");
                Console.WriteLine("3) Türe Göre Listele");
                Console.WriteLine("4) Son X Gün");
                Console.WriteLine("5) Sil (Hard)");
                Console.WriteLine("6) Soft Delete");
                Console.WriteLine("7) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        var petId = PickId("Pet", s.Pets.GetAll().Select(p => (p.Id, p.Name)));
                        var at = ReadEnumChoice<AlertType>("AlertType");
                        var msg = Read("Mesaj");
                        s.Alerts.Add(new AlertCreateDto { PetId = petId, AlertType = at, Message = msg });
                        Green("Alert eklendi.");
                        break;

                    case "2":
                        var pid = ReadInt("PetId");
                        foreach (var a in s.Alerts.GetAllByPet(pid))
                            Console.WriteLine($"{a.Id} | {a.AlertDate:g} | {a.AlertType} | {a.Message}");
                        break;

                    case "3":
                        var type = ReadEnumChoice<AlertType>("AlertType");
                        foreach (var a in s.Alerts.GetByType(type))
                            Console.WriteLine($"{a.Id} | PetId={a.PetId} | {a.AlertDate:g} | {a.Message}");
                        break;

                    case "4":
                        var d = ReadInt("Kaç gün?");
                        foreach (var a in s.Alerts.GetRecent(d))
                            Console.WriteLine($"{a.Id} | {a.AlertDate:g} | {a.Message}");
                        break;

                    case "5": s.Alerts.Delete(ReadInt("Alert Id")); Green("Silindi."); break;
                    case "6": s.Alerts.SoftDelete(ReadInt("Alert Id")); Green("Soft delete."); break;
                    case "7": s.Alerts.UndoDelete(ReadInt("Alert Id")); Green("Undo delete."); break;

                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ APPOINTMENTS ------------------------
        private static void AppointmentsMenu(IUnitOfWorkService s)
        {
            while (true)
            {
                Console.WriteLine("\n[Appointments]");
                Console.WriteLine("1) Ekle");
                Console.WriteLine("2) Pet'e Göre Listele");
                Console.WriteLine("3) Vet'e Göre Listele");
                Console.WriteLine("4) Tarih Aralığı Listele");
                Console.WriteLine("5) Güncelle");
                Console.WriteLine("6) Sil (Hard)");
                Console.WriteLine("7) Soft Delete");
                Console.WriteLine("8) Undo Delete");
                Console.WriteLine("0) Geri");
                Console.Write("> Seçim: ");
                var c = Console.ReadLine();
                if (c == "0") return;

                switch (c)
                {
                    case "1":
                        var petId = PickId("Pet", s.Pets.GetAll().Select(p => (p.Id, p.Name)));
                        var vetId = PickId("Vet", s.Vets.GetAll().Select(v => (v.Id, v.FullName)));
                        var when = ReadDateTime("Randevu (yyyy-MM-dd HH:mm)");
                        var notes = ReadOptional("Not (ops.)");
                        s.VetAppointments.Add(new VetAppointmentCreateDto { PetId = petId, VetId = vetId, AppointmentDate = when, Notes = notes });
                        Green("Randevu eklendi.");
                        break;

                    case "2":
                        var pid = ReadInt("PetId");
                        foreach (var a in s.VetAppointments.GetAllByPet(pid))
                            Console.WriteLine($"{a.Id} | {a.AppointmentDate:g} | VetId={a.VetId} | Notes={a.Notes}");
                        break;

                    case "3":
                        var vid = ReadInt("VetId");
                        foreach (var a in s.VetAppointments.GetAllByVet(vid))
                            Console.WriteLine($"{a.Id} | {a.AppointmentDate:g} | PetId={a.PetId} | Notes={a.Notes}");
                        break;

                    case "4":
                        var start = ReadDateTime("Başlangıç (yyyy-MM-dd HH:mm)");
                        var end = ReadDateTime("Bitiş (yyyy-MM-dd HH:mm)");
                        foreach (var a in s.VetAppointments.GetByDateRange(start, end))
                            Console.WriteLine($"{a.Id} | {a.AppointmentDate:g} | PetId={a.PetId} | VetId={a.VetId}");
                        break;

                    case "5":
                        var idU = ReadInt("Randevu Id");
                        s.VetAppointments.Update(idU, new VetAppointmentUpdateDto
                        {
                            AppointmentDate = ReadDateTimeOptional("Yeni Tarih-Saat"),
                            VetId = ReadIntOptional("Yeni VetId"),
                            PetId = ReadIntOptional("Yeni PetId"),
                            Notes = ReadOptional("Yeni Not")
                        });
                        Green("Güncellendi.");
                        break;

                    case "6": s.VetAppointments.Delete(ReadInt("Id")); Green("Silindi."); break;
                    case "7": s.VetAppointments.SoftDelete(ReadInt("Id")); Green("Soft delete."); break;
                    case "8": s.VetAppointments.UndoDelete(ReadInt("Id")); Green("Undo delete."); break;

                    default: Console.WriteLine("Geçersiz."); break;
                }
            }
        }

        // ------------------------ HELPERS ------------------------
        private static string Read(string label)
        {
            Console.Write($"{label}: ");
            return Console.ReadLine() ?? "";
        }

        private static string? ReadOptional(string label)
        {
            Console.Write($"{label}: ");
            var s = Console.ReadLine();
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }

        private static int ReadInt(string label)
        {
            Console.Write($"{label}: ");
            return int.TryParse(Console.ReadLine(), out var v) ? v : 0;
        }

        private static int? ReadIntOptional(string label)
        {
            Console.Write($"{label}: ");
            return int.TryParse(Console.ReadLine(), out var v) ? v : (int?)null;
        }

        private static double? ReadDoubleOptional(string label)
        {
            Console.Write($"{label}: ");
            return double.TryParse(Console.ReadLine(), out var v) ? v : (double?)null;
        }

        private static DateTime ReadDate(string label)
        {
            Console.Write($"{label}: ");
            return DateTime.TryParse(Console.ReadLine(), out var v) ? v : DateTime.Today;
        }

        private static DateTime ReadDateTime(string label)
        {
            Console.Write($"{label}: ");
            return DateTime.TryParse(Console.ReadLine(), out var v) ? v : DateTime.Now;
        }

        private static DateTime? ReadDateOptional(string label)
        {
            Console.Write($"{label}: ");
            return DateTime.TryParse(Console.ReadLine(), out var v) ? v : (DateTime?)null;
        }

        private static DateTime? ReadDateTimeOptional(string label)
        {
            Console.Write($"{label}: ");
            return DateTime.TryParse(Console.ReadLine(), out var v) ? v : (DateTime?)null;
        }

        private static bool YesNo(string label)
        {
            Console.Write($"{label}: ");
            var s = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
            return s == "E" || s == "EVET" || s == "Y";
        }

        private static bool? ReadBoolOptional(string label)
        {
            Console.Write($"{label}: ");
            var s = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
            if (string.IsNullOrEmpty(s)) return null;
            return s == "E" || s == "EVET" || s == "Y";
        }

        private static TEnum FirstEnumValue<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().First();
        }

        private static TEnum ReadEnumChoice<TEnum>(string title) where TEnum : struct, Enum
        {
            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));
            Console.WriteLine($"{title} seçenekleri:");
            for (int i = 0; i < names.Length; i++) Console.WriteLine($"{i} = {names[i]}");
            Console.Write("Seçiminiz (index): ");
            var ok = int.TryParse(Console.ReadLine(), out var idx);
            if (!ok || idx < 0 || idx >= values.Length) idx = 0;
            return values[idx];
        }

        private static TEnum? ReadEnumChoiceOptional<TEnum>(string title) where TEnum : struct, Enum
        {
            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));
            Console.WriteLine($"{title} seçenekleri (boş geçilebilir):");
            for (int i = 0; i < names.Length; i++) Console.WriteLine($"{i} = {names[i]}");
            Console.Write("Seçiminiz (index / boş): ");
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) return null;
            var ok = int.TryParse(s, out var idx);
            if (!ok || idx < 0 || idx >= values.Length) return null;
            return values[idx];
        }

        private static int PickId(string title, IEnumerable<(int Id, string Name)> items)
        {
            var arr = items.ToArray();
            Console.WriteLine($"{title} listesi:");
            foreach (var x in arr) Console.WriteLine($"{x.Id} = {x.Name}");
            Console.Write($"{title} Id: ");
            return int.TryParse(Console.ReadLine(), out var v) ? v : arr.First().Id;
        }

        private static void Green(string s)
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(s); Console.ResetColor();
        }

        private static void Yellow(string s)
        {
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(s); Console.ResetColor();
        }
    }
}

