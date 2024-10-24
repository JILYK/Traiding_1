using UnityEngine;

[CreateAssetMenu(fileName = "NewAnswerFeedback", menuName = "Quiz/AnswerFeedback")]
public class AnswerFeedback : ScriptableObject
{
    public Sprite correctAnswerSprite;
    [TextArea]
    public string correctAnswerTextEnglish;
    [TextArea]
    public string correctAnswerTextRussian;
    
    public Sprite incorrectAnswerSprite;
    [TextArea]
    public string incorrectAnswerTextEnglish;
    [TextArea]
    public string incorrectAnswerTextRussian;
}