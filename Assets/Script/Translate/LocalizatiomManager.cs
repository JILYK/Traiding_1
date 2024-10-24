using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocalizatiomManager : MonoBehaviour
{
    public Text[] textObjectsToTranslate; // Массив текстовых объектов для локализации
    public static string language = "";
    public Text englishText;
    public Text russianText;

    private void OnEnable()
    {
        switch (language)
        {
            case "RU":
                language = "RU";
                break;
            case "ENG":
                language = "ENG";
                break;
            default:
                language = "ENG";
                break;
        }
        loadlanguage();
        updatelang();
    }

    private void OnApplicationQuit()
    {
        savelanguage();
    }

    public void loadlanguage()
    {
        PlayerPrefs.GetString("Language", language);
    }
    public void savelanguage()
    {
        PlayerPrefs.SetString("Language", language);
    }

    public void switchlanguage()
    {
        print(language);
        if (language == "RU")
        {
            language = "ENG";
            print(language);
        }
        else
        {
            language = "RU";
            print(language);
        }
        updatelang();
    }

    public void updatelang()
    {
        if (language == "ENG")
        {
            englishText.color = Color.black;
            russianText.color = Color.gray;
        }
        else
        {
            russianText.color = Color.black;
            englishText.color = Color.gray;
        }
        // Получаем данные для локализации из статического конфига
        Dictionary<string, string> localizationData = LocalizationConfig.GetLocalizationData(language);

        // Проходимся по всем текстовым объектам и устанавливаем им локализованный текст
        foreach (Text textObject in textObjectsToTranslate)
        {
            if (localizationData.ContainsKey(textObject.name))
            {
                textObject.text = localizationData[textObject.name];
            }
        }
    }
    
}