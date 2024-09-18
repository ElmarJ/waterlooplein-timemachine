// C# example.
using UnityEditor;
using UnityEngine;
using System;
using System.IO;

public static class AssetPreparationScripts
{    
    [MenuItem("Waterlooplein Time Machine/Prepare Assets/Copy GeoJson Git Data")]
    private static void  CopyGeoJsonGitData()
    {
        // Get the path to the git submodule
        string geoJsonPath = Path.GetFullPath($"{Application.dataPath}\\..\\..\\..\\data\\waterlooplein-timemachine-gisdata\\geojson");

        // Get the assets geojson path
        string assetsGeoJsonPath = Path.GetFullPath($"{Application.dataPath}\\..\\Assets\\GeoJSON");

        // Make a list of all the folders in the git submodule folder:
        string[] folders = Directory.GetDirectories(geoJsonPath);

        // Loop through all the folders

        foreach (string folder in folders)
        {
            // Get the folder name
            string folderName = Path.GetFileName(folder);

            // Get the path to the folder in the assets folder
            string assetsFolder = Path.GetFullPath($"{assetsGeoJsonPath}\\{folderName}");

            // Check if the folder exists in the assets folder
            if (!Directory.Exists(assetsFolder))
            {
                // If it doesn't exist, create it
                Directory.CreateDirectory(assetsFolder);
            }

            // Get all the files in the folder
            string[] files = Directory.GetFiles(folder);

            // Loop through all the files
            foreach (string file in files)
            {
                // Get the file name
                string fileName = Path.GetFileNameWithoutExtension(file);

                // Get the path to the file in the assets folder
                string assetsFile = Path.GetFullPath($"{assetsFolder}\\{fileName}.json");

                // Copy the file (with overwrite)
                File.Copy(file, assetsFile, true);
            }
        }

    }
}