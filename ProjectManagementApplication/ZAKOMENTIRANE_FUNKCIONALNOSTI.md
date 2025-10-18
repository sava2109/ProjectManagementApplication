# Zakomentirane dodatne funkcionalnosti

## Pregled

Sledeće dodatne funkcionalnosti su zakomentirane jer prevazilaze osnovne zahteve zadatka:

## 1. **Aktivnosti sa više zaposlenih** (AktivnostiViseZaposlenih)
- **Fajlovi:** 
  - `Pages/AktivnostiViseZaposlenih.cshtml`
  - `Pages/AktivnostiViseZaposlenih.cshtml.cs`
- **Opis:** Prikazuje aktivnosti na kojima radi više zaposlenih istovremeno
- **Status:** Zakomentarisano u navigaciji

## 2. **Nezavršeni zadaci** (ZadaciNezavrseni)
- **Fajlovi:**
  - `Pages/ZadaciNezavrseni.cshtml`
  - `Pages/ZadaciNezavrseni.cshtml.cs`
- **Opis:** Prikazuje zadatke sa manje od 50% završenih aktivnosti
- **Status:** Zakomentarisano u navigaciji

## 3. **Napredni izveštaji** (Izvestaji folder)
- **Fajlovi:**
  - `Pages/Izvestaji/Zaposleni.cshtml` - Izveštaj po zaposlenom
  - `Pages/Izvestaji/Projekat.cshtml` - Izveštaj po projektu
  - `Pages/Izvestaji/Tim.cshtml` - Izveštaj po timu
  - `Pages/Izvestaji/Statistika.cshtml` - Statistika projekata
- **Opis:** Napredni izveštaji i analitika podataka
- **Status:** Zakomentirano u navigaciji i na početnoj strani

## Kako ponovo aktivirati dodatne funkcionalnosti

1. Otvori `Pages/Shared/_Layout.cshtml`
2. Pronađi liniju koja počinje sa `@* DODATNE FUNKCIONALNOSTI - zakomentirano *@`
3. Ukloni `@*` i `*@` komentare sa linkova koje želiš da aktiviraš

4. Otvori `Pages/Index.cshtml`
5. Pronađi liniju koja počinje sa `@* DODATNE FUNKCIONALNOSTI - zakomentirano *@`
6. Ukloni `@*` i `*@` komentare sa sekcije "Treća kolona: Izveštaji"

## Osnovne funkcionalnosti koje su i dalje aktivne

✅ CRUD za Projekte
✅ CRUD za Radne pakete
✅ CRUD za Zadatke
✅ CRUD za Aktivnosti
✅ CRUD za Zaposlene
✅ CRUD za Timove
✅ CRUD za Članove tima
✅ CRUD za Dodele zadataka
✅ Relacije između svih tabela
✅ Pretraga i sortiranje

**Napomena:** Sve dodatne stranice i dalje postoje u projektu i funkcionalne su, samo nisu dostupne kroz navigaciju.
