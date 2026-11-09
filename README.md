# Cykelverkstaden – TDD med tjänster i C#

**KYH · Arbetsmetodik – Versionshantering, Kravhantering och TDD · Lektion 3**

Det här är ett undervisningsprojekt där vi utvecklar en liten del av ett system för en cykelverkstad med **testdriven utveckling (TDD)**.

Vi utgår från ett förväntat beteende, **skriver testet först** och låter sedan testerna styra vilken produktionskod som behöver skapas. Fokus ligger på arbetsmetoden, inte på att repetera hur klasser, interfaces, repositories eller dependency injection fungerar.

> **SUT (System Under Test)** betyder den komponent som testet fokuserar på. Här är vår SUT en vanlig C#-tjänst, exempelvis `WorkshopService`.

## Lärandemål

Efter övningen ska du kunna:

- Gå från ett **acceptanskriterium** till ett automatiserat test.
- Genomföra en **Red → Green → Refactor**-cykel.
- Förklara skillnaden mellan ett **kompileringsfel** och ett **körbart, misslyckat test**.
- Skriva testfall för både **normalfall** och **gränsfall**.
- Låta ett nytt behov motivera ett repository och **dependency injection**, i stället för att skapa alla beroenden i förväg.
- Köra hela testsviten efter varje ändring och beskriva vad testerna visar.

## Projektstruktur

Vi arbetar med ett C#-klassbibliotek och ett separat **xUnit-testprojekt**:

```text
CykelverkstadTdd/
├── Cykelverkstad/             # Produktionskod
├── Cykelverkstad.Tests/       # Automatiserade tester (xUnit)
└── README.md
```

Strukturen visar **målbilden**. Under demonstrationen kommer vissa klasser och interfaces avsiktligt att saknas från början – de skapas först när testerna kräver dem.

### Förutsättningar

- Installerat **.NET SDK** och valfri C#-utvecklingsmiljö (t.ex. Visual Studio, Rider eller VS Code).
- xUnit i testprojektet och en projektreferens från `Cykelverkstad.Tests` till `Cykelverkstad`.

Kör testerna från lösningens rot:

```bash
dotnet restore
dotnet test
```

Om du behöver skapa projektstrukturen från början:

```bash
dotnet new sln -n CykelverkstadTdd
dotnet new classlib -n Cykelverkstad
dotnet new xunit -n Cykelverkstad.Tests
dotnet sln add Cykelverkstad/Cykelverkstad.csproj
dotnet sln add Cykelverkstad.Tests/Cykelverkstad.Tests.csproj
dotnet add Cykelverkstad.Tests/Cykelverkstad.Tests.csproj reference Cykelverkstad/Cykelverkstad.csproj
dotnet test
```

> Olika .NET SDK-versioner kan skapa lösningsfilen som `.sln` eller `.slnx`. Båda fungerar för övningen.

## Del 1 – Gemensam genomgång: `WorkshopService`

**Scenario:** Cykelverkstaden behöver kunna registrera arbetsordrar, till exempel *”Byta bromsar”*.

### Acceptanskriterier

| ID | Situation | Förväntat resultat |
|---|---|---|
| A1 | En arbetsorder registreras med beskrivningen `"Byta bromsar"`. | En arbetsorder returneras med samma beskrivning. |
| A2 | Beskrivningen är tom eller består endast av blanksteg. | Registreringen avvisas med `ArgumentException`. |
| A3 | En giltig arbetsorder registreras. | Arbetsordern sparas och kan hittas i repositoryt. |

### Vi bygger tjänsten stegvis

1. **Skriv testet för A1 innan `WorkshopService` finns.** Fundera på vilket publikt anrop testet behöver göra.
2. **Gör testet körbart.** Skapa bara de typer och metodsignaturer som behövs. Låt metoden tillfälligt kasta `NotImplementedException`.
3. **Red → Green.** Kör testet, se att det misslyckas och skriv minsta möjliga implementation som gör det grönt.
4. **Nytt test för A2.** Skriv först ett misslyckat test för ogiltig beskrivning. Lägg därefter till valideringen.
5. **Nytt test för A3.** Först nu behöver vi ett repository. Låt sparkravet driva fram exempelvis `IWorkOrderRepository` och konstruktorinjektion.
6. **Refactor.** Förbättra koden utan att ändra dess beteende. Kör hela testsviten igen.

