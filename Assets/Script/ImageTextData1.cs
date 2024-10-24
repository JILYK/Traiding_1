using UnityEngine;

[CreateAssetMenu(fileName = "NewImageTextData", menuName = "ImageTextData")]
public class ImageTextData : ScriptableObject
{
    public Sprite image;
    public string textEnglish;
    public string textRussian;
}