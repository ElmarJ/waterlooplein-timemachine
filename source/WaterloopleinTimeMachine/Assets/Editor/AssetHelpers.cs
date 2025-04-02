using GeoJsonCityBuilder.Components;
using GeoJsonCityBuilder.Editor.Builders;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace WaterloopleinTimeMachine.Editor
{
    public static class AssetHelpers
    {
        [MenuItem("Waterlooplein Time Machine/Prepare Assets/Copy GeoJson Git Data")]
        public static void CopyGeoJsonGitData()
        {
            string gitGeoJsonPath = Path.GetFullPath($"{Application.dataPath}\\..\\..\\..\\data\\waterlooplein-timemachine-gisdata\\geojson");
            string assetsGeoJsonPath = Path.GetFullPath($"{Application.dataPath}\\..\\Assets\\GeoJSON");

            string[] gitSubFolders = Directory.GetDirectories(gitGeoJsonPath);

            foreach (string gitSubFolder in gitSubFolders)
            {
                string folderName = Path.GetFileName(gitSubFolder);
                string assetsSubFolder = Path.GetFullPath($"{assetsGeoJsonPath}\\{folderName}");

                // If folder doesn't exist in Assets, create it
                if (!Directory.Exists(assetsSubFolder))
                {
                    Directory.CreateDirectory(assetsSubFolder);
                }

                string[] gitFiles = Directory.GetFiles(gitSubFolder, "*.geojson");

                // Copy each file, replacing .geojson with .json extension
                foreach (string gitFile in gitFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(gitFile);
                    string assetsFile = Path.GetFullPath($"{assetsSubFolder}\\{fileName}.json");
                    File.Copy(gitFile, assetsFile, true);
                }
            }

            AssetDatabase.Refresh();
        }

        [MenuItem("Waterlooplein Time Machine/Prepare Assets/Regenerate All Content From GeoJson")]
        public static void RegenerateAllContentFromGeoJson()
        {
            // Find all GameObjects in the Scene (but not Prefab-assets) that have a BlocksFromGeoJson component
            var blocksImporters = (from go in Resources.FindObjectsOfTypeAll<BlocksFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var blocksCount = blocksImporters.Count;
            int blockIndex = 0;
            foreach (var block in blocksImporters)
            {
                EditorUtility.DisplayProgressBar("Building Blocks (step 1/3)", $"Generating {block.gameObject.name}", (float)blockIndex / blocksCount);
                var builder = new BlocksFromGeoJsonBuilder(block);
                builder.RemoveAllChildren();
                builder.Rebuild();

                blockIndex++;
            }

            var prefabsImporters = (from go in Resources.FindObjectsOfTypeAll<PrefabsFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var prefabsCount = prefabsImporters.Count;
            int prefabIndex = 0;
            foreach (var prefab in prefabsImporters)
            {
                EditorUtility.DisplayProgressBar("Building Prefabs (step 2/3)", $"Generating {prefab.gameObject.name}", (float)prefabIndex / prefabsCount);
                var builder = new PrefabsFromGeoJsonBuilder(prefab);
                builder.RemoveAllChildren();
                builder.Rebuild();
            }

            var bordersImporters = (from go in Resources.FindObjectsOfTypeAll<BordersFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var bordersCount = bordersImporters.Count;
            int borderIndex = 0;
            foreach (var border in bordersImporters)
            {
                EditorUtility.DisplayProgressBar("Building Borders (step 3/3)", $"Generating {border.gameObject.name}", (float)borderIndex / bordersCount);
                var builder = new BordersFromGeoJsonBuilder(border);
                builder.RemoveAllChildren();
                builder.Rebuild();
            }

            EditorUtility.ClearProgressBar();
        }

        [MenuItem("Waterlooplein Time Machine/Prepare Assets/Clear all generated content")]
        public static void ClearAllGeneratedContent()
        {
            // Find all GameObjects in the Scene (but not Prefab-assets) that have a BlocksFromGeoJson component
            var blocksImporters = (from go in Resources.FindObjectsOfTypeAll<BlocksFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var blocksCount = blocksImporters.Count;
            int blockIndex = 0;
            foreach (var block in blocksImporters)
            {
                EditorUtility.DisplayProgressBar("Removing Blocks (step 1/3)", $"Removing {block.gameObject.name}", (float)blockIndex / blocksCount);
                var builder = new BlocksFromGeoJsonBuilder(block);
                builder.RemoveAllChildren();

                blockIndex++;
            }

            var prefabsImporters = (from go in Resources.FindObjectsOfTypeAll<PrefabsFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var prefabsCount = prefabsImporters.Count;
            int prefabIndex = 0;
            foreach (var prefab in prefabsImporters)
            {
                EditorUtility.DisplayProgressBar("Removing Prefabs (step 2/3)", $"Removing {prefab.gameObject.name}", (float)prefabIndex / prefabsCount);
                var builder = new PrefabsFromGeoJsonBuilder(prefab);
                builder.RemoveAllChildren();
            }

            var bordersImporters = (from go in Resources.FindObjectsOfTypeAll<BordersFromGeoJson>() where !EditorUtility.IsPersistent(go) select go).ToList();
            var bordersCount = bordersImporters.Count;
            int borderIndex = 0;
            foreach (var border in bordersImporters)
            {
                EditorUtility.DisplayProgressBar("Removing Borders (step 3/3)", $"Removing {border.gameObject.name}", (float)borderIndex / bordersCount);
                var builder = new BordersFromGeoJsonBuilder(border);
                builder.RemoveAllChildren();
            }

            EditorUtility.ClearProgressBar();
        }

        [MenuItem("Waterlooplein Time Machine/Prepare Assets/Apply all Prefab Overrides")]
        public static void ApplyAllPrefabOverrides()
        {
            var prefabInstances = GameObject.FindGameObjectsWithTag("SceneOrganizationSingletonPrefab");

            int counter = 0;

            foreach(var go in prefabInstances)
            {
                Debug.Log($"Applying overrides to {go.name}");
                EditorUtility.DisplayProgressBar("Applying Overrides", $"Applying overrides to {go.name}", (float)counter++ / prefabInstances.Length);
                PrefabUtility.ApplyPrefabInstance(go, InteractionMode.AutomatedAction);
            }
        }
    }
}