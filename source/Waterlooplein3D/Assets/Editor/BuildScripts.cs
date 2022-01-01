// C# example.
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;

public static class BuildScripts
{
    const string innoSetupPath = "C:\\Program Files (x86)\\Inno Setup 6\\compil32.exe";
    static readonly string[] scenes = new string[] { "Assets/Scenes/MainScene.unity" };

    static string GetProjectPath() => Path.GetFullPath($"{Application.dataPath}\\..\\..\\..");
    static string GetBuildPath(string platformFolder) => $"{GetProjectPath()}\\builds\\{platformFolder}";
    static string GetInstallerScriptPath(string installerFile) => $"{GetProjectPath()}\\installer\\{installerFile}";

    [MenuItem("Waterlooplein 3D/Build and Package/Build All")]
    public static void BuildAllPlatforms()
    {
        BuildWinX86();
        BuildWinX64();
        BuildLinuxX8664();
        BuildMacOsx64();
        BuildWindowsStore();
    }

    [MenuItem("Waterlooplein 3D/Build and Package/Windows Store VS Project")]
    private static void BuildWindowsStore()
    {
        var winstorePath = GetBuildPath("winstore_source_x64");
        Build(winstorePath, BuildTarget.WSAPlayer);
    }

    [MenuItem("Waterlooplein 3D/Build and Package/Mac OSX 64 zip")]
    private static void BuildMacOsx64()
    {
        var mac64Path = GetBuildPath("mac_x86\\Waterlooplein3D.app");
        Build(mac64Path, BuildTarget.StandaloneOSX);
        ZipFile.CreateFromDirectory(mac64Path, GetProjectPath() + "\\Builds\\waterlooplein3d_mac.zip", System.IO.Compression.CompressionLevel.Optimal, true);
    }

    [MenuItem("Waterlooplein 3D//Build and Package/Linux x86_64 zip")]
    private static void BuildLinuxX8664()
    {
        var linuxX8664Path = GetBuildPath("linux_x86_64");
        Build(linuxX8664Path + "\\Waterlooplein3D.x86_64", BuildTarget.StandaloneLinux64);
        ZipFile.CreateFromDirectory(linuxX8664Path, GetProjectPath() + "\\Builds\\linux_x86_64.zip", System.IO.Compression.CompressionLevel.Optimal, false);
    }

    [MenuItem("Waterlooplein 3D/Build and Package/Windows x86 installer")]
    public static void BuildWinX86()
    {
        var winX86Path = GetBuildPath("win_x86");
        var win32InstallerScriptPath = GetInstallerScriptPath("win32_setup_script.iss");
        Build(winX86Path + "\\Waterlooplein 3D.exe", BuildTarget.StandaloneWindows);
        CompileInnoSetupScript(win32InstallerScriptPath);
    }

    [MenuItem("Waterlooplein 3D/Build and Package/Windows x64 installer")]
    public static void BuildWinX64()
    {
        var winX64Path = GetBuildPath("win_x64");
        var win64InstallerScriptPath = GetInstallerScriptPath("win64_setup_script.iss");
        Build(winX64Path + "\\Waterlooplein 3D.exe", BuildTarget.StandaloneWindows64);
        CompileInnoSetupScript(win64InstallerScriptPath);
    }

    private static void CompileInnoSetupScript(string scriptPath)
    {
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = innoSetupPath;
        proc.StartInfo.Arguments = " /cc " + scriptPath;
        proc.Start();
        proc.WaitForExit();
    }

    private static void Build(string buildPath, BuildTarget platform)
    {
        Debug.Log($"Starting {platform} build to: {buildPath}");
        try
        {
            BuildPipeline.BuildPlayer(scenes, buildPath, platform, BuildOptions.None);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Build Error: {ex.Message}");
            throw ex;
        }
        Debug.Log($"Finished {platform} build");
    }
}