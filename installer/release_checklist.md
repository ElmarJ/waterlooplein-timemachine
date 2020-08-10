# Publishing a new version of Waterlooplein3D
 - [ ] Under Player Settings, update version number
 - [ ] Make sure all code changes are committed and pushed to master on Github.

## Windows Installers:
 - [ ] Build Windows x86 to \Builds\win_x86
 - [ ] Build Windows x64 to \Builds\win_x64

## Windows Store:
- [ ] In Unity, build for Universal Windows Platform (leaving settings untouched) to \Builds\winstore_source_x64

- [ ] Open *\Builds\winstore_source_x64\Waterlooplein 3D.sln* in Visual Studio.
- [ ] [Add the following lines](https://forum.unity.com/threads/how-to-make-windows-build-to-use-dedicated-graphic-on-optimus-laptop.391194/) to Waterlooplein 3D\App.cpp (right under the "using" declarations):
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
- [ ] Turn off [FH4 exception handling](https://devblogs.microsoft.com/cppblog/making-cpp-exception-handling-smaller-x64/) to prevent [this problem](https://forum.unity.com/threads/uwp-certification-failure-desktop-xbox-build.758729/) from happening in certification (it's a [Windows Store Bug](https://developercommunity.visualstudio.com/content/problem/746534/visual-c-163-runtime-uses-an-unsupported-api-for-u.html)). *According to forum reply, this is no longer necessary for submissions. However, I still get this error locally if I remove this switch*
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
  - Make sure that only x64 architecture is selected
  - Click "Create"
- [ ] Run certification test


