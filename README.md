# Zgadnij Liczbe 2

## Opis projektu

**Zgadnij Liczbę 2** to konsolowa gra napisana w języku **C#** z wykorzystaniem podejścia obiektowego. Zadaniem gracza jest odgadnięcie losowo wygenerowanej liczby w jak najmniejszej liczbie prób i możliwie najkrótszym czasie.

Projekt został wykonany zgodnie z założeniami programowania obiektowego – każda klasa znajduje się w osobnym pliku i odpowiada za konkretny element działania aplikacji.

---

## Funkcjonalności

### 🎮 Rozgrywka

* losowanie liczby do odgadnięcia,
* podpowiedzi informujące, czy podana liczba jest za mała lub za duża,
* pomiar czasu rozgrywki,
* zliczanie liczby prób,
* zapis najlepszych wyników do Hall of Fame.

### 📊 Poziomy trudności

| Poziom | Zakres  |
| ------ | ------- |
| Łatwy  | 1 - 50  |
| Średni | 1 - 100 |
| Trudny | 1 - 250 |

### 🏆 Hall of Fame

Gra zapisuje najlepsze wyniki do pliku:

hall_of_fame.txt

Ranking sortowany jest według:

1. najmniejszej liczby prób,
2. najkrótszego czasu wykonania.

Wyświetlanych jest maksymalnie 5 najlepszych wyników dla każdego poziomu trudności.

### 🎲 Tryb zakładu

Przed rozpoczęciem gry użytkownik może:

* włączyć tryb zakładu,
* określić maksymalną liczbę prób.

Po przekroczeniu limitu prób gra kończy się przegraną.

### 🚀 New Game Plus

Specjalny tryb rozgrywki, w którym liczba może być ponownie losowana podczas gry, co zwiększa poziom trudności.
Brak możliwości wyboru trybu zakładu.

### 🌍 Obsługa wielu języków

Gra umożliwia zmianę języka:

* Polski
* Angielski

### 🎨 Motywy kolorystyczne

Dostępne motywy:

* Cyberpunk
* Classic
* Hacker

---

## Struktura projektu

### Program.cs

Punkt wejścia aplikacji.

Odpowiada za:

* utworzenie obiektów konfiguracyjnych,
* utworzenie Hall of Fame,
* uruchomienie menu głównego.

### MenuController.cs

Obsługuje:

* menu główne,
* menu ustawień,
* nawigację za pomocą strzałek,
* uruchamianie nowej gry,
* wyświetlanie Hall of Fame.

### GameEngine.cs

Główna logika gry:

* losowanie liczby,
* obsługa prób gracza,
* sprawdzanie poprawności odpowiedzi,
* mierzenie czasu,
* zapisywanie wyników.

### HallOfFame.cs

Zarządza rankingiem wyników:

* odczyt wyników z pliku,
* zapis wyników do pliku,
* sortowanie wyników,
* czyszczenie rankingu.

### ScoreModel.cs

Model pojedynczego wyniku gracza.

Przechowuje:

* nazwę gracza,
* liczbę prób,
* czas gry,
* poziom trudności,
* informację o trybie NG+.

### GameConfiguration.cs

Przechowuje ustawienia gry:

* język,
* tryb zakładu.

### LanguageManager.cs

Odpowiada za tłumaczenia wszystkich komunikatów wyświetlanych w grze.

### ThemeManager.cs

Zarządza kolorystyką interfejsu użytkownika.

---

## Wymagania

* .NET 8 lub nowszy
* System Windows, Linux lub macOS
* Konsola obsługująca kolory ANSI

---

## Uruchomienie projektu

Sklonuj repozytorium:
https://github.com/Wojciechchyzy1/Zgadnij-Liczbe-2.git
### Visual Studio
 Naciśnij:

F5

lub

Przycisk "Zgadnij liczbe 2" wyświetlany na górze

aby uruchomić aplikację.

## Zastosowane elementy programowania obiektowego

### Klasy

Projekt został podzielony na klasy odpowiedzialne za konkretne zadania:

* Program
* MenuController
* GameEngine
* HallOfFame
* ScoreModel
* GameConfiguration
* LanguageManager
* ThemeManager

### Enkapsulacja

Dane przechowywane są wewnątrz klas i udostępniane poprzez właściwości oraz metody.

Przykład:

```csharp
public bool AskForBet { get; set; }
```

### Abstrakcja

Każda klasa realizuje jedno określone zadanie, ukrywając szczegóły implementacyjne przed pozostałymi elementami programu.

### Polimorfizm

Klasa `ScoreModel` implementuje interfejs:

```csharp
IComparable<ScoreModel>
```

co umożliwia własny sposób porównywania i sortowania wyników.

---

## Autor

Wojciech Chyży
