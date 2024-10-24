using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public PlayerProgress playerProgress;
    public int points;
    public int coins;
    public Text coinstext;
    public Text pointstext;
    public int StartActive = 1;
    public GameObject StartMenu;
    public GameObject MainMenu;

    public Image avatarImage;
    public Image levelProgressBar;
    public List<Image> achievementProgressBars; // Прогрессбары для каждого достижения
    public List<Image> achievementIcons; // Иконки для каждого достижения

    public Text achievementDescription;
    public Text levelNameText; // Текст для имени уровня

    public GameObject achievementDetailsPanel;
    public Text achievementTitleText;
    public Text achievementDetailsText;
    public Image achievementDetailsImage;
    public List<float> progressInt;
    public AchievementsProgress achievementsProgress;

    private void Start()
    {
        if (StartActive == 1)
        {
            StartMenu.SetActive(true);
            MainMenu.SetActive(false);
            StartActive = 0;
        }
        else
        {
            StartMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
        InitializeProgressInt();
        load();
        InitializeIcons();
        UpdateUI();
        Debug.Log("Coins: " + coins);
        Debug.Log("Points: " + points);
    }

    private void OnEnable()
    {
        InitializeProgressInt();
        load();
        InitializeIcons();
        UpdateUI();
    }

    private void OnApplicationQuit()
    {
        save();
    }

    private void InitializeProgressInt()
    {
        if (progressInt == null || progressInt.Count != achievementsProgress.achievements.Length)
        {
            progressInt = new List<float>(new float[achievementsProgress.achievements.Length]);
        }
    }

    private void InitializeIcons()
    {
        for (int i = 0; i < playerProgress.achievementsProgress.achievements.Length; i++)
        {
            if (achievementIcons[i] != null)
            {
                achievementIcons[i].sprite = playerProgress.achievementsProgress.achievements[i].achievement.grayscaleSprite;
            }
        }
    }

    public void UpdateProgress(int achievementIndex, int progressIncrement)
    {
        if (achievementsProgress.achievements[achievementIndex].achievement.targetValue > progressInt[achievementIndex])
        {
            progressInt[achievementIndex] += progressIncrement;
            Debug.Log(progressInt);
        }
        else if (achievementsProgress.achievements[achievementIndex].isCompleted == false)
        {
            coins += achievementsProgress.achievements[achievementIndex].achievement.rewardAmount;
            achievementsProgress.achievements[achievementIndex].isCompleted = true;
            Debug.Log("Achievement completed");
        }
    }

    public void UpdateUI()
    {
        playerProgress.LoadProgress();
        avatarImage.sprite = playerProgress.GetCurrentLevelAvatar();
        levelProgressBar.fillAmount = playerProgress.GetLevelProgress();
        Debug.Log("Level progress: " + levelProgressBar.fillAmount);
        levelNameText.text = playerProgress.GetCurrentLevelName();
        pointstext.text = "★ " + points.ToString();
        coinstext.text = coins.ToString();

        for (int i = 0; i < playerProgress.achievementsProgress.achievements.Length; i++)
        {
            var achievementProgress = playerProgress.achievementsProgress.achievements[i];
            var achievement = achievementProgress.achievement;

            if (achievementProgress.isCompleted)
            {
                achievementIcons[i].sprite = achievement.normalSprite;
            }
            else
            {
                achievementIcons[i].sprite = achievement.grayscaleSprite;
            }
            Debug.Log("UI Progress: " + progressInt[i]);
            achievementProgressBars[i].fillAmount = (float)progressInt[i] / achievement.targetValue;
        }
    }

    public void OnAchievementClicked(int index)
    {
        var achievementProgress = playerProgress.achievementsProgress.achievements[index];
        var achievement = achievementProgress.achievement;

        achievementDetailsPanel.SetActive(true);
        achievementDetailsImage.sprite = achievement.normalSprite;

        if (LocalizatiomManager.language == "ENG")
        {
            achievementTitleText.text = achievement.titleEnglish;
            achievementDetailsText.text = achievement.descriptionEnglish;
        }
        else
        {
            achievementTitleText.text = achievement.titleRussian;
            achievementDetailsText.text = achievement.descriptionRussian;
        }
    }

    private void OnDisable()
    {
        save();
    }

    public void save()
    {
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("StartActive", StartActive);
        for (int i = 0; i < achievementsProgress.achievements.Length; i++)
        {
            Debug.Log("Saving progress: " + progressInt[i]);
            PlayerPrefs.SetFloat("Progress" + i, progressInt[i]);
        }
        PlayerPrefs.Save();
    }

    public void load()
    {
        StartActive = PlayerPrefs.GetInt("StartActive", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
        points = PlayerPrefs.GetInt("points", 0);
        for (int i = 0; i < achievementsProgress.achievements.Length; i++)
        {
            progressInt[i] = PlayerPrefs.GetFloat("Progress" + i, 0);
            Debug.Log("Loaded progress: " + progressInt[i]);
        }
    }
}
