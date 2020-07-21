# Waterlooplein 3D

![Screenshot](https://raw.githubusercontent.com/ElmarJ/Waterlooplein3D/master/source/Waterlooplein3D/Assets/Images/Luchtfoto.png "Luchtfoto in Waterlooplein 3D")

![GitHub release (latest by date)](https://img.shields.io/github/v/release/elmarj/waterlooplein3d)
![GitHub issues](https://img.shields.io/github/issues/elmarj/waterlooplein3d)
![GitHub](https://img.shields.io/github/license/elmarj/waterlooplein3d)

![GitHub stars](https://img.shields.io/github/stars/elmarj/waterlooplein3d?style=social)

![Twitter Follow](https://img.shields.io/twitter/follow/elmarj?style=social)

Welkom bij de repo van Waterlooplein 3D. Waterlooplein 3D is een reconstructie van het stratenpatroon van de Waterloopleinbuurt van rond 1870-1890. Zie voor meer informatie https://waterlooplein3d.nl of de docs-map in deze repo.

Bijdragen (verbeteren kaartmateriaal, programmeerwerk en het melden van bugs of wensen) [zijn meer dan welkom](./docs/contribute)!

 - Bugs en wensen kunnen gemeld worden op de [issues pagina](https://github.com/elmarj/waterlooplein3d/issues).
 - Aanpassingen aan kaartmateriaal kunnen worden gedaan in een ander Github-project: [Amsterdam.1892.GeoJSON](https://github.com/ElmarJ/Amsterdam.1892.GeoJSON).
 - Bijdragen aan de software, 3D-modellen en 3D-materialen zijn ook meer dan welkom! Zie voor mijn plannen de [issues pagina](https://github.com/elmarj/waterlooplein3d/issues) en de [roadmap](./docs/roadmap) in de docs. De gebruikte engine is Unity. De code die Geojson omzet in Unity-gebouwen is ondergebracht in een ander Github-project, maar kan ook direct vanuit deze repo bewerkt worden (als git submodule).

## Zelf in Unity aan de slag gaan
 1. Installeer [Unity](https://store.unity.com/#plans-individual)
 2. Installeer [VS Code](https://code.visualstudio.com/) (of Visual Studio)
 3. Installeer [Git](https://git-scm.com/)
 4. Clone deze repo inclusief submodules:
 
    git clone --recurse-submodules -j8 https://github.com/ElmarJ/Waterlooplein3D.git
 
 5. Open in Unity uit de geclonede repo, de map "./source/Waterlooplein3D".
 6. Stel eventueel VS Code in als standaard-editor (Edit > Preferences > External Tools > External Script Editor).
 7. Open de standaard Scene (File > Open Scene) MainScene.unity in /Waterlooplein3D/Assets/Scenes

