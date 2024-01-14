using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQualityMenuController : MonoBehaviour
{
    private Dropdown dropdown;
    void Start()
    {
        this.dropdown = gameObject.GetComponent<Dropdown>();
        
        // Populate dropdown:
        var names = QualitySettings.names;

        dropdown.options.Clear();
        foreach (var name in names)
        {
            dropdown.options.Add(new Dropdown.OptionData(name));
        }

        // Set current value:
        this.dropdown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());
        this.dropdown.RefreshShownValue();
    }

    public void OnQualityChange()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }
}
