using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public QuestionList questionList;
    public Text questionText;
    public Button[] answerButtons;
    public AnswerFeedback answerFeedback;
    public Text coinstext;
    public Text pointstext;

    public GameObject feedbackPanel;
    public Image feedbackImage;
    public Text feedbackText;
    public Button nextQuestionButton;
    public UIManager ui;
    
    private Question currentQuestion;
    public bool isCoroutineRunning;
    public bool correctanswere;
    public int plus10achivement;
    public AchievementsProgress achievementsProgress;
    

    private void Start()
    {
        nextQuestionButton.onClick.AddListener(NextQuestion); // Добавляем слушатель для кнопки
        LoadQuestion();
    }

    private void OnEnable()
    {
        coinstext.text = ui.coins.ToString();
        pointstext.text = "★ " + ui.points.ToString();
        LoadQuestion();
    }

    public void LoadQuestion()
    {
        currentQuestion = questionList.GetCurrentQuestion();

        if (currentQuestion == null)
        {
            Debug.LogWarning("No current question available!");
            return;
        }

        feedbackPanel.SetActive(false); // Скрываем панель обратной связи

        if (LocalizatiomManager.language == "ENG")
        {
            questionText.text = currentQuestion.questionTextEnglish;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answersEnglish.Length)
                {
                    answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answersEnglish[i];
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponent<Image>().color = Color.white; // Reset button color
                    answerButtons[i].interactable = true; // Enable button
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            questionText.text = currentQuestion.questionTextRussian;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < currentQuestion.answersRussian.Length)
                {
                    answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answersRussian[i];
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].GetComponent<Image>().color = Color.white; // Reset button color
                    answerButtons[i].interactable = true; // Enable button
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnAnswerSelected(int index)
    {
        if (index == currentQuestion.correctAnswerIndex)
        {
            Debug.Log("Correct!");
            DisplayFeedback(true);
            ui.points += 30;
            pointstext.text = "★ " + ui.points.ToString();
            int deff = UnityEngine.Random.Range(0, 3);
            print(deff);
            switch (deff)
            {
                case 0:
                    ui.UpdateProgress(6, 1);
                    print("Easy");
                    break;
                case 1:
                    ui.UpdateProgress(7, 1);
                    print("Normal");
                    break;
                case 2:
                    ui.UpdateProgress(8, 1);
                    print("Hard ");
                    break;
            }

            if (correctanswere == false)
            {
                ui.UpdateProgress(10, 1);
                plus10achivement += 1;
            }
            else
            {
                ui.UpdateProgress(10, plus10achivement - plus10achivement);
                plus10achivement = 0;
            }
            ui.UpdateProgress(9, 1);
            StartCoroutine(MyCoroutine());
        }
        else
        {
            Debug.Log("Incorrect.");
            DisplayFeedback(false);
        }

        if (isCoroutineRunning)
        {
            ui.UpdateProgress(11, 1);
        }
        else
        {
            StopCoroutine(MyCoroutine());
        }
    }


    private IEnumerator MyCoroutine()
    {
        isCoroutineRunning = true;
        if (achievementsProgress.achievements[11].isCompleted == true)
        {
            StopCoroutine(MyCoroutine());
        }
        yield return new WaitForSeconds(60f);
        isCoroutineRunning = false;
    }

    public void ShowCorrectAnswer()
    {
        if (ui.points >= 50)
        {
            correctanswere = true;
            ui.points -= 50;
            pointstext.text = "★ " + ui.points.ToString();
            ui.save();
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i == currentQuestion.correctAnswerIndex)
                {
                    answerButtons[i].GetComponent<Image>().color = Color.green; // Подсветить правильный ответ зеленым цветом
                }
                else
                {
                    answerButtons[i].GetComponent<Image>().color = Color.red; // Подсветить неправильные ответы красным цветом
                }
            }
        }
    }

    public void disablecurrectanserre()
    {
        correctanswere = false;
    }

    private void DisplayFeedback(bool isCorrect)
    {
        feedbackPanel.SetActive(true);
        if (isCorrect)
        {
            feedbackImage.sprite = answerFeedback.correctAnswerSprite;
            if (LocalizatiomManager.language == "RU")
            {
                feedbackText.text = "Верно!\nВы получаете \u260530";
            }
            else
            {
                feedbackText.text = "That's right!\nYou get \u2605 30";
            }
        }
        else
        {
            feedbackImage.sprite = answerFeedback.incorrectAnswerSprite;
            if (LocalizatiomManager.language == "RU")
            {
                feedbackText.text = "Неверно.\nПопробуйте снова";
            }
            else
            {
                feedbackText.text = "Wrong.\nTry again.";
            }
        }
    }

    public void NextQuestion()
    {
        questionList.NextQuestion();
        LoadQuestion();
    }
}
