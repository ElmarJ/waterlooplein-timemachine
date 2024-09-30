using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace WaterloopleinTimeMachine.Editor
{
    [InitializeOnLoad]
    public static class SceneSaveHooks
    {
        static SceneSaveHooks()
        {
            EditorSceneManager.sceneSaving += OnSceneSaving;
            EditorSceneManager.sceneSaved += OnSceneSaved;
            EditorSceneManager.sceneOpening += OnSceneOpening;
            EditorApplication.delayCall += OnDelayCall;
        }

        private static void OnDelayCall()
        {
            AssetHelpers.RegenerateAllContentFromGeoJson();
        }

        private static void OnSceneOpening(string path, OpenSceneMode mode)
        {
            AssetHelpers.RegenerateAllContentFromGeoJson();
        }

        private static void OnSceneSaving(Scene scene, string path)
        {
            AssetHelpers.ClearAllGeneratedContent();
            AssetHelpers.ApplyAllPrefabOverrides();
        }

        private static void OnSceneSaved(Scene scene)
        {
            AssetHelpers.RegenerateAllContentFromGeoJson();
        }
    }
}