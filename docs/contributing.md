---
layout: default
title: Meehelpen
nav_order: 4
---

# Meehelpen

Bijdragen zijn meer dan welkom! Bugs rapporteren en nieuwe functies suggereren, doe je op [de "issues" pagina](https://github.com/ElmarJ/Waterlooplein3D/issues) van dit project, maar je kunt ook op andere manieren bijdragen:

## Donatie

Dit is een hobbyprojectje en ik hoef er natuurlijk geen geld mee te verdienen. Wel heb ik kleine kosten: vanwege de omvang van de bestanden kost het hosten op Github me 5 euro per maand. Als je dit project leuk vindt zou het heel fijn zijn als je zin hebt om iets bij te dragen - dat kan hier: [![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y521CCD).

## Uitbreiden en detailleren van de onderliggende kaarten 

De structuur van de buurt komt uit gedigitaliseerde kaarten. Het werk aan deze (onder het 3D-model liggende) kaarten heb ik ondergebracht in een [apart open source project](https://github.com/ElmarJ/Amsterdam.1892.GeoJSON). Het bewerken kan redelijk eenvoudig in het open source GIS-programma QGIS.

## Programmeren (C# / Unity)

De achterliggende code bestaat uit twee losse projecten, beide beschikbaar als open source:
- [Een project voor een Unity-plugin](https://github.com/ElmarJ/GeoJsonCityBuilder), waarmee automatisch objecten kunnen worden geplaatst en gecreëerd op basis van GeoJSON-data
- Dit Github-project voor de specifieke 3D reconstructie van de Waterloopleinbuurt / Amsterdam.

Code is nu nog wat ongestructureerd en weinig gedocumenteerd (typisch eenpersoonsproject). Mochten er mensen zin hebben om mee te doen dan ga ik graag even aan de slag met het opschonen en documenteren van de code!

## 3D-modellen

Op de lange termijn zou het mooi zijn als meer bijzondere gebouwen eigen, gedetailleerde 3D-modellen (met fotorealistische PBR-textures) krijgen. Daarnaast is er veel ander 3D-werk te doen, bijvoorbeeld het maken van lantaarnpalen en bruggen, het ontwikkelen van meer accurate PBR-textures voor bakstenen en bestrating en werk aan meer modulaire 3D-gevels.