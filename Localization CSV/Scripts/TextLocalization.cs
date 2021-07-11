using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Переводит текст по значению ключа
/// </summary>
[RequireComponent(typeof(Text))]
public class TextLocalization : MonoBehaviour
{
    Text textField;
    public string key;

    void Start()
    {
        textField = GetComponent<Text>();
        string value = LocalizationMain.GetLocalisedValue(key);
        textField.text = value;
    }
}
