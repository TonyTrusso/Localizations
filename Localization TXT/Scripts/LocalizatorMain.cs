using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������� ����� ������ � ����������.
/// � ������, ������� ������ ���� �������� ������ ���� ��������� ��������� TranslateText.cs � ������� ����.
/// ������� ������������ �� ���� ��������������� ����� �������.(defaul: English).
/// </summary>
public class LocalizatorMain : MonoBehaviour
{
    private const char SEPARATCHAR = ':';
    private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

    private SystemLanguage language;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ReadTextFile();
    }

    private void Start()
    {
        GetTranslation();
    }

    private void ReadTextFile()
    {
        //_language = SystemLanguage.English; - �������� English ������, ����������� ��������� ������. 
        language = Application.systemLanguage;

        var file = Resources.Load<TextAsset>(language.ToString());
        // �������� �� ������� �������� �� ����� �������, ��� ���������� ���������� English.
        if (file == null)
        {
            file = Resources.Load<TextAsset>(SystemLanguage.English.ToString());
            language = SystemLanguage.English;
        }

        // ���������� ������� �� �������, � ������� ���� � �������� ��������� ������ SEPARATCHAR.
        foreach (var line in file.text.Split('\n'))
        {
            var property = line.Split(SEPARATCHAR);
            dictionary[property[0]] = property[1];
        }
    }

    private void GetTranslation()
    {
        // ���� ���� �����, ������� �������� �����������.
        var allText = Resources.FindObjectsOfTypeAll<TranslateText>();

        // ������������� ����� � ������������ � ������������ ������.
        foreach (var translateText in allText)
        {
            var text = translateText.GetComponent<Text>();
            text.text = Regex.Unescape(dictionary[translateText.TextKey]); 
            // Regex.Unescape - ��� ����������� ����������� ��������.
        }
    }
}
