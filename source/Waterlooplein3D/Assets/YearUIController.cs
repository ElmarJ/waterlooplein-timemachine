using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class YearUIController : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateYear(float year)
    {
        textComponent.text = year.ToString("0000");
    }
}
