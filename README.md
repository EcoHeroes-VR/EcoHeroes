<h1 align="center">
    <img src="images/eco-heroes-showcase-1.png" alt="Logo" height="350">
    </br></br>
    <b>Eco Heroes VR</b>
    </br>
    Augsburgs Zukunft in deinen Händen!
</h1>

# Inhalt

## Eco Heroes VR
Auf spielerische Art und Weise sollen Schülerinnen und Schülern die in den Zukunftsleitlinien der Stadt Augsburg verankerten Nachhaltigkeitsziele vermittelt werden.</br>
Im Zuge dieses Projekts entstehen in Zusammenarbeit mit dem Nachhaltigkeitsbüro Augsburg und dem Schülerlabor der Universität Augsburg vier Virtual Reality Minispiele zu jeweils einer der vier Nachhaltigkeitsdimensionen Ökologie, Wirtschaft, Soziales und Kultur.

## Ziel des Spiels
Der Spieler ist dafür verantwortlich Augsburg nachhaltiger zu gestalten. In der Rolle des Stadtoberhauptes legt er dabei selbst Hand an und lernt wichtige Prinzipien nachhaltigen Handelns. Am Ende jedes Minispiels werden Nachhaltigkeitspunkte vergeben, sodass der Spieler ein direktes Feedback über seine getroffenen Entscheidungen erhält und sich mit anderen Spielern vergleichen kann.

## Referenzen

* [Projekt Website](https://showcase.informatik.hs-augsburg.de/sose-2023/eco-heroes)
* [Projekt Trailer](https://youtu.be/19jOlFxftfI)

## Open-Source Lizenz
- [LIZENZ DATEI](LICENSE)
- Wir freuen uns über die Nennung der Originalautoren beim Kopieren, Verwenden oder Umschreiben des Quellcodes.

## Feedback
- Kommentare, Fragen und allgemeines Feedback können gerne an folgende E-Mail-Adresse gesendet werden: anja.metzner@hs-augsburg.de

## Mitwirkende

- Bei dem Projekt handelte es sich initial um ein studiengangsübergreifendes Semesterprojekt an der Technischen Hochschule Augsburg.

### **Mitwirkende im Sommersemester 2023:**

- **Studenten/Studentinnen:** Johanna Dannenberg, Marc Fischer, Nikita Guryanov, Manuel Hagen, Lukas Konietzka, Theresa Mayer, Martin Sattler, Dominik Wagner

- **Beteiligte:** Büro f. Nachhaltigkeit der Stadt Augsburg, Schülerlabor der Universität Augsburg<br>

- **Supervision:** Prof. Dr. Anja Metzner (Technische Hochschule Augsburg)<br>

Vielen Dank an alle Beteiligten! Darunter das Projekt ii.oo, das Büro für Nachhaltigkeit der Stadt Augsburg sowie das DLR_School_Lab Uni Augsburg

<br>

# Technisches

## Anforderungen
* Unity 2021.3.21f1
* VR Headset (z.B. Occulus Rift/HTC Vive Pro)
* Die empfohlene Systemanforderungen der Workstation sind mit derer des genutzten VR-Headsets abzugleichen
* Für Weiterentwicklung: Unity C# Code Editor (Bsp.: Jetbrains Rider oder Microsoft Visual Studio)

## Installation

[Schritt-für-Schritt Anleitung](Eco_Heroes_Installationsanleitung.pdf)

Um das Spiel zu spielen:

```
1. Projektdaten herunterladen
2. Projektdaten in Unity öffenen (Editor Version s.o.)
3. Spielversion bauen
4. gebaute Version ausführen
5. falls noch nicht getan: entsprechende Software des VR-Headsets installieren
```

Zur Weiterentwicklung:
```
1. Projektdaten herunterladen
2. Projektdaten in Unity öffenen (Editor Version s.o.)
3. Weiterentwicklung:
    - Bearbeiten der Scenes & Assets in Unity
    - Bearbeiten der C# Scripts in einem ausgwählten Code Editor
4. falls noch nicht getan: entsprechende Software des Entwicklungs-VR-Headsets installieren
```

## Überblick der wichtigsten Dateien für Entwickler

Dateien befinden sich in: `Assets > _Game`

### Scenes
Hier befinden sich die Szenen der einzelnen Level (`Spiel1`, `Hubworld`, `Kitchen`)

### Scripts
Hier sind alle C#-Scripts sortiert nach Szene oder Anwendung

### Resources
Hier befinden sich die Ressourcen, wie Models, Materials, Sounds etc. Innerhalb der einzelnen Subordner wird nach Anwendung sortiert (beispielsweise: `Models > Spiel2`)

### RenderSettings
Hier werden die Licht- und RenderPipeline-Einstellungen gespeichert, die für das gesamte Spiel verwendet werden.

### Prefabs
Hier sind die Prefab-Objekte, die für das Spiel erstellt wurden, sortiert nach Anwendung.

## Softwarestruktur
<h1 align="center">
    <img src="images/Komponentendiagramm.png" alt="SW-Komponenten" height="350"> </br>
</h1>
