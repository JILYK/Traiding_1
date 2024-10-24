using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Game/Achievement")]
public class Achievement : ScriptableObject
{
    public Sprite normalSprite;
    public Sprite grayscaleSprite;
    public string titleEnglish; // Заголовок на английском
    public string titleRussian; // Заголовок на русском
    [TextArea]
    public string descriptionEnglish; // Описание на английском
    [TextArea]
    public string descriptionRussian; // Описание на русском
    public int targetValue; // Значение, по достижении которого достижение считается выполненным
    public int rewardAmount; // Награда в виде числа
}