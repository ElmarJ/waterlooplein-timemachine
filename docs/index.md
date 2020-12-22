---
layout: default
title: Waterlooplein 3D
nav_order: 1
---

# Waterlooplein 3D

![Screenshot](https://raw.githubusercontent.com/ElmarJ/Waterlooplein3D/master/source/Waterlooplein3D/Assets/Images/Luchtfoto.png "Luchtfoto in Waterlooplein 3D")

[![GitHub release (latest by date)](https://img.shields.io/github/v/release/elmarj/waterlooplein3d)](https://github.com/elmarj/waterlooplein3d/releases)
[![GitHub issues](https://img.shields.io/github/issues/elmarj/waterlooplein3d)](https://github.com/elmarj/waterlooplein3d/issues)
[![GitHub](https://img.shields.io/github/license/elmarj/waterlooplein3d)](https://github.com/ElmarJ/Waterlooplein3D/blob/master/COPYING)
[![GitHub stars](https://img.shields.io/github/stars/elmarj/waterlooplein3d?style=social)](https://github.com/elmarj/waterlooplein3d)
[![Twitter Follow](https://img.shields.io/twitter/follow/elmarj?style=social)](https://twitter.com/elmarj)

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y521CCD)

<a href='//www.microsoft.com/store/apps/9PFFX4W0P498?cid=storebadge&ocid=badge'><img src='https://github.com/ElmarJ/Waterlooplein3D/blob/master/WindowsStoreBadge.png?raw=true' alt='English badge' style='width: 142px; height: 52px;'/></a>


## Wat is het?

Waterlooplein 3D is een reconstructie van het stratenpatroon van de Waterloopleinbuurt van tussen 1800 en 1950. Het programma is bedoeld voor die mensen die (net als ik) moeite hebben zich voor te stellen hoe de buurt er ongeveer uit heeft gezien, voordat - door stadsvernieuwing en metrobouw - de buurt in de jaren '60 en '70 grotendeels is gesloopt. 

Met een schuifje kun je bepalen in welk jaar je precies rond wilt lopen (sinds versie 2.0). Verwacht geen perfecte reconstructie, het belangrijkste doel is om een beeld te geven van waar straten liepen en hoe de buurt in elkaar stak. Wat je ziet is een gemodelleerde stad waarin het stratenpatroon en de locatie van de gebouwen echt is, maar de gevels en gebouwhoogten willekeurig gegenereerd. Verwacht dus niet je eigen huis in detail terug te vinden: waarschijnlijk staat er een gebouw met de juiste omvang maar de verkeerde gevel.

Overigens zijn de straten van het hele Centrum te bewandelen, maar ontbreken buiten de Waterloopleinbuurt alle details zoals bomen, bruggen en individuele panden.

Als je een moderne webbrowser hebt, kun je een beperkte versie (versie 1.0) van de reconstructie bekijken op [Unity Play](https://play.unity.com/mg/other/waterlooplein-3d). Als je een goede computer hebt, ziet het er veel beter uit als je het programma downloadt en installeren.

## Installeren

### Windows
- Het makkelijkst is via de <a href='//www.microsoft.com/store/apps/9PFFX4W0P498'>vermelding in de Microsoft Store</a>.
- Maar je kunt zelf ook een installer downloaden. Je zult dan wel zelf naar deze pagina terug moeten komen voor updates. [Download 32-bits versie hier](https://github.com/ElmarJ/Waterlooplein3D/releases/latest/download/waterlooplein3d_win32_setup.exe) (109 mb) of [Download 64-bits versie hier](https://github.com/ElmarJ/Waterlooplein3D/releases/latest/download/waterlooplein3d_win64_setup.exe) (111 mb)

### Mac

- [Download zip-file hier](https://github.com/ElmarJ/Waterlooplein3D/releases/latest/download/waterlooplein3d_mac.zip) (114 MB) te downloaden.
- *Let op*: Ik heb zelf geen Mac, dus kan dit niet testen - maar het zou moeten werken.

### Linux (64 bit)

- [Download zip-file hier](https://github.com/ElmarJ/Waterlooplein3D/releases/latest/download/linux_x86_64.zip) (109 MB) te downloaden.
- Pak het zip-bestand uit
- Geef _execution_-rechten aan Waterlooplein_3D_linux64.x86_64 ```chmod +x Waterlooplein_3D_linux64.x86_64```
- Start ```./Waterlooplein_3D_linux64.x86_64```

## Versiegeschiedenis

 - Versie v1.0.0 (18 augustus 2020)
    - Eerste publieke relaese
    - 3D-reconstructie van Waterloopleinbuurt rond 1880.
 - Versie v2.0.0 (22 december 2020)
    - Tijdschuifje: je kunt nu zelf het jaar bepalen waarin je rondloopt
    - Nieuwe grafische techniek (HDRP), daardoor veel gedetailleerder en overtuigender beeld

## Open Source Projecten

Waterlooplein 3D maakt gebruik van de volgende open source software en bibliotheken:

### Github-pagina's bij dit project:

 - [Waterlooplein 3D project](https://github.com/ElmarJ/Waterlooplein3D): De source code van dit project.
 - [GeoJsonCityBuilder](https://github.com/elmarj/geojsoncitybuilder): Unity Package dat het mogelijk maakt een 3D-reconstructie te maken van een stad op basis van GeoJSON-data.
 - [Amsterdam.1892.GeoJSON](https://github.com/ElmarJ/Amsterdam.1892.GeoJSON): De achterliggende GeoJSON die de kaart van Amsterdam in 1880 beschrijft.

### Unity packages, 3D-materialen, modellen, shaders etc.

 - [First Person All In One by Aedan Graves](https://assetstore.unity.com/packages/tools/input-management/first-person-all-in-one-135316)
 - [#NVJOB Simple Water Shaders by Nicholas Veselov](https://nvjob.github.io/unity/nvjob-water-shader)
 - [Procedural Grass by Vincent Gault](https://share.substance3d.com/libraries/64)
 - [Cobble Stone Pavement by soi](https://share.substance3d.com/libraries/3721)
 - [Bricks by Wes McDermott](https://share.substance3d.com/libraries/2041)
 - [SpeedTree: Sample Broadleaf tree](https://speedtree.com/)
 
### Ontwikkeltools

 - [Unity](https://unity.com/)
 - [Visual Studio Code](https://code.visualstudio.com/)
 - [Blender](https://www.blender.org/)
 - [QGis](https://www.qgis.org/)
