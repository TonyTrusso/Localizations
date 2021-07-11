using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ��� �������� ���������.
/// </summary>
[RequireComponent(typeof(CSVLoader))]
public class LocalizationMain : MonoBehaviour
{
    public enum Language
    {
        English,
        Russian
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedRU;

    void Awake()
    {
        CSVLoader csvLoader = GetComponent<CSVLoader>();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedRU = csvLoader.GetDictionaryValues("ru");
    }

    
    /// <summary>
    /// ������� �������� �������� �� �����.
    /// </summary>
    /// <param name="key">���� ��� ��������</param>
    /// <returns></returns>
    public static string GetLocalisedValue(string key)
    {
        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;

            case Language.Russian:
                localisedRU.TryGetValue(key, out value);
                break;

            default:
                value = "No translation found =(";
                break;
        }
        return value;
    }
}
