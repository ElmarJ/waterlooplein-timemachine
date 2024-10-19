using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace WaterloopleinTimeMachine.Editor
{
    [BuildCallbackVersion(1)]
    class MyCustomBuildProcessor : IProcessSceneWithReport
    {
        public int callbackOrder { get { return 0; } }
        public void OnProcessScene(UnityEngine.SceneManagement.Scene scene, BuildReport report)
        {
            if (scene.name == "MainScene")
            {
                AssetHelpers.RegenerateAllContentFromGeoJson();
            }
        }
    }
}