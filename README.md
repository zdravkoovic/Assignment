# Assignment
 
## Sadržaj
 
1. [Pregled aplikacije](#pregled-aplikacije)
2. [Tehnička realizacija](#tehnička-realizacija)
3. [Arhitektura](#arhitektura)
4. [Testovi](#testovi)
---
 
## Pregled aplikacije
 
Aplikacija je vizuelno unapređena u odnosu na skeleton — usklađene su boje nav-bar dugmića i akcionih dugmića unutar Content kontejnera. Tema je svetla sa tamnim akcentima.


| Početni ekran | Loaders | ToDo lista |
|---|---|---|
| ![Početni ekran](docs/images/initial.png) | ![Loaders](docs/images/loaders.png) | ![ToDo lista](docs/images/todo.png) |
 
**Loaders** prikazuje napredak tri paralelna procesa u realnom vremenu sa mogućnošću pojedinačnog zaustavljanja.
 
**ToDo** omogućava dodavanje stavki koje se automatski sortiraju po prioritetu i vizuelno razlikuju bojom.
 
---
 
## Tehnička realizacija
 
### Model
 
Skeleton projekat nije sadržao Model sloj. Dodat je folder `Model` sa sledećim klasama:
 
**`ThreadWorker`** — predstavlja stanje jednog background thread-a koje mora biti prikazano na UI:
 
- `Duration` — nasumično vreme izvršavanja (10–50 sekundi), postavljeno pri inicijalizaciji
- `Elapsed` — proteklo vreme, ažurira se svake sekunde
- `Progress` — computed property, izračunava se iz `Elapsed / Duration`
- `CancellationTokenSource` — čuva token za zaustavljanje thread-a
- `IsActive` — signalizira UI-u da li je thread aktivan; `Cancel()` metoda postavlja na `false` i poziva `Cts.Cancel()`

**`ToDoItem`** — predstavlja jednu stavku ToDo liste sa nazivom i prioritetom.
 
---
 
### Loaders — Multithreading
 
Tri background thread-a pokreću se pri inicijalizaciji `LoadersViewModel`-a. Svaki thread samostalno upravlja sopstvenim napretkom:
 
```
Thread → WaitOne(1000) → Elapsed++ → OnPropertyChanged → WaitOne(1000) → ...
```
 
`CancellationToken.WaitHandle.WaitOne(1000)` koristi se umesto `Thread.Sleep` jer se odmah prekida pri Cancel-u bez čekanja da istekne sekunda.
 
Thread-ovi žive nezavisno od aktivnog View-a — instanca `LoadersViewModel`-a se čuva između navigacija i kreira se iznova samo kada je `TotalProgress == 100` ili `TotalProgress == 0`.
 
Cancel dugme postaje neaktivno automatski kroz `CommandManager.InvalidateRequerySuggested()` kada thread završi ili bude cancelovan.
 
---
 
### ToDo lista — Strategy pattern
 
Dodat `ISortStrategy` interfejs za apstrakciju algoritma sortiranja radi lakše zamene u budućnosti:
 
```csharp
// Zamena algoritma — jedna linija
new ToDoSubmitViewModel(new BinaryInsertStrategy());
```
 
`LinearSortStrategy` implementira O(n) insert koji ubacuje novi element direktno na odgovarajuće mesto u već sortiranoj kolekciji — bez ponovnog sortiranja čitave liste.
 
`PriorityToColorConverter` vizuelno razlikuje prioritete:
 
| Prioritet | Značenje | Boja pozadine |
|-----------|----------|---------------|
| 1 | Najviši | Crvena |
| 2 | Srednji | Narandžasta |
| 3 | Najniži | Zelena |
 
---
 
### Validacija
 
- **Confirm dugme** — aktivno samo kada su ispunjeni uslovi forme: `ItemName != null` i `SelectedPriority != 0`
- **Cancel dugme** — aktivno samo za thread-ove koji su živi i čiji progress nije dostigao 100%
---
 
## Arhitektura
 
Projekat prati MVVM obrazac. Thread-ovi su pokrenuti u `LoadersViewModel` jer predstavljaju logiku izvršavanja, time je postignuta zavisnost u jednom smeru - `LoadersViewModel` zavisi od `ThreadWorker`
```
Assignment/
├── Models/
│   ├── ThreadWorker.cs
│   └── ToDoItem.cs
├── ViewModels/
│   ├── LoadersViewModel.cs
│   ├── ToDoSubmitViewModel.cs
│   └── ToDoListViewModel.cs
├── Views/
│   ├── PriorityToColorConverter.cs
│   └── ...
├── Commands/
│   └── RelayCommand.cs
└── Strategies/
    ├── ISortStrategy.cs
    └── LinearSortStrategy.cs
```
 
---
 
## Testovi
 
Projekat `Assignment.Tests` koristi MSTest framework. `MSTest.TestAdapter` ugrađen sa verzije `2.2.10` na `4.2.3` radi kompatibilnosti.
 
Testovi pokrivaju logiku `ThreadWorker`-a direktno kroz `Duration` i `Elapsed` propertije bez pokretanja pravih thread-ova — moguće zahvaljujući razdvajanju stanja od logike izvršavanja.
 
---
