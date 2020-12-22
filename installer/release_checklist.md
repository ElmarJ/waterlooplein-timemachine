# Publishing a new version of Waterlooplein3D

## Prerequisites:
 - [ ] [Inno Setup](https://jrsoftware.org/isdl.php) installed?
 - [ ] Unity Build Components (Windows mono, Mac mono, Linux mono, Windows Store App) installed?
## Checks
 - [ ] No overlapping buildings? (check Mozes and Aaron!)
 - [ ] Project converted to latest version of Unity?
 - [ ] Substance binaries excluded from runtime? Make sure to only select "Editor" for all binaries in Substance folder.

## Finalize code
 - [ ] In Unity, under Player Settings, update version number
 - [ ] In [installer/win32_setup_script.iss](./win32_setup_script.iss) update version number (MyAppVersion variable)
 - [ ] In [installer/win64_setup_script.iss](./win64_setup_script.iss) update version number (MyAppVersion variable)
 - [ ] Make sure all code changes are committed and pushed to master on Github.

## Build
 - [ ] Makes sure that the \builds\ folder is empty or non-existent
 - [ ] Build for all platforms (Unity Menu: Waterlooplein 3D -> Build and Package -> Build All)

## Github release
- If anything changed since last commit, make sure to commit any updates.
- [Create release](https://github.com/ElmarJ/Waterlooplein3D/releases) for new version on Github. 
- Upload windows installers, mac zip and linux zip to release.

## Finalize and upload Windows Store App:

### Modify and build in Visual Studio
- [ ] Open *\Builds\winstore_source_x64\Waterlooplein 3D.sln* in Visual Studio.
- [ ] Add the following lines [(to make sure that dedicated GPU is used)](https://forum.unity.com/threads/how-to-make-windows-build-to-use-dedicated-graphic-on-optimus-laptop.391194/) to Waterlooplein 3D\App.cpp (right under the "using" declarations):
```cpp
extern "C" {
    _declspec(dllexport) DWORD NvOptimusEnablement = 0x00000001;
}
extern "C"
{
    __declspec(dllexport) int AmdPowerXpressRequestHighPerformance = 1;
}
```
- [ ] Associate with store app:
  - Right click Waterlooplein 3D project, and select Publish -> Associate with Microsoft Store App
  - Login with my Microsoft Account
  - Select "Include app names that already have packages"
  - Select Waterlooplein 3D
- [ ] In Package.appxmanifest:
  - [ ] Set correct version number under "Packaging" tab
  - [ ] Set default language "nl-NL"
  - [ ] Generate logo's, in Package.appxmanifest, under "Visual Assets" -> Asset Generator:
    - Set as Source "source\Waterlooplein3D\Assets\Images\Logos\StoreLogo.scale-400.png"
    - Click "Generate"
  - [ ] Set "splash screen background" to #000000.
- [ ] Set Build Configuration in toolbar to "MasterWithLTCG" with "x64" architecture. (note: [for shorter compilation time](https://forum.unity.com/threads/masterwithltcg.600499/), choose Master)
- [ ] Create App Package:
  - Right click Waterlooplein 3D project -> Publish -> Create App Packages
  - Select first option: "Microsoft Store as Waterlooplein 3D by Elmar Jansen"
  - Click Next
  - Set correct version number
  - Under "Generate app bundle", select "Never".
  - Make sure that only x64 architecture is selected
  - Click "Create"

### Upload to MS Partner Center.
- [ ] Create [a new Submission](https://partner.microsoft.com/en-us/dashboard/products/9PFFX4W0P498)
- [ ] Under Packages, upload \Builds\winstore_source_x64\AppPackages\Waterlooplein 3D\Waterlooplein 3D_0.XX.XX.X_x64_MasterWithLTCG.appxupload
- [ ] Add Dutch release notes under "Store Listings"
- [ ] Add English release notes under "Store Listings"

