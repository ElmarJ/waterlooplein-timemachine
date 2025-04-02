using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Build;
using UnityEditor.Rendering;
using System.Linq;

// Strips shader variants that are not in the specified ShaderVariantCollection.
// The shader variants are passed in as a list of ShaderCompilerData objects.
class WhitelistShaderStripper : IPreprocessShaders
{
    const string k_ShaderVariantCollectionPath = "Assets/Editor/ShaderStripping/ShaderWhitelist.shadervariants";

    ShaderKeyword m_KeywordToStrip;
    ShaderVariantCollection m_ShaderVariantCollection;

    public WhitelistShaderStripper()
    {
        m_ShaderVariantCollection = AssetDatabase.LoadAssetAtPath<ShaderVariantCollection>(k_ShaderVariantCollectionPath);

        if (m_ShaderVariantCollection == null)
        {
            throw new System.Exception("Shader variant collection not found at " + k_ShaderVariantCollectionPath);
        }
    }

    // Use callbackOrder to set when Unity calls this shader preprocessor. Unity starts with the preprocessor that has the lowest callbackOrder value.
    public int callbackOrder { get { return 0; } }

    public void OnProcessShader(Shader shader, ShaderSnippetData snippet, IList<ShaderCompilerData> variantCompilerDatas)
    {
        // Compile a list with variant-data of variants to strip:
        var dataToStrip = new List<ShaderCompilerData>();

        foreach (var variantData in variantCompilerDatas)
        {
            var keywordNames = from kw in variantData.shaderKeywordSet.GetShaderKeywords() select kw.ToString();
            var variant = new ShaderVariantCollection.ShaderVariant(shader, snippet.passType, keywordNames.ToArray());

            if (!m_ShaderVariantCollection.Contains(variant))
            {
                dataToStrip.Add(variantData);
                Debug.Log($"Stripping shader variant: {variantData.shaderKeywordSet} from {shader.name}");
            }
            else
            {
                Debug.Log($"Keeping shader variant: {variantData.shaderKeywordSet} from {shader.name}");
            }
        }

        // Remove the compiler-data for the variants to strip:
        foreach (var variantData in dataToStrip)
        {
            variantCompilerDatas.Remove(variantData);
        }
    }
}