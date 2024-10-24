using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageTextChanger : MonoBehaviour
{
    public List<ImageTextData> imageTextDataList; // Список данных с картинками и текстом
    public Image targetImage; // Целевое изображение
    public Text targetText; // Целевой текст
    public InputField nameInputField; // Поле ввода имени
    public Text usernametext;
    private string playerName;

    private void Start()
    {
        LoadName();
        print(playerName);
        UpdateTextWithName(playerName);
        

        // Добавляем слушатель изменения текста в поле ввода
        nameInputField.onEndEdit.AddListener(OnNameInputEndEdit);
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeImageAndText());
        updateusername();
    }

    private void OnDisable()
    {
        StopCoroutine(ChangeImageAndText());
    }

    private void OnApplicationQuit()
    {
        SaveName();
    }

    private IEnumerator ChangeImageAndText()
    {
        int index = 0;
        while (true)
        {
            // Устанавливаем изображение и текст из текущего элемента списка
            targetImage.sprite = imageTextDataList[index].image;
            UpdateTargetText(imageTextDataList[index]);

            // Ждем 30 секунд
            yield return new WaitForSeconds(30f);

            // Переходим к следующему элементу списка (с циклическим возвратом к началу)
            index = (index + 1) % imageTextDataList.Count;
        }
    }

    private void UpdateTargetText(ImageTextData data)
    {
        if (LocalizatiomManager.language == "RU")
        {
            targetText.text = data.textRussian;
        }
        else
        {
            targetText.text = data.textEnglish;
        }
    }

    private void SaveName()
    {
        PlayerPrefs.SetString("Name", playerName);
    }

    private void LoadName()
    {
        playerName = PlayerPrefs.GetString("Name", "User");
    }

    public void updateusername()
    {
        if (LocalizatiomManager.language == "RU")
        {
            usernametext.text = "Привет " + playerName;
        }
        else
        {
            usernametext.text = "Hi " + playerName;
        }
    }

    private void UpdateTextWithName(string name)
    {
        if (name.Length <= 9)
        {
            playerName = name;

            if (LocalizatiomManager.language == "RU")
            {
                usernametext.text = "Привет " + playerName;
            }
            else
            {
                usernametext.text = "Hi " + playerName;
            }

            SaveName();
        }
        else
        {
            Debug.LogWarning("The nickname exceeds the maximum length of 9 characters.");
        }
    }

    private void OnNameInputEndEdit(string inputName)
    {
        UpdateTextWithName(inputName);
    }
}