Vi använder ett **enkelt in-memory-repository som testdubbel**. Vi ska kontrollera tjänstens beteende, inte kommunikation med en riktig databas.

## Del 2 – Parövning: `PartsOrderService`

**Scenario:** Cykelverkstaden vill kunna beställa reservdelar. En beställning innehåller ett **reservdelsnamn** och ett **antal**. Beställningar med ogiltiga värden får inte registreras.

### Acceptanskriterier

| ID | Situation | Förväntat resultat |
|---|---|---|
| P1 | Beställ `"Kedja"` med antal `2`. | En beställning returneras med namnet `Kedja` och antalet `2`. |
| P2 | Beställ `"Däck"` med antal `1`. | Beställningen godkänns (minsta giltiga antal). |
| P3 | Reservdelsnamnet är tomt eller består endast av blanksteg. | Beställningen avvisas med `ArgumentException`. |
| P4 | Antalet är `0` eller negativt. | Beställningen avvisas med `ArgumentOutOfRangeException`. |
| P5 | En giltig beställning registreras. | Beställningen sparas i repositoryt. |

### Arbetsinstruktion

Arbeta **två och två**. Skapa testprojekt och produktionskod vid behov, men börja med **ett test mot en `PartsOrderService` som inte finns ännu**.

1. Välj **ett** acceptanskriterium, gärna P1.
2. Skriv testet och skapa därefter minsta skelett som behövs för att kunna köra det.
3. Få ett **körbart rött** test. Implementera sedan minsta beteende som får det grönt.
4. Fortsätt med ett nytt kriterium i taget. Lägg inte till ett repository förrän sparkravet behöver det.
5. Byt roller under arbetet: en är **driver** (skriver kod) och den andra är **navigator** (håller koll på krav, tester och nästa steg).
6. Refaktorera vid behov och **kör samtliga tester** efteråt.

Förslagsvis får tjänsten en metod som heter `PlaceOrder`, men ni väljer själva lämpliga typer och utformning. **Ingen färdig implementation är avsiktligt given här.**

### Kontrollfrågor för paret

- Vilket acceptanskriterium motsvarar ert senaste test?
- Varför var testet rött innan ni skrev produktionskoden?
- Vilken minsta ändring gjorde testet grönt?
- Vilket gränsfall testar ni, och varför?
- När blev ett repository nödvändigt?
- Vad refaktorerade ni, och hur vet ni att beteendet är oförändrat?

## Kom ihåg: Red → Green → Refactor

| Steg | Frågan ni ska besvara |
|---|---|
| 🔴 **Red** | Vilket konkret beteende saknas? Kan vi visa det med ett misslyckat test? |
| 🟢 **Green** | Vad är minsta ändringen som gör testet och övriga tester gröna? |
| 🔵 **Refactor** | Kan vi göra koden tydligare utan att ändra beteendet? |

**Viktigt:** När testet hänvisar till en klass som ännu inte finns uppstår först ett *kompileringsfel*. Skapa minsta möjliga skelett och kör sedan ett test som verkligen misslyckas. TDD handlar inte om att stanna vid kompileringsfelet.

## Koppling till examinationsuppgiften Biblioteket

Dagens cykelverkstad är en **fristående undervisningsövning**. Den ersätter inte det individuella TDD-underlag som ska kunna visas från **biblioteksprojektets sprint**, som startar **11 november 2026**.

För betygskriteriet **G6** behöver du bland annat kunna förklara och visa:

- En egen **Red–Green–Refactor-cykel**, med spår av arbetsordningen.
- Hur ett test hör ihop med ett **acceptanskriterium**.
- Ett **normalfall** och ett relevant **gränsfall**.
- Hur automatiserade tester körs och hålls aktuella när koden ändras.

Spara därför relevanta commits, testresultat och korta egna anteckningar under sprinten.
