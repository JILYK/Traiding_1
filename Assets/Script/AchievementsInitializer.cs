using UnityEngine;
using UnityEngine.UI;

public class AchievementsInitializer : MonoBehaviour
{
    public AchievementsProgress achievementsProgress;
    public GameObject[] achievementSlots; // Слоты в GridLayoutGroup

    private void OnEnable()
    {
        InitializeAchievements();
    }

    public void InitializeAchievements()
    {
        for (int i = 0; i < achievementsProgress.achievements.Length && i < achievementSlots.Length; i++)
        {
            var achievementProgress = achievementsProgress.achievements[i];
            var achievementSlot = achievementSlots[i];

            // Проверяем наличие Image компонентов
            var images = achievementSlot.GetComponentsInChildren<Image>();

            if (images.Length < 3)
            {
                Debug.LogError($"Slot {i} does not have enough Image components. Expected 3, found {images.Length}.");
                continue;
            }

            // Получаем Image компоненты
            var iconImage = images[0]; // Первый Image компонент для иконки достижения
            var progressBarImage = images[2]; // Второй Image компонент для прогрессбара

            if (iconImage == null || progressBarImage == null)
            {
                Debug.LogError($"Slot {i} is missing necessary Image components.");
                continue;
            }

            Debug.Log($"Updating slot {i} with achievement {achievementProgress.achievement.titleEnglish}");

            // Установка черно-белой картинки достижения
            iconImage.sprite = achievementProgress.achievement.grayscaleSprite;

            // Установка прогресса
            float fillAmount = 1.0f - ((float)achievementProgress.currentValue / achievementProgress.achievement.targetValue);
            progressBarImage.fillAmount = fillAmount;
            Debug.Log($"Initial fill amount for achievement {i}: {progressBarImage.fillAmount} (current value: {achievementProgress.currentValue}, target value: {achievementProgress.achievement.targetValue})");
        }
    }

    public void UpdateAchievementProgress(int achievementIndex)
    {
        if (achievementIndex < 0 || achievementIndex >= achievementsProgress.achievements.Length) return;

        var achievementProgress = achievementsProgress.achievements[achievementIndex];
        var achievementSlot = achievementSlots[achievementIndex];

        var images = achievementSlot.GetComponentsInChildren<Image>();
        if (images.Length < 3) return;

        var progressBarImage = images[2]; // Второй Image компонент для прогрессбара
        float fillAmount = 1.0f - ((float)achievementProgress.currentValue / achievementProgress.achievement.targetValue);
        progressBarImage.fillAmount = fillAmount;
        Debug.Log($"Updated fill amount for achievement {achievementIndex}: {progressBarImage.fillAmount} (current value: {achievementProgress.currentValue}, target value: {achievementProgress.achievement.targetValue})");
    }
}