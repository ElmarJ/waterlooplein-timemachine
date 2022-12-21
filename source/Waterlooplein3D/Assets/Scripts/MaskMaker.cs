/*
 * Do-what-you-want-with-it License :)
 * I am releasing this a Public Domain CC0 License
 * Anyone can use/improve this as they see fit personal/educational/commercial
 * Does this even count as a license?
 * Doesn't matter, I'm not going to sue over it :)
 */

using UnityEngine;
using UnityEditor;
using System.IO;

public class MaskMaker : EditorWindow {

    //Preview Textures
    private Texture2D Albedo;
    private Texture2D Normal;

    private Texture2D Metallic;
    private Texture2D AmbientOcclusion;
    private Texture2D DetailMask;
    private Texture2D Smooth_Rough;

    private Texture2D r_Metallic;
    private Texture2D r_AmbientOcclusion;
    private Texture2D r_DetailMask;
    private Texture2D r_Smooth_Rough;

    private float defaultMetal;
    private float defaultAO = 1f;
    private float defaultDetail;
    private float defaultSmooth;

    private Texture2D finalTexture;
    private bool allCorrectSizes;
    private bool useRough;
    private bool changed;

    private Vector2Int texSize;
    private static EditorWindow window;
    private Vector2 scrollPos;

    private Editor previewMatViewer;
    private Material previewMat;

    private bool MaterialMade;

