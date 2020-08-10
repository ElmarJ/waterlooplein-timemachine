# Publishing a new version of Waterlooplein3D

## Checks
 - [ ] No overlapping buildings? (check Mozes and Aaron!)
 - [ ] Project converted to latest version of Unity? 

## Finalize code
 - [ ] Under Player Settings, update version number
 - [ ] All code changes are committed and pushed to master on Github.

## Create Windows installers

### Build in Unity
 - [ ] Build Windows x86 to \Builds\win_x86
 - [ ] Build Windows x64 to \Builds\win_x64

### Packaging 32-bit version
 - [ ] Open installer\win32_setup_script.iss in Inno Setup
 - [ ] Update version number (MyAppVersion variable)
 - [ ] Run script

### Packaging 64-bit version
 - [ ] Open installer\win64_setup_script.iss in Inno Setup
 - [ ] Update version number (MyAppVersion variable)
 - [ ] Run script

## Create and upload Windows Store App:

### Build in Unity
- [ ] In Unity, build for Universal Windows Platform (leaving settings untouched) to \Builds\winstore_source_x64

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
- [ ] Turn off [FH4 exception handling](https://devblogs.microsoft.com/cppblog/making-cpp-exception-handling-smaller-x64/) to prevent [this problem](https://forum.unity.com/threads/uwp-certification-failure-desktop-xbox-build.758729/) from happening in certification (it's a [Windows Store Bug](https://developercommunity.visualstudio.com/content/problem/746534/visual-c-163-runtime-uses-an-unsupported-api-for-u.html)). *According to forum reply, this is no longer necessary for submissions. However, I still get this error locally if I remove this switch.*
  - [ ] Right click Waterlooplein 3D project, and select "Properties".
  - [ ] Under C/C++ -> Command Line -> Additional Options, add "-d2FH4-".
  - [ ] Under Linker -> Command Line -> Additional Options, add "-d2:-FH4-".
- [ ] Remove Substance dll's from Unity Data project:
  - [ ] Right click Substance.Engine.dll and select "Exclude from project"
  - [ ] Right click substance_sse2_blend.dll and select "Exclude from project"
- [ ] Test by running on local machine
- [ ] Create App Package:
  - Right click Waterlooplein 3D project -> Publish -> Create App Packages
  - Select first option: "Microsoft Store as Waterlooplein 3D by Elmar Jansen"
  - Click Next
  - Set correct version number
  - Under "Generate app bundle", select "Never".
  - Make sure that only x64 architecture is selected
  - Click "Create"
- [ ] Run certification test

### Upload to MS Partner Center.
- [ ] Create [a new Submission](https://partner.microsoft.com/en-us/dashboard/products/9PFFX4W0P498)
- [ ] Under Packages, upload \Builds\winstore_source_x64\AppPackages\Waterlooplein 3D\Waterlooplein 3D_0.XX.XX.X_x64_MasterWithLTCG.appxupload
- [ ] Add Dutch release notes under "Store Listings"
- [ ] Add English release notes under "Store Listings"

## Create Mac release

### Build
 - [ ] Build Mac release in Unity.
 - [ ] Rename the .app-folder Waterlooplein3D.app
 - [ ] Zip the .app-folder.
 - [ ] Rename the zip waterlooplein3d_mac.zip

### Package as .dmg (doesn't work - only keeping this here for later reference)
 - Open an Ubuntu prompt in the folder that contains the MacOSX-build (so the folder containing the Waterlooplein3D.App folder).
 - [Use the following commands](https://askubuntu.com/questions/1117461/how-do-i-create-a-dmg-file-on-linux-ubuntu-for-macos) to create Mac .dmg. Replace the "count=16" part with the number of MBs needed for the build.

```bash
sudo apt-get install hfsprogs
dd if=/dev/zero of=/tmp/waterlooplein3d.dmg bs=1M count=16 status=progress
mkfs.hfsplus -v Install /tmp/waterlooplein3d.dmg
sudo mkdir -pv /mnt/tmp
sudo mount -o loop /tmp/waterlooplein3d.dmg /mnt/tmp
sudo cp -av Waterlooplein3d.app /mnt/tmp
sudo umount /mnt/tmp
mv /tmp/waterlooplein3d.dmg .
```

## Github release
- If anything changed since last commit, make sure to commit any updates.
- [Create release](https://github.com/ElmarJ/Waterlooplein3D/releases) for new version on Github. 
