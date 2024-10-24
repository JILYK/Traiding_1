using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TrandeGame : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Button sideButton;
    public Button nextButton;
    public UIManager ui;
    public Text coinstext;
    public Text pointtext;

    public List<Sprite> initialUpImages;
    public List<Sprite> finalUpImages;
    public List<Sprite> initialDownImages;
    public List<Sprite> finalDownImages;
    public List<Sprite> initialSideImages;
    public List<Sprite> finalSideImages;

    public Image graphImageView;

    public GameObject resultPanel;
    public Text resultText;
    public Image resultImage;

    public string winTextEnglish;
    public string winTextRussian;
    public string loseTextEnglish;
    public string loseTextRussian;

    public Sprite winSprite;
    public Sprite loseSprite;

    public PlayerProgress playerProgress;
    public AchievementsInitializer achievementsInitializer; // Ссылка на AchievementsInitializer для обновления прогресса

    private int currentImageIndex;
    private string correctTrend;
    private int consecutiveCorrectAnswers;
    private int totalCorrectAnswers;
    private int correctUpTrends;
    private int correctDownTrends;
    private int correctSideTrends;

    private void Start()
    {
        if (achievementsInitializer == null)
        {
            Debug.LogError("AchievementsInitializer is not assigned.");
            return;
        }

        InitializeGame();
        upButton.onClick.AddListener(() => OnButtonClicked("Up"));
        downButton.onClick.AddListener(() => OnButtonClicked("Down"));
        sideButton.onClick.AddListener(() => OnButtonClicked("Side"));
        nextButton.onClick.AddListener(NextGraph);
        achievementsInitializer.InitializeAchievements();
    }

    private void InitializeGame()
    {
        currentImageIndex = Random.Range(0, initialUpImages.Count);
        correctTrend = DetermineCorrectTrend();
        graphImageView.sprite = GetInitialImage(correctTrend, currentImageIndex);
        resultPanel.SetActive(false);
    }

    private string DetermineCorrectTrend()
    {
        int trend = Random.Range(0, 3);
        switch (trend)
        {
            case 0: return "Up";
            case 1: return "Down";
            case 2: return "Side";
            default: return "Side";
        }
    }
    

    public void updatetext()
    {
        coinstext.text = ui.coins.ToString();
        pointtext.text = "★ " + ui.points.ToString();
        
    }

    private void OnEnable()
    {
        updatetext();
    }

    private Sprite GetInitialImage(string trend, int index)
    {
        switch (trend)
        {
            case "Up": return initialUpImages[index];
            case "Down": return initialDownImages[index];
            case "Side": return initialSideImages[index];
            default: return null;
        }
    }

    private Sprite GetFinalImage(string trend, int index)
    {
        switch (trend)
        {
            case "Up": return finalUpImages[index];
            case "Down": return finalDownImages[index];
            case "Side": return finalSideImages[index];
            default: return null;
        }
    }

    private void OnButtonClicked(string guessedTrend)
    {
        graphImageView.sprite = GetFinalImage(correctTrend, currentImageIndex);

        bool isCorrect = guessedTrend == correctTrend;
        UpdateResult(isCorrect);
        UpdateAchievements(isCorrect, guessedTrend);

        resultPanel.SetActive(true);
    }


    private void UpdateResult(bool isCorrect)
    {
        if (isCorrect)
        {
            resultImage.sprite = winSprite;
            if (LocalizatiomManager.language == "RU")
            {
                resultText.text = "Верно!\nВы получаете \u2605 40";
            }
            else
            {
                resultText.text = "That's right!\nYou get \u2605 40";
            }
        }
        else
        {
            resultImage.sprite = loseSprite;
            if (LocalizatiomManager.language == "RU")
            {
                resultText.text = "Неверно.\nПопробуйте ещё.";
            }
            else
            {
                resultText.text = "Wrong.\nTry again.";
            }
        }
    }

    private void UpdateAchievements(bool isCorrect, string guessedTrend)
    {
        if (isCorrect)
        {
            totalCorrectAnswers++;
            consecutiveCorrectAnswers++;
            switch (guessedTrend)
            {
                case "Up":
                    ui.points += 40;
                    pointtext.text = "★ " + ui.points.ToString();
                    print(ui.points);
                    ui.save();
                    correctUpTrends++;
                    ui.UpdateProgress(0, 1); // Правильно определите 50 восходящих трендов
                    break;
                case "Down":
                    ui.points += 40;
                    pointtext.text = "★ " +  ui.points.ToString();
                    print(ui.points);
                    ui.save();
                    correctDownTrends++;
                    ui.UpdateProgress(1, 1); // Правильно определите 50 нисходящих трендов
                    break;
                case "Side":
                    ui.points += 40;
                    pointtext.text = "★ " + ui.points.ToString();
                    print(ui.points);
                    ui.save();
                    correctSideTrends++;
                    ui.UpdateProgress(2, 1); // Правильно определите 50 боковых трендов
                    break;
            }

            ui.UpdateProgress(3, 1); // Правильно определите 200 трендов любого типа
            ui.UpdateProgress(4, consecutiveCorrectAnswers); // Достигните 30 правильных ответов подряд
        }
        else
        {
            consecutiveCorrectAnswers = 0;
        }

        ui.UpdateProgress(5, consecutiveCorrectAnswers); // Достигните 100% точности на 100 графиках подряд
        
    }

    private void NextGraph()
    {
        InitializeGame();
    }
}
