# 📊 Aplikacija za upravljanje projektima

Moderna web aplikacija za upravljanje projektima, timovima i resursima razvijena u .NET Core sa Razor Pages arhitekturom.

![.NET Core](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat-square&logo=sqlite)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=flat-square)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=flat-square&logo=bootstrap)

## 🎯 O projektu

Aplikacija omogućava kompletno upravljanje projektima kroz hijerarhijsku strukturu:
- **Projekti** → **Radni paketi** → **Zadaci** → **Aktivnosti**

Pored toga, aplikacija omogućava upravljanje zaposlenima, timovima, i dodeljivanje aktivnosti članovima tima.

## ✨ Funkcionalnosti

### 📁 Upravljanje projektima
- ✅ CRUD operacije za projekte
- ✅ Praćenje radnih paketa po projektima
- ✅ Upravljanje zadacima i aktivnostima
- ✅ Planiranje i praćenje sati/dana

### 👥 Upravljanje resursima
- ✅ Evidencija zaposlenih
- ✅ Kreiranje i upravljanje timovima
- ✅ Dodavanje članova u timove
- ✅ Dodeljivanje zadataka i aktivnosti zaposlenima

### 🔍 Dodatne mogućnosti
- ✅ Pretraga i sortiranje svih entiteta
- ✅ Praćenje ostvarenih vs planiranih sati
- ✅ Responzivni moderan UI dizajn
- ✅ Validacija unosa podataka

## 🛠️ Tehnologije

- **Backend:** .NET Core 8.0 (Razor Pages)
- **ORM:** Entity Framework Core 8.0
- **Baza podataka:** SQLite
- **Frontend:** Bootstrap 5.3, Bootstrap Icons
- **Migracije:** EF Core Migrations

## 📦 Instalacija i pokretanje

### Preduslovi

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) ili noviji

### Koraci za pokretanje

1. **Kloniranje repozitorijuma**
   ```bash
   git clone https://github.com/vase-korisnicko-ime/ProjectManagementApplication.git
   cd ProjectManagementApplication/ProjectManagementApplication/ProjectManagementApplication
   ```

2. **Restore NuGet paketa**
   ```bash
   dotnet restore
   ```

3. **Build projekta**
   ```bash
   dotnet build
   ```

4. **Pokretanje aplikacije**
   ```bash
   cd ProjectManagementApplication
   dotnet run
   ```

5. **Otvaranje u browseru**
   - HTTPS: `https://localhost:7281`
   - HTTP: `http://localhost:5290`

## 📊 Struktura baze podataka

Aplikacija koristi 8 relacionih tabela:

| Tabela | Opis |
|--------|------|
| **Projekti** | Osnovni podaci o projektima |
| **RadniPaketi** | Radni paketi u okviru projekata |
| **Zadaci** | Zadaci u radnim paketima |
| **Aktivnosti** | Aktivnosti u zadacima |
| **Zaposleni** | Podaci o zaposlenima |
| **Timovi** | Timovi zaposlenih |
| **ClanoviTimova** | Veza između timova i zaposlenih (M:N) |
| **DodeleZadataka** | Dodeljivanje aktivnosti zaposlenima |

### ER Dijagram relacija

```
Projekat (1) ─── (N) RadniPaket (1) ─── (N) Zadatak (1) ─── (N) Aktivnost
    │                                                              │
    │                                                              │
    └─── (N) Tim (1) ─── (N) ClanTima (N) ─── (1) Zaposleni ─── (N) DodelaZadatka
```

## 🗂️ Struktura projekta

```
ProjectManagementApplication/
├── ProjectManagementApplication/      # Glavna web aplikacija
│   ├── Pages/                         # Razor Pages
│   │   ├── Projekti/                  # CRUD za projekte
│   │   ├── RadniPaketi/               # CRUD za radne pakete
│   │   ├── Zadaci/                    # CRUD za zadatke
│   │   ├── Aktivnosti/                # CRUD za aktivnosti
│   │   ├── ZaposleniPages/            # CRUD za zaposlene
│   │   ├── Timovi/                    # CRUD za timove
│   │   ├── ClanoviTima/               # Upravljanje članovima
│   │   ├── Dodele/                    # Dodeljivanje zadataka
│   │   └── Shared/                    # Layout i delovi stranica
│   ├── wwwroot/                       # Statički fajlovi
│   └── Program.cs                     # Entry point aplikacije
│
├── DataBaseContext/                   # EF Core DbContext
│   ├── AplikacijaDbContext.cs         # Konfiguracija baze
│   ├── DesignTimeDbContextFactory.cs  # Factory za migracije
│   └── Migrations/                    # EF migracije
│
└── DatabaseEntityLib/                 # Model klase
    └── Models/                        # Entiteti baze podataka
```

## 🎨 UI/UX Features

- 🎯 Moderan minimalistički dizajn
- 📱 Potpuno responzivan layout
- ✨ Smooth hover animacije
- 🎨 Moderna paleta boja
- 🔍 Intuitivna navigacija
- 💡 Jasno grupisane funkcionalnosti

## 📝 Primeri korišćenja

### Kreiranje novog projekta

1. Kliknite na **"Projekti"** na početnoj strani
2. Izaberite **"Create New"**
3. Unesite naziv, opis, datume i planirane sate
4. Sačuvajte projekat

### Dodeljivanje aktivnosti zaposlenom

1. Idite na **"Dodele zadataka"**
2. Kliknite **"Create New"**
3. Izaberite zaposlenog i aktivnost
4. Unesite datum dodele i planirane sate
5. Pratite ostvarene sate i status završetka

## 🔧 Konfiguracija

### Connection String

Connection string se nalazi u `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=projectmanagement.db"
  }
}
```

### Migracije

Kreiranje nove migracije:
```bash
dotnet ef migrations add NazivMigracije --project DataBaseContext
```

Primena migracija:
```bash
dotnet ef database update --project DataBaseContext
```

## 🚀 Deployment

Aplikacija automatski kreira SQLite bazu pri prvom pokretanju.

Za production deployment:
```bash
dotnet publish -c Release -o ./publish
```

## 📄 Licenca

Ovaj projekat je kreiran u obrazovne svrhe.

## 👨‍💻 Autor

Vaše ime / GitHub username

## 🤝 Doprinosi

Sugestije i doprinosi su dobrodošli! Slobodno otvorite issue ili pull request.

## 📞 Kontakt

- GitHub: [@vase-korisnicko-ime](https://github.com/vase-korisnicko-ime)
- Email: vas.email@example.com

---

⭐ Ako vam se sviđa projekat, ostavite mu zvezdicu na GitHub-u!
