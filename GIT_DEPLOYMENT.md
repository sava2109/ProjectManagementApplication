# Git Deployment Guide

## 📋 Koraci za postavljanje na GitHub

### 1. Inicijalizacija Git repozitorijuma

Otvorite PowerShell terminal u root folderu projekta i izvršite:

```powershell
# Pozicionirajte se u glavni folder projekta
cd "c:\Users\Dimitrijevic\Downloads\ProjectManagementApplication\ProjectManagementApplication\ProjectManagementApplication"

# Inicijalizujte git repozitorijum
git init

# Dodajte sve fajlove
git add .

# Napravite prvi commit
git commit -m "Initial commit: Project Management Application"
```

### 2. Kreiranje GitHub repozitorijuma

1. Idite na [GitHub](https://github.com)
2. Kliknite na **"+"** u gornjem desnom uglu
3. Izaberite **"New repository"**
4. Unesite naziv: `ProjectManagementApplication`
5. Dodajte opis: `Modern web application for project management built with .NET Core and Razor Pages`
6. **NEMOJTE** inicijalizovati sa README, .gitignore ili licencom (već imate u projektu)
7. Kliknite **"Create repository"**

### 3. Povezivanje lokalnog repo sa GitHub-om

```powershell
# Dodajte remote origin (zamenite USERNAME sa vašim GitHub korisničkim imenom)
git remote add origin https://github.com/USERNAME/ProjectManagementApplication.git

# Postavite main kao default branch
git branch -M main

# Push-ujte kod na GitHub
git push -u origin main
```

### 4. Alternativa: Korišćenje SSH umesto HTTPS

Ako koristite SSH ključeve:

```powershell
git remote add origin git@github.com:USERNAME/ProjectManagementApplication.git
git push -u origin main
```

## 🔄 Svakodnevni Git workflow

### Dodavanje novih izmena

```powershell
# Proverite status
git status

# Dodajte izmenjene fajlove
git add .

# Commit sa porukom
git commit -m "Opis izmena"

# Push na GitHub
git push
```

### Kreiranje nove feature grane

```powershell
# Kreirajte novu granu
git checkout -b feature/nova-funkcionalnost

# Radite izmene...

# Commit izmena
git add .
git commit -m "Dodao novu funkcionalnost"

# Push grane na GitHub
git push -u origin feature/nova-funkcionalnost
```

### Merge grane u main

```powershell
# Prebacite se na main granu
git checkout main

# Merge-ujte feature granu
git merge feature/nova-funkcionalnost

# Push na GitHub
git push
```

## 📝 Korisne Git komande

```powershell
# Prikaz istorije commitova
git log --oneline

# Prikaz razlika
git diff

# Odbaci promene u fajlu
git checkout -- naziv-fajla.cs

# Pull najnovije izmene sa GitHub-a
git pull

# Kloniranje repozitorijuma
git clone https://github.com/USERNAME/ProjectManagementApplication.git

# Prikaz svih grana
git branch -a

# Brisanje grane
git branch -d feature/stara-grana
```

## 🎯 Best Practices

### Commit poruke

Dobro:
```
✅ "Add SQLite database configuration"
✅ "Fix: Resolve null reference in DodeleZadataka"
✅ "Update: Modernize UI with new color scheme"
✅ "Refactor: Improve query performance in Projekti"
```

Loše:
```
❌ "update"
❌ "fix bug"
❌ "changes"
❌ "asdf"
```

### Pre svakog push-a

```powershell
# Proverite šta će biti poslato
git status

# Testirajte aplikaciju
dotnet build
dotnet test

# Commit i push
git add .
git commit -m "Opisna poruka"
git push
```

## 🔒 Sigurnost

### Fajlovi koji NIKADA ne smeju biti na GitHub-u:

- ❌ `appsettings.Development.json` (connection strings, secrets)
- ❌ `*.db` fajlovi (SQLite baze sa podacima)
- ❌ `/bin/` i `/obj/` folderi
- ❌ `.vs/` folder
- ❌ Lični API ključevi

**Ovi fajlovi su već isključeni u `.gitignore` fajlu!**

## 📦 Kloniranje projekta (za druge korisnike)

```powershell
# Kloniranje
git clone https://github.com/USERNAME/ProjectManagementApplication.git

# Ulazak u folder
cd ProjectManagementApplication

# Restore paketa
dotnet restore

# Build
dotnet build

# Pokretanje
cd ProjectManagementApplication
dotnet run
```

## 🎨 GitHub README Badges

Vaš README već sadrži badges. Možete dodati još:

```markdown
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4)
```

## 📞 Pomoć

Ako imate problema sa Git-om:

```powershell
# Provera verzije Git-a
git --version

# Provera konfiguracije
git config --list

# Postavljanje korisničkog imena i email-a
git config --global user.name "Vaše Ime"
git config --global user.email "vas.email@example.com"
```

## 🚀 Gotovo!

Vaš projekat je sada na GitHub-u i dostupan svima! 

Link ka vašem repozitorijumu će biti:
`https://github.com/USERNAME/ProjectManagementApplication`

---

**Napomena:** Zamenite `USERNAME` sa vašim stvarnim GitHub korisničkim imenom.
