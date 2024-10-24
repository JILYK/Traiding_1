using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelNameEnglish; // Имя уровня на английском языке
    public string levelNameRussian; // Имя уровня на русском языке
    public Sprite avatar;
    public float requiredCompletionPercentage; // Процент выполнения достижений, необходимый для достижения уровня
}