﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject spiritBar;
    public Image[] spiritImages;
    public Slider healthBar;

    public CharacterControl judyControl;

    public void ActivateSpiritBar()
    {
        spiritBar.SetActive(true);
    }

    public void UpdateSpiritBar()
    {
        for (int i = 0; i < 10; i++)
        {
             if (judyControl.spirits > i) 
                spiritImages[i].color = Color.white;
            else
                spiritImages[i].color = Color.black;
        }
    }

    public void UpdateHealth()
    {
        healthBar.value = judyControl.health;
    }

    public void HighlightImage()
    {
        
    }
}
