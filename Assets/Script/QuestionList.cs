using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionList", menuName = "Quiz/QuestionList")]
public class QuestionList : ScriptableObject
{
    public Question[] questions;
    private int currentQuestionIndex = 0;

    public Question GetCurrentQuestion()
    {
        if (questions.Length == 0)
            return null;
        return questions[currentQuestionIndex];
    }

    public void NextQuestion()
    {
        if (questions.Length == 0)
            return;
        currentQuestionIndex = (currentQuestionIndex + 1) % questions.Length;
    }

    public void ResetQuestions()
    {
        currentQuestionIndex = 0;
    }
}