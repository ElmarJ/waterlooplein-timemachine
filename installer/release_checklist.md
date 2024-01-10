# Publishing a new version of Waterlooplein Time Machine

## Prerequisites:
 - [ ] [Inno Setup](https://jrsoftware.org/isdl.php) installed?
 - [ ] Unity Build Components (Windows mono, Mac mono, Linux mono, Windows Store App) installed?
## Checks
 - [ ] No overlapping buildings? (check Mozes and Aaron!)
 - [ ] Project converted to latest version of Unity?

## Finalize code
 - [ ] In Unity, under Player Settings, update version number
 - [ ] In [installer/win32_setup_script.iss](./win32_setup_script.iss) update version number (MyAppVersion variable)
 - [ ] In [installer/win64_setup_script.iss](./win64_setup_script.iss) update version number (MyAppVersion variable)
 - [ ] Make sure all code changes are committed and pushed to master on Github.

## Build
 - [ ] Makes sure that the \builds\ folder is empty or non-existent
 - [ ] Make sure the editor Code Optimization Mode is set to Release Mode (and not Debug Mode)
 - [ ] Build for all platforms (Unity Menu: Waterlooplein Time Machine -> Build and Package -> Build All)

## Github release
- If anything changed since last commit, make sure to commit any updates.
- [Create release](https://github.com/ElmarJ/waterlooplein-timemachine/releases) for new version on Github. 
- Upload windows installers, mac zip and linux zip to release.

## Finalize and upload Windows Store App:

### Modify and build in Visual Studio
- [ ] Open *\Builds\winstore_source_x64\Waterlooplein Time Machine.sln* in Visual Studio.
- [ ] Associate with store app:
  - Right click Waterlooplein Time Machine project, and select Publish -> Associate with Microsoft Store App
  - Login with my Microsoft Account
  - Select "Include app names that already have packages"
  - Select Waterlooplein Time Machine
- [ ] In Package.appxmanifest:
  - [ ] Set correct version number under "Packaging" tab
  - [ ] Set default language "nl-NL"
  - [ ] Generate logo's, in Package.appxmanifest, under "Visual Assets" -> Asset Generator:
    - Set as Source "source\WaterloopleinTimeMachine\Assets\Images\Logos\StoreLogo.scale-400.png"
    - Click "Generate"
  - [ ] Set "splash screen background" to #000000.
- [ ] Set Build Configuration in toolbar to "MasterWithLTCG" with "x64" architecture. (note: [for shorter compilation time](https://forum.unity.com/threads/masterwithltcg.600499/), choose Master)
- [ ] Create App Package:
  - Right click Waterlooplein Time Machine project -> Publish -> Create App Packages
  - Select first option: "Microsoft Store as Waterlooplein Time Machine by Elmar Jansen"
  - Click Next
  - Set correct version number
  - Under "Generate app bundle", select "Never".
  - Make sure that only x64 architecture is selected
  - Click "Create"

### Upload to MS Partner Center.
- [ ] Create [a new Submission](https://partner.microsoft.com/en-us/dashboard/products/9PFFX4W0P498)
- [ ] Under Packages, upload \Builds\winstore_source_x64\AppPackages\Waterlooplein Time Machine\Waterlooplein Time Machine_0.XX.XX.X_x64_MasterWithLTCG.appxupload
- [ ] Add Dutch release notes under "Store Listings"
- [ ] Add English release notes under "Store Listings"

