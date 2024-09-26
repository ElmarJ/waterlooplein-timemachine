using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ShadowMapUpdater : MonoBehaviour
{
    private HDAdditionalLightData[] lights;

    private void Start()
    {
        this.lights = Resources.FindObjectsOfTypeAll<HDAdditionalLightData>();
    }

    public void UpdateStreetLightShadowMaps()
    {
        foreach (var light in lights)
        {
            light.RequestShadowMapRendering();
        }
    }
}
