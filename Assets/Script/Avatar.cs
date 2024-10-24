using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Avatar : MonoBehaviour
{
    public Image avatar1;
    public Image avatar2;

    private void Update()
    {
        avatar2.sprite = avatar1.sprite;
    }
}
