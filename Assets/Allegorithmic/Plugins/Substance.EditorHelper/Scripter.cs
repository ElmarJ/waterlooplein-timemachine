
using System;
using UnityEditor;
using UnityEngine;
using System.Runtime.InteropServices;

using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace Substance.EditorHelper
{
    public class Scripter
    {
        public static void Hello()
        {
            Debug.Log("Scripter's Hello");

#if UNITY_2019_3_OR_NEWER
            Debug.Log("UNITY_2019_3_OR_NEWER");
#endif
        }

        // NOTE: The following functions will be re-assessed when adding URP context...

        public static bool IsInstallingHDRP()
        {
#if UNITY_2019_3_OR_NEWER
            bool bInstalled = false;

            UnityEngine.Rendering.RenderPipelineAsset asset;
            asset = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset;

            if ((asset != null) &&
                (asset.GetType().ToString().EndsWith(".HDRenderPipelineAsset")))
            {
                bInstalled = true;
            }

            return bInstalled;
#else
            return false;
#endif
        }

        public static bool IsHDRP()
        {
            bool bHDRP = false;

#if UNITY_2019_3_OR_NEWER
            if (IsInstallingHDRP())
                bHDRP = true;
#endif

            return bHDRP;
        }
    }
}