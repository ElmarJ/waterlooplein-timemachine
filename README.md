# Waterlooplein 3D

![Screenshot Jodenbreestraat](https://github.com/ElmarJ/Waterlooplein3D/blob/master/Jodenbreestraat.png "Screenshot Jodenbreestraat")

## Wat is het?

Waterlooplein 3D is een reconstructie van het stratenpatroon van de Waterloopleinbuurt van rond 1870-1890. Het programma is bedoeld voor die mensen die (net als ik) altijd wat moeite hebben om zich voor te stellen hoe de buurt er nou ongeveer uit heeft gezien. Verwacht geen perfecte reconstructie, het belangrijkste doel is om een beetje een beeld te krijgen van waar nou de straten liepen en hoe de buurt in elkaar stak. Zo kun je over het Valkenburgereiland rondlopen, en dan via het Markenplein de Rapenburgerstraat uitlopen tot het Rapenburgerplein - allemaal plekken die nu niet meer bestaan of anders liggen. Wat je ziet is een gemodelleerde stad waarin het stratenpatroon en de locatie van de gebouwen echt is, maar de gevels en gebouwhoogten willekeurig gegenereerd. Verwacht dus niet je eigen huis in detail terug te vinden: waarschijnlijk staat er een gebouw met de juiste omvang maar de verkeerde gevel.

## Wie?

Dit is een privé-projectje van [Elmar Jansen](https://twitter.com/elmarj) (elmar@elmarjansen.nl). Omdat ik dacht dat er mogelijk meer geïnteresseerden zijn, heb ik het nu online gezet en de code beschikbaar gemaakt. Als je het leuk vindt om hier aan verder te gaan of mee te helpen om het allemaal wat mooier te maken: wees welkom (zie hieronder bij "meehelpen").

## Hoe werkt het?

### Installeren

#### Windows (64 bits)

- [Download het zip-bestand hier](https://github.com/ElmarJ/Waterlooplein3D/releases/) en pak het uit (bijvoorbeeld door er rechts op te klikken en te kiezen voor Alles uitpakken / Extract all).
- Open de uitgepakte map en start het programma (Waterlooplein 3D.exe).

#### Mac

- Mac-versie is ook bij [releases](https://github.com/ElmarJ/Waterlooplein3D/releases/) te downloaden.
- Ik heb geen idee of het werkt, want ik heb zelf geen mac, maar wie weet!

#### Web (niet ideaal)

Een WebGL-versie die het direct doet in de browser is beschikbaar op https://waterlooplein3d.web.app/. Houd er rekening mee dat deze versie minder goed werkt dan de gedownloade versie, en nog steeds inhoudt dat je browser achter de schermen een bestand van 250mb downloadt.

### Besturen

- Rondlopen met A, S, D en W -toetsen.
- Rondkijken met muis
- Springen (heel hoog!) met spatie.
- Afsluiten met ESC.

## Waarom?

De Waterloopleinbuurt is de plek waar ik ben opgegroeid en waar ik nog altijd woon. Ik houd van deze plek, maar het is toch ook vooral een rare buurt. Op foto's is te zien wat een levendige en bijzondere buurt het tot de jaren '40 moet zijn geweest. Maar de moord op bijna al haar inwoners in de jaren 1940 gevolgd door de sloop van bijna alle woonblokken en de aanleg van verkeersaders en Stopera, hebben de buurt vervormd tot een raar lidteken in de stad. Zo lang ik me kan herinneren heb ik me afgevraagd hoe deze buurt er uit heeft gezien. Waar lag dat Markenplein? Hoe was het om door de Jodenhouttuinen te lopen? Waar liepen de straten op de plek waar nu de Stopera staat? Hoe zijn het Jonas Daniël Meijerplein en het Waterlooplein als er een gracht overheen loopt? Om m'n voorstellingsvermogen een handje te helpen heb ik een tijd terug een eenvoudig 3d-model van de buurt gemaakt, zodat ik zelf door de buurt kon lopen. Voor wie ook nieuwsgierig is, is hier het project te downloaden.
  
## Toekomst

Ik heb nog altijd een vaag plan om, als ik weer eens wat meer tijd heb, verder te werken aan dit project. In grote lijnen zou ik aan vier zaken willen werken:

1.  Het project meer geschikt maken voor gezamenlijk _open-source-project_-werk, zodat geïnteresseerden makkelijk bij kunnen dragen. Dit betekent vooral: het opschonen en documenteren van de C#-code, en het verder opsplitsen in meer losse bestanden van de GeoJSON-kaarten.
2.  _Realisme_, zodat de ervaring van rondlopen dichter komt te liggen bij wat rondlopen in de Waterloopleinbuurt moet zijn geweest. Dat betekent bijvoorbeeld toevoegen van details, betere reconstructie van gevels, betere "PBR materialen", meer "props" en eventueel voorbijgangers en verkeer en meer gedetailleerde modellen van de belangrijkste gebouwen.
3.  _Uitbreiden in ruimte_: met relatief weinig moeite (aanpassen de onderliggende kaarten) zou het nagebouwde gebied kunnen worden uitgebreid. Hulp is hierbij meer dan welkom!
4. _Uitbreiden in tijd_: graag zou ik ook een model maken van de periode voor 1862 (?), toen de grachten over het waterlooplein gedempt werden. Uiteindelijk zou het helemaal mooi zijn om completen reconstructies van Amsterdam op verschillende momenten te kunnen maken.

Ik hoor het graag als er animo is om bij te dragen, dan zet ik meer vaart achter het open-sourcen.

## Meehelpen

Bijdragen zijn meer dan welkom! Bugs rapporteren en nieuwe functies suggereren, doe je op [de "issues" pagina](https://github.com/ElmarJ/Waterlooplein3D/issues) van dit project, maar je kunt ook op andere manieren bijdragen:

### Uitbreiden en detailleren van de onderliggende kaarten 

De structuur van de buurt komt uit gedigitaliseerde kaarten. Het werk aan deze (onder het 3D-model liggende) kaarten heb ik ondergebracht in een [apart open source project](https://github.com/ElmarJ/Amsterdam.1892.GeoJSON). Het bewerken kan redelijk eenvoudig in het open source GIS-programma QGIS.

### Programmeren (C# / Unity)

De achterliggende code bestaat uit twee losse projecten, beide beschikbaar als open source:
- [Een project voor een Unity-plugin](https://github.com/ElmarJ/GeoJsonCityBuilder), waarmee automatisch objecten kunnen worden geplaatst en gecreëerd op basis van GeoJSON-data
- Dit Github-project voor de specifieke 3D reconstructie van de Waterloopleinbuurt / Amsterdam.

Code is nu nog wat ongestructureerd en weinig gedocumenteerd (typisch eenpersoonsproject). Mochten er mensen zin hebben om mee te doen dan ga ik graag even aan de slag met het opschonen en documenteren van de code!

### 3D-modellen

Op de lange termijn zou het mooi zijn als meer bijzondere gebouwen eigen, gedetailleerde 3D-modellen (met fotorealistische PBR-textures) krijgen. Daarnaast is er veel ander 3D-werk te doen, bijvoorbeeld het maken van lantaarnpalen en bruggen, het ontwikkelen van meer accurate PBR-textures voor bakstenen en bestrating en werk aan meer modulaire 3D-gevels.

## Vergelijkbare projecten

Er zijn op dit moment meerdere organisaties bezig met vergelijkbare of verwante projecten. Hopelijk komen daar ofwel veel mooiere (en vrij beschikbare) simulaties uit of komt het werk van deze projecten beschikbaar zodat het op een later tijdstip verwerkt kan worden in dit project. Interessant zijn in ieder geval:

- Het [4D Research Lab](http://4dresearchlab.nl/) van de Universiteit van Amsterdam. Ik heb begrepen dat ze op dit moment zelf bezig zijn met een (veel gedetailleerdere) 3D-reconstructie van de oude Jodenbuurt / Waterloopleinbuurt, in samenwerking met oa het Joods Historisch Museum. Mogelijk komen de achterliggende data en modellen op een later tijdstip open beschikbaar.
- [Amsterdam Time Machine](https://amsterdamtimemachine.nl/) ([Github repo](https://github.com/CLARIAH/ATM)). Bezig met het beschikbaar maken van historische vectorkaarten, rasterkaarten en daarnaast ook met gedetailleerde, lokale 3D-reconstructies. Vooral de gevectoriseerde kadasterdata (van HisGis) biedt kansen - in ieder geval de mogelijkheid om snel een simpele reconstructie te maken van de hele 19e-eeuwse stad. [Volgens de website](https://amsterdamtimemachine.nl/hisgis/) wordt deze data binnenkort openbaar toegankelijk. Verder een project om een 3D-reconstructie te maken van de hele stad: [ziet er veelbelovend uit](https://amsterdamtimemachine.nl/historical-amsterdam-in-3d/).
- [HisGIS](https://hisgis.nl/projecten/amsterdam/) voert een groot aantal historische GIS-projecten uit, waaronder een paar digitaliseringen van historisch Amsterdam.
