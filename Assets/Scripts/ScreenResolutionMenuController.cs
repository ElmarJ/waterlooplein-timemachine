using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutionMenuController : MonoBehaviour
{
    private Dropdown dropdown;

    void Start()
    {
        this.dropdown = gameObject.GetComponent<Dropdown>();
        
        // Populate dropdown:
        dropdown.options.Clear();
        foreach (var resolution in Screen.resolutions)
        {
            dropdown.options.Add(new Dropdown.OptionData($"{resolution.width} x {resolution.height}"));
        }

        // Find current resolution index.
        // Note that Array.IndexOf with Screen.currentResolution does not work 
        // because Resolution structs don't match (refresh rate data is missing).
        int currentResolutionIndex = -1;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            if(resolution.height == Screen.height && resolution.width == Screen.width)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        // Set current value:
        this.dropdown.SetValueWithoutNotify(currentResolutionIndex);
        this.dropdown.RefreshShownValue();
    }

    public void OnResolutionChange()
    {
        var currentResolution = Screen.resolutions[dropdown.value];
        Screen.SetResolution(currentResolution.width, currentResolution.height, true);
    }
}
