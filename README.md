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

![Bruteforcer2](https://i.imgur.com/CJZwQrf.png "Bruteforcer2")
