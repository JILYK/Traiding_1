using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Themeswitch : MonoBehaviour
{
    public List<Text> whitetext;
    public Camera cam;
    public List<Text> blacktext;
    public List<Image> whiteimage;
    public List<Image> blackImage;
    public int color;

    private void Start()
    {
        load();
        onbuttonclick();
    }

    private void OnEnable()
    {
        load();
    }

    public void save()
    {
        PlayerPrefs.SetInt("color", color);
    }
    public void load()
    {
        color = PlayerPrefs.GetInt("color");
    }
    public void onbuttonclick()
    {
        if (color == 1)
        {
            cam.backgroundColor = Color.white;
            for (int i = 0; i < whitetext.Count; i++)
            {
                whitetext[i].color = Color.black;
            }

            for (int i = 0; i <blacktext.Count; i++)
            {
                blacktext[i].color = Color.white;
            }

            for (int i = 0; i < whiteimage.Count; i++)
            {
                whiteimage[i].color = Color.black;
            }

            for (int i = 0; i < blackImage.Count; i++)
            {
                blackImage[i].color = Color.white;
            }

            save();
            color = 0;
        }
        else
        {
            cam.backgroundColor = Color.black;
                for (int i = 0; i < whitetext.Count; i++)
                {
                    whitetext[i].color = Color.white;
                }

                for (int i = 0; i <blacktext.Count; i++)
                {
                    blacktext[i].color = Color.black;
                }

                for (int i = 0; i < whiteimage.Count; i++)
                {
                    whiteimage[i].color = Color.white;
                }

                for (int i = 0; i < blackImage.Count; i++)
                {
                    blackImage[i].color = Color.black;
                }
                color = 1;
                save();
        }
    }
}
