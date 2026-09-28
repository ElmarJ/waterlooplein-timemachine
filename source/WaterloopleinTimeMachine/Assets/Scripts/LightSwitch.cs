using System.Collections.Generic;
using GeoJsonCityBuilder.Components;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public AutomaticSunPosition sunPosition;

    public void UpdateLights()
    {
        this.SetLights(!sunPosition.sunIsUp);
    }

    private void SetLights(bool on)
    {
        var lights = new List<Light>();
        this.gameObject.GetComponentsInChildren(lights);

        foreach (var light in lights)
        {
            light.enabled = on;
        }
    }

}
