using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Перевод всего текста в приложении.
/// К тексту, который должен быть переведён должен быть прикреплён компонент TranslateText.cs и указаан ключ.
/// Перевод производится на язык соответствующий языку системы.(defaul: English).
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
        //_language = SystemLanguage.English; - проверка English версии, закоментить следующую строку. 
        language = Application.systemLanguage;

        var file = Resources.Load<TextAsset>(language.ToString());
        // проверка на наличие перевода на языке системы, при отсутствии установить English.
        if (file == null)
        {
            file = Resources.Load<TextAsset>(SystemLanguage.English.ToString());
            language = SystemLanguage.English;
        }

        // заполнение словаря по строкам, в которых ключ и значения разделены знаком SEPARATCHAR.
        foreach (var line in file.text.Split('\n'))
        {
            var property = line.Split(SEPARATCHAR);
            dictionary[property[0]] = property[1];
        }
    }

    private void GetTranslation()
    {
        // ищем весь текст, который подлежит локализации.
        var allText = Resources.FindObjectsOfTypeAll<TranslateText>();

        // устанавливаем текст в соответствии с установленым ключом.
        foreach (var translateText in allText)
        {
            var text = translateText.GetComponent<Text>();
            text.text = Regex.Unescape(dictionary[translateText.TextKey]); 
            // Regex.Unescape - для корректного отображения символов.
        }
    }
}
