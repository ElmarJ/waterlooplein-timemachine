using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MonthUIController : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateMonth(float month)
    {
        textComponent.text = monthNames[(int) month - 1];
    }
}
