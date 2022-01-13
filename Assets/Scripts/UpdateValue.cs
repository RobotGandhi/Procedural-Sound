using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateValue : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    void Start()
    {
        valueText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(float newValue) {
        valueText.text = newValue.ToString();
    }
}
