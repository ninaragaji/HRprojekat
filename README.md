# HRprojekat

Ovo je .NET 8 projekt pod nazivom **HRprojekat**.

## Pokretanje projekta

1. Kloniraj repozitorijum:

   ```bash
   git clone https://github.com/ninaragaji/HRprojekat.git
   ```

2. Uđi u direktorijum projekta:

   ```bash
   cd HRprojekat
   ```

3. Kreiraj datoteku **.env** u korenu projekta.

   > ⚠️ Ovaj fajl mora postojati da bi projekat pravilno radio. U njemu se nalaze konfiguracije i tajne promenljive okruženja (npr. konekcioni stringovi, API ključevi, i slično).


## Struktura projekta

* **Controllers/** – sadrži API kontrolere.
* **Models/** – modeli podataka.
* **Services/** – poslovna logika.
* **Data/** – konfiguracija baze podataka.
* **Profiles/** – AutoMapper profili.
* **DTO/** – Data Transfer Objects.

## Napomena

Pre pokretanja, obavezno kreiraj i pravilno popuni fajl `.env`!
