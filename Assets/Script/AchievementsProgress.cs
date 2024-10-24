using UnityEngine;

[CreateAssetMenu(fileName = "AchievementsProgress", menuName = "Game/AchievementsProgress")]
public class AchievementsProgress : ScriptableObject
{
    public AchievementProgress[] achievements;

    [System.Serializable]
    public class AchievementProgress
    {
        public Achievement achievement; // Ссылка на само достижение
        public int currentValue; // Текущее значение прогресса
        public bool isCompleted; // Флаг, указывающий на выполнение достижения
    }

    public void Initialize()
    {
        foreach (var achievementProgress in achievements)
        {
            achievementProgress.currentValue = 0;
            achievementProgress.isCompleted = false;
        }
    }
}