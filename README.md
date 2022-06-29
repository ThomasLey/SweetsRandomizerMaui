## Eine App f�r das Steuern von SweetsRandomizer Ger�ten

### Hauptmen�
Jedes hinzugef�gte Ger�t wird in der �bersicht mitsamt Name, Hostadresse und Verbindungsstatus angezeigt.
Der Verbindungsstatus wird anhand einer Farbe verdeutlicht.

![OverviewPage](Assets/OverviewPage.png)

> ## Information:
> Die Farben haben folgende Bedeutung:
> - Rot -> Nicht erreichbar (Timeout oder gar nicht existent)
> - Gelb -> Erreichbar, aber das Verhalten ist nicht dessen Kategorie entsprechend
> - Gr�n -> Erreichbar und einsatzbereit

## Benutzerhandbuch

### Hinzuf�gen eines Ger�tes
Zum hinzuf�gen eines neuen Ger�ts, sollten beide Endpunkte sich im selben Netzwerk befinden.
Anschlie�end l�sst sich durch das Dr�cken von "Ger�t hinzuf�gen" ein neues Fenster �ffnen, in dem das Ger�t n�her definiert werden muss.

![AddModulePage](Assets/AddModulePage.png)

> ## Achtung:
> Bei dieser Funktion ist die Angabe von folgenden Informationen pflichtig:
> - Ger�tename -> Der Name des angezeigten Ger�ts - Wird f�r das Anzeigen des Ger�ts im Hauptmen� ben�tigt
> - Host -> Die Adressbasis, die zum Ger�t zeigt - N�tig f�r das Ansprechen des jeweiligen Ger�tes
> - Typ -> Die Kategorie in die das Ger�t geh�rt - F�r das Anzeigen und Nutzen der dementsprechenden Funktionen n�tig
>     - ```Hierbei gibt es die Auswahl aus 3 verschiedenen Kategorien: Segmented lights, Spinning lights & Webpage```

##

### Steuern eines Ger�ts
Zum steuern eines Ger�ts m�ssen Sie nur auf das jeweilige Ger�t dr�cken.
Je nach Typ und Verbindungsstatus, �ndert sich auch die Ansicht.

#### Webpage Ger�t
Da die Kommunikation mit dem Webpage-Ger�t einseitig ist, ist dementsprechend das Steuern des Ger�tes nicht n�tig.
Diesbez�glich gibt es beim wechseln ins Steuerungsmen� keine Steuerungseinheiten.

![ModuleControlPage](Assets/ModuleControlPage_WebpageView.png)

#### Segmented-Light Ger�t
Die Steuerung eines Segmented-Light-Ger�ts l�sst sich durch das Senden von einzelnen Befehlen verwirklichen.
Nicht jeder Befehl ben�tigt alle Eingabefelder. Dementsprechend werden bei der Auswahl des Befehls, nur die n�tigen Eingabefelder editierbar gemacht.

![ModuleControlPage](Assets/ModuleControlPage_SegmentedLightsView.png)

> #### Information:
> Folgende Befehle stehen bei den Segmented lights zur verf�gung:
> - Segment einschalten -> L�sst ein Segment (mehrere LEDs) mit der ausgew�hlten Farbe aufleuchten.
> - Segment exklusiv einschalten -> L�sst ein Segment mit der ausgew�hlten Farbe aufleuchten. Alle anderen Segmente werden hierbei ausgeschaltet.
> - Auf Hintergrundfarbe setzen -> L�sst alle Segmente mit der gesetzten Hintergrundfarbe aufleuchten.
> - Hintergrundfarbe �ndern -> Setzt die Hintergrundfarbe die bei der Funktion "Auf Hintergrundfarbe setzten" benutzt wird. Diese wird beim Neustart des Ger�ts zur�ckgesetzt.
> - Alle Segmente einschalten -> L�sst alle Segmente mit der gegebenen Farbe aufleuchten.
> - Alle Segmente ausschalten -> Schaltet alle Segmente aus.

#### Spinning-Light Ger�t
Spinning-Light-Ger�te besitzen im Gegensatz zu den Segmented lights, keine einfache Kommandostruktur.
Hierbei werden Befehle mittels verschiedener Kn�pfe gesendet.

![ModuleControlPage](Assets/ModuleControlPage_SpinningLightsView.png)

> #### Information:
> Folgende Funktionen stehen bei den Spinning lights zur verf�gung:
> - Hebe ein vordefiniertes Segment mit der gegebenen Weite hervor.
> - Hebe eine Reihe an LEDs mit den gegebenen Grenzen hervor.
> - Schalte alle Segmente mit der gegebenen Farbe ein.
> - Schalte alle Segmente aus.
> - Stelle die Animationsgeschwindigkeit in Undrehungen pro Sekunde ein.
> - Stelle die Umdrehungsrichtung ein. (-1 oder 1)
> - Stelle die Hintergrundfarbe ein.
> - Stelle die Vordergrundfarbe ein.
> - Stelle ein, wie viele Pixel ein Segment besitzt.
> - Stelle ein, wie viele Segmente das Ger�t besitzt.
