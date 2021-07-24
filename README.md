# Waterlooplein 3D

![Screenshot](https://raw.githubusercontent.com/ElmarJ/Waterlooplein3D/master/source/Waterlooplein3D/Assets/Images/Luchtfoto.png "Luchtfoto in Waterlooplein 3D")

![GitHub release (latest by date)](https://img.shields.io/github/v/release/elmarj/waterlooplein3d)
![GitHub issues](https://img.shields.io/github/issues/elmarj/waterlooplein3d)
![GitHub](https://img.shields.io/github/license/elmarj/waterlooplein3d)

![GitHub stars](https://img.shields.io/github/stars/elmarj/waterlooplein3d?style=social)

![Twitter Follow](https://img.shields.io/twitter/follow/elmarj?style=social)

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/Y8Y521CCD)

This is the source code of the Waterlooplein 3D project. Waterlooplein 3D is a 3D reconstruction of the street pattern in the Amsterdam Waterlooplein neighborhood around 1870-1890. See the official website for more information (in Dutch) https://waterlooplein3d.nl.

Contributions (correcting underlying maps, programming or reporting bugs or requests) [are more than welcome (page in Dutch)](./docs/contributing.md)!

 - Comments of any sort can be left at the [discussions page](https://github.com/ElmarJ/Waterlooplein3D/discussions)
 - You can report bugs and requests on the [issues page](https://github.com/elmarj/waterlooplein3d/issues).
 - Modifications to the underlying map material (in GeoJSON) can be done in a seperate Github project: [Amsterdam.1892.GeoJSON](https://github.com/ElmarJ/Amsterdam.1892.GeoJSON).
 - Contributions to the software, 3D-models and PBR-materials are also more than welcome! For more information about my future plans, see the [issues pagina](https://github.com/elmarj/waterlooplein3d/issues) and the [roadmap (in Dutch)](./docs/roadmap.md) in the docs folder. The software is based on the Unity engine. The code that translates Geojson into Unity-buildings has been housed in [another Github project](https://github.com/ElmarJ/GeoJsonCityBuilder), but can also be edited directly from this repo (as git submodule).

## Setup your dev environment
 1. Install [Unity](https://store.unity.com/#plans-individual)
 2. Install [VS Code](https://code.visualstudio.com/) (or Visual Studio)
 3. Install [Git](https://git-scm.com/)
 4. Clone this repo (including submodules):
 
 ```
 git clone --recurse-submodules -j8 https://github.com/ElmarJ/Waterlooplein3D.git
 ```
 
 5. In Unity, open the folder "./source/Waterlooplein3D" (from the cloned repo).
 6. Install the [Substance Plugin from the Asset Store](https://assetstore.unity.com/packages/tools/utilities/substance-in-unity-110555) 
 7. Optionally, setup VS Code as the default editor (Edit > Preferences > External Tools > External Script Editor).
 8. Open MainScene.unity in /Waterlooplein3D/Assets/Scenes (File > Open Scene)

When switching between version branches (particularly v1.0-branch and v2.0-branch), always cleanup any ignored unity cache files whenever switching branches (```git checkout```), using the following command:
```
git clean -X -f -d
```
Failing to do so might result in a crashing Unity editor. 