    [MenuItem("Tools/Mask Map Packer")]
    public static void ShowWindow()
    {
        window = GetWindow(typeof(MaskMaker), false);
    }
    [MenuItem("Assets/Mask Map Packer")]
    public static void ShowWindowRightClick()
    {
        window = GetWindow(typeof(MaskMaker), false);
    }
    private void OnInspectorUpdate()
    {
        if(!window)
            window = GetWindow(typeof(MaskMaker), false);
    }
    private void OnGUI()
    {
        if (window)
        {
            GUILayout.BeginArea(new Rect(0, 0, window.position.size.x, window.position.size.y));
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
        }
            


        GUIStyle BigBold = new GUIStyle();
        BigBold.fontSize = 16;
        BigBold.fontStyle = FontStyle.Bold;
        BigBold.wordWrap = true;
        BigBold.alignment = TextAnchor.MiddleCenter;

        GUIStyle Wrap = new GUIStyle();
        Wrap.wordWrap = true;
        Wrap.alignment = TextAnchor.MiddleCenter;

        GUIStyle warn = new GUIStyle();
        warn.richText = true;
        warn.wordWrap = true;
        warn.fontStyle = FontStyle.Bold;
        warn.alignment = TextAnchor.MiddleCenter;
        warn.normal.textColor = new Color(0.7f,0,0);

        GUIStyle preview = new GUIStyle();
        preview.alignment = TextAnchor.UpperCenter;

        GUILayout.Space(10f);
        GUILayout.Label("Add grayscale images to be packed", BigBold);
        GUILayout.Label("Textures must be the same width and height", BigBold);
        GUILayout.Space(10f);
        GUILayout.Label("For accurate results, make sure your textures have sRGB unchecked", warn);
        GUILayout.Space(10f);

        //Metallic
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        Metallic = (Texture2D)EditorGUILayout.ObjectField("Metallic (R)", Metallic, typeof(Texture2D), false);
        if (!Metallic)
        {
            GUILayout.Label("No Metallic image found, use slider to set value", Wrap);
            defaultMetal = EditorGUILayout.Slider(defaultMetal, 0f, 1f);
        }
        if (Metallic && texSize == Vector2Int.zero)
            texSize = new Vector2Int(Metallic.width, Metallic.height);
        if (Metallic && texSize != new Vector2Int(Metallic.width, Metallic.height))
            Metallic = null;
        
        GUILayout.Space(10f);
        GUILayout.EndVertical();

        //Ambient Occlusion
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        AmbientOcclusion = (Texture2D)EditorGUILayout.ObjectField("Ambient Occlusion (G)", AmbientOcclusion, typeof(Texture2D), false);
        if (!AmbientOcclusion)
        {
            GUILayout.Label("No Ambient Occlusion image found, use slider to set value", Wrap);
            defaultAO = EditorGUILayout.Slider(defaultAO, 0f, 1f);
        }
        if (AmbientOcclusion && texSize == Vector2Int.zero)
            texSize = new Vector2Int(AmbientOcclusion.width, AmbientOcclusion.height);
        if (AmbientOcclusion && texSize != new Vector2Int(AmbientOcclusion.width, AmbientOcclusion.height))
            AmbientOcclusion = null;

        GUILayout.Space(10f);
        GUILayout.EndVertical();

        //Detail Mask
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        DetailMask = (Texture2D)EditorGUILayout.ObjectField("Detail Mask (B)", DetailMask, typeof(Texture2D), false);
        if (!DetailMask)
        {
            GUILayout.Label("No Detail Mask image found, use slider to set value", Wrap);
            defaultDetail = EditorGUILayout.Slider(defaultDetail, 0f, 1f);
        }
        if (DetailMask && texSize == Vector2Int.zero)
            texSize = new Vector2Int(DetailMask.width, DetailMask.height);
        if (DetailMask && texSize != new Vector2Int(DetailMask.width, DetailMask.height))
            DetailMask = null;

        GUILayout.Space(10f);
        GUILayout.EndVertical();

        //Roughness/Smoothness
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        if (useRough = EditorGUILayout.Toggle("Input Is Roughness Map", useRough))
            Smooth_Rough = (Texture2D)EditorGUILayout.ObjectField("Rough Map (A)", Smooth_Rough, typeof(Texture2D), false);
        else
            Smooth_Rough = (Texture2D)EditorGUILayout.ObjectField("Smoothness Map (A)", Smooth_Rough, typeof(Texture2D), false);
        if (!Smooth_Rough)
        {
            GUILayout.Label("No Smoothness or Roughness image found, use slider to set value", Wrap);
            defaultSmooth = EditorGUILayout.Slider(defaultSmooth, 0f, 1f);
            if(defaultSmooth == 0)
                GUILayout.Label("Slider set to 0, preview image will display alpha of 1", Wrap);
        }
        if (Smooth_Rough && texSize == Vector2Int.zero)
            texSize = new Vector2Int(Smooth_Rough.width, Smooth_Rough.height);
        if (Smooth_Rough && texSize != new Vector2Int(Smooth_Rough.width, Smooth_Rough.height))
            Smooth_Rough = null;

        GUILayout.Space(10f);
        GUILayout.EndVertical();

        //Preview Albedo
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        Albedo = (Texture2D)EditorGUILayout.ObjectField("Albedo (Preview)", Albedo, typeof(Texture2D), false);

        GUILayout.Space(10f);
        GUILayout.EndVertical();

        //Preview Normal
        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(10f);
        Normal = (Texture2D)EditorGUILayout.ObjectField("Normal Map (Preview)", Normal, typeof(Texture2D), false);

        GUILayout.Space(10f);
        GUILayout.EndVertical();


        if (!Metallic && !AmbientOcclusion && !DetailMask && !Smooth_Rough)
        {
            texSize = Vector2Int.zero;
            GUILayout.Label("No Textures selected", Wrap);
        }
        else
        {
            if (finalTexture)
            {
                if(!previewMat)
                    previewMat = new Material(Shader.Find("HDRP/Lit"));

                if(previewMat != null)
                {
                    if (previewMatViewer == null)
                        previewMatViewer = Editor.CreateEditor(previewMat);

                    if(!MaterialMade)
                    {
                        //_HEIGHTMAP _MASKMAP _NORMALMAP _NORMALMAP_TANGENT_SPACE
                        if (Albedo)
                            previewMat.SetTexture("_BaseColorMap", Albedo);

                        if (Normal)
                        {
                            previewMat.EnableKeyword("_NORMALMAP");
                            previewMat.EnableKeyword("_NORMALMAP_TANGENT_SPACE");
                            previewMat.SetTexture("_NormalMap", Normal);
                        }

                        previewMat.EnableKeyword("_MASKMAP");
                        previewMat.SetTexture("_MaskMap", finalTexture);
                        MaterialMade = true;
                    }

                    if (MaterialMade && previewMatViewer != null)
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        GUILayout.Space(10f);
                        previewMatViewer.OnPreviewGUI(GUILayoutUtility.GetRect(256, 256), EditorStyles.objectField);
                        GUILayout.Space(10f);
                        GUILayout.EndVertical();
                    }
                }
            }
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(10f);
            if (GUILayout.Button("Update Preview Texture"))
            {
                MaterialMade = false;
                EditorUtility.DisplayProgressBar("Generating Preview, please wait...", "", 1f);
                UpdateTexture(true);
            }
            GUILayout.Space(5f);
            if (GUILayout.Button("Pack and Save Texture"))
            {
                EditorUtility.DisplayProgressBar("Packing Textures, please wait...", "", 1f);
                PackTextures();
            }
            GUILayout.Space(5f);
            if (GUILayout.Button("Clear All"))
            {
                MaterialMade = false;
                Albedo = Normal = Metallic = AmbientOcclusion = DetailMask = Smooth_Rough = finalTexture = null;
                previewMat = null;
                previewMatViewer = null;
            }
            GUILayout.Space(10f);
            GUILayout.EndVertical();
        }
        
