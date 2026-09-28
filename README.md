# Bruteforcer2
Performant Multithreaded Hash (MD5) Bruteforcer for educational purposes.

## Starten in VS Code

Voraussetzungen: Windows (WinForms), das .NET 8 SDK oder ein neueres SDK,
die .NET 8 Desktop Runtime und die VS-Code-Erweiterung **C#** von Microsoft
(`ms-dotnettools.csharp`). Das .NET 8 SDK enthält die benötigte Runtime bereits.
Bei einem neueren SDK muss die .NET 8 Desktop Runtime zusätzlich vorhanden sein.

1. Den Repository-Ordner `Bruteforcer2` in VS Code öffnen (mit der `.sln`-Datei).
2. Die empfohlene C#-Erweiterung installieren, falls sie noch fehlt.
3. **F5** startet `Bruteforcer2 (Debug)` mit Debugger und baut vorher automatisch.
   **Strg+F5** startet ohne Debugger; **Strg+Umschalt+B** baut das Projekt.

Alternativ im Terminal:

```powershell
dotnet build Bruteforcer2.sln
dotnet run --project Bruteforcer2/Bruteforcer2.csproj
```

NuGet-Pakete werden beim Build automatisch wiederhergestellt; beim ersten Build
ist dafür Internetzugang nötig. Für Geschwindigkeitsmessungen den Release-Build
ohne Debugger verwenden:

```powershell
dotnet run --project Bruteforcer2/Bruteforcer2.csproj --configuration Release
```

Die bestehenden Implementierungen verwenden noch `HashLib 2.0.1` (Warnung
`NU1701` wegen des alten .NET-Framework-Ziels) und `MD5CryptoServiceProvider`
(`SYSLIB0021`). Diese Warnungen verhindern den Build nicht.
VS Code bietet keinen visuellen WinForms-Designer; die Oberfläche lässt sich
weiterhin über den vorhandenen C#-Designer-Code bearbeiten.

## AstraMD5

Auf CPUs mit AVX-512 steht zusätzlich **AstraMD5 (AVX-512 x16)** zur Verfügung.
Dieser separate Adapter verarbeitet 16 kurze Kandidaten gleichzeitig und nutzt
native Vektorrotationen sowie ternäre Logik-Instruktionen für die MD5-Runden.
Der AVX2-Eintrag bleibt für direkte Vergleiche erhalten. Beide Varianten verwenden
denselben Batch-Suchpfad und zählen tatsächlich berechnete Kandidaten.

Im Hash-Auswahlfeld steht zusätzlich **AstraMD5 (AVX2 x8)** zur Verfügung.
Für Kandidaten bis 55 Bytes berechnet dieser Adapter acht MD5-Hashes gleichzeitig
mit AVX2 und vergleicht die Ergebniswörter direkt mit dem Zielhash. Die Anzeige
zählt Kandidaten, nicht SIMD-Batches; bei einem Treffer wird der gesamte bereits
berechnete Batch mitgezählt. Die bisherigen Adapter bleiben unverändert auswählbar.

Ohne AVX2 erscheint **AstraMD5 (Scalar)**. Einzelaufrufe und unvollständige Batches
verwenden den eigenen skalaren Kern. Eingaben ab 56 Bytes werden über .NET-MD5
berechnet. Die Auswahl allein garantiert noch keinen Geschwindigkeitsgewinn;
Vergleiche mit identischen Einstellungen im Release-Modus ohne Debugger durchführen.

Die Implementierung ist aufgeteilt in:

- `Hash/MD5/HashAstraMD5.cs`: Adapter, Blockvorbereitung und SIMD-Ergebnisvergleich.
- `Hash/MD5/HashAstraMD5.Rounds.cs`: vollständig ausgeschriebene MD5-Runden.
  Reproduzierbar mit `python tools/generate_astra_md5.py`; Python wird zum normalen
  Bauen oder Starten nicht benötigt.
- `Bruteforce/BruteforceTask.cs`, Methode `BruteforceBatches`: Batch-Zuführung
  ausschließlich für passende Adapter; der bisherige skalare Suchpfad bleibt erhalten.

Korrektheitsprüfung (bekannte MD5-Vektoren, Vergleich mit .NET und BouncyCastle,
SIMD-Positionen, Restbatches und Suchraumgrenzen; kein Benchmark):

```powershell
dotnet run --project tools/AstraMD5.Verify --configuration Release
dotnet run --project tools/AstraMD5.Verify --configuration Release -- --avx512
```

![Bruteforcer2](https://i.imgur.com/CJZwQrf.png "Bruteforcer2")
