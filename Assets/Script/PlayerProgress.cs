using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgress", menuName = "Game/PlayerProgress")]
public class PlayerProgress : ScriptableObject
{
    public AchievementsProgress achievementsProgress; // Ссылка на AchievementsProgress
    public LevelData[] levels;
    public List<int> progressInt;

    [HideInInspector]
    public int currentLevelIndex;
    [HideInInspector]
    public int completedAchievements;
    [HideInInspector]
    public int totalRewards;

    private void OnEnable()
    {
        if (achievementsProgress != null)
        {
            LoadProgress();
            UpdateCompletedAchievements();
        }
        else
        {
            Debug.LogError("AchievementsProgress is not assigned.");
        }
    }

    public void CheckLevelUp()
    {
        UpdateCompletedAchievements();
        float completionPercentage = (float)completedAchievements / achievementsProgress.achievements.Length;
        completionPercentage *= 100;
        Debug.Log(completionPercentage + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Debug.Log(completedAchievements + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!~!~!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~!!!!!!!!!!!!!!!");
        Debug.Log(achievementsProgress.achievements.Length + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! ");

        for (int i = levels.Length - 1; i >= 0; i--)
        {
            if (completionPercentage >= levels[i].requiredCompletionPercentage)
            {
                currentLevelIndex = i;
                break;
            }
        }

        SaveProgress();
    }

    private void OnDisable()
    {
        SaveProgress();
    }
    public Sprite GetCurrentLevelAvatar()
    {
        return levels[currentLevelIndex].avatar;
    }

    public string GetCurrentLevelName()
    {
        return LocalizatiomManager.language == "ENG" ? levels[currentLevelIndex].levelNameEnglish : levels[currentLevelIndex].levelNameRussian;
    }

    public float GetLevelProgress()
    {
        if (currentLevelIndex >= levels.Length - 1) return 1.0f;
        float nextLevelCompletion = levels[currentLevelIndex + 1].requiredCompletionPercentage;
        float currentLevelCompletion = levels[currentLevelIndex].requiredCompletionPercentage;
        return (completedAchievements / (float)achievementsProgress.achievements.Length - currentLevelCompletion) / (nextLevelCompletion - currentLevelCompletion);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
        PlayerPrefs.SetInt("CompletedAchievements", completedAchievements);
        PlayerPrefs.SetInt("TotalRewards", totalRewards);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
        completedAchievements = PlayerPrefs.GetInt("CompletedAchievements", 0);
        totalRewards = PlayerPrefs.GetInt("TotalRewards", 0);
        CheckLevelUp();
    }

    public void UpdateCompletedAchievements()
    {
        completedAchievements = 0;
        foreach (var achievement in achievementsProgress.achievements)
        {
            if (achievement.isCompleted)
            {
                completedAchievements++;
            }
        }
        Debug.Log(completedAchievements + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
}