        GUILayout.Label("Output texture will be the same height and width as input", Wrap);
        
        GUILayout.Space(100);
        if (window)
        {
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    private void UpdateTexture(bool asPreview)
    {
        finalTexture = new Texture2D(texSize.x, texSize.y, TextureFormat.RGBAFloat, true);

        if (Metallic)
            r_Metallic = duplicateTexture(Metallic);
        if (AmbientOcclusion)
            r_AmbientOcclusion = duplicateTexture(AmbientOcclusion);
        if (DetailMask)
            r_DetailMask = duplicateTexture(DetailMask);
        if (Smooth_Rough)
            r_Smooth_Rough = duplicateTexture(Smooth_Rough);

        for (int x = 0; x < texSize.x; x++)
        {
            for (int y = 0; y < texSize.y; y++)
            {
                float R, G, B, A;
                if (Metallic)
                    R = r_Metallic.GetPixel(x, y).r;
                else
                    R = defaultMetal;

                if (AmbientOcclusion)
                    G = r_AmbientOcclusion.GetPixel(x, y).r;
                else
                    G = defaultAO;

                if (DetailMask)
                    B = r_DetailMask.GetPixel(x, y).r;
                else
                    B = defaultDetail;

                if (Smooth_Rough)
                {
                    if (useRough)
                        A = 1 - r_Smooth_Rough.GetPixel(x, y).r;
                    else
                        A = r_Smooth_Rough.GetPixel(x, y).r;
                }
                else
                {
                    if (useRough)
                        A = 1 - defaultSmooth;
                    else
                        A = defaultSmooth;
                }

                finalTexture.SetPixel(x, y, new Color(R, G, B, A));
            }
        }
        finalTexture.Apply();
        EditorUtility.ClearProgressBar();
    }

    private void PackTextures()
    {
        UpdateTexture(false);
        
        var path = EditorUtility.SaveFilePanelInProject("Save Texture To Diectory", "LitMask", "png", "Saved");
        var pngData = finalTexture.EncodeToPNG();
        if (path.Length != 0)
        {
            if (pngData != null)
            {
                File.WriteAllBytes(path, pngData);
            }
        }
        AssetDatabase.Refresh();
       
        Debug.Log("Texture Saved to: " + path);
    }


    Texture2D duplicateTexture(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(source.width,source.height,0,RenderTextureFormat.ARGBFloat,RenderTextureReadWrite.sRGB);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}