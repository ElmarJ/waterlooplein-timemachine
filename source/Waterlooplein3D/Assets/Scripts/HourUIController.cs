using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HourUIController : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHour(float hour)
    {
        textComponent.text = hour.ToString("00") + ":00";
    }
}
