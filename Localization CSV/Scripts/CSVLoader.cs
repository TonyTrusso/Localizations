using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// Считывает таблицу localization.csv в папке Resources.
/// Данные в таблице должны быть написаны в формате: "key","перевод на 1м языке","перевод 2м языке",...
/// </summary>
public class CSVLoader : MonoBehaviour
{
    private TextAsset csvFile;
    private char lineSeparator = '\n';
    private char surround = '"';
    private string[] fieldSeparator = { "\",\"" };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeId)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] lines = csvFile.text.Split(lineSeparator);

        int attributeIndex = -1;

        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);

        for (int i=0; i<headers.Length; i++)
        {
            if (headers[i].Contains(attributeId))
            {
                attributeIndex = i;
                break;
            }
        }

        Regex CVSParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CVSParser.Split(line);

            for (int j = 0; j < fields.Length; j++)
            {
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
            }

            if (fields.Length > attributeIndex)
            {
                var key = fields[0];

                if (dictionary.ContainsKey(key))
                {
                    continue;
                }

                var value = fields[attributeIndex];

                dictionary.Add(key, value);
            }
        }
        return dictionary;
    }
}