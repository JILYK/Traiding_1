using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "Quiz/Question")]
public class Question : ScriptableObject
{
    public string questionTextEnglish;
    public string questionTextRussian;
    public string[] answersEnglish;
    public string[] answersRussian;
    public int correctAnswerIndex;
    public DifficultyLevel difficultyLevel;
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}
