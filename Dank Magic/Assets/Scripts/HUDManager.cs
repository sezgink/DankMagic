using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public GameObject spiritBar;
    public Image[] spiritImages;
    public Image[] skillImages;
    public Slider healthBar;

    public CharacterControl judyControl;


    public void FireUsed() {
        skillImages[0].color = Color.gray;
    }
    public void FireRefresh()
    {
        skillImages[0].color = Color.white;
    }
    public void ActivateSpiritBar()
    {
        print("ActivateSpiritBar");
        spiritBar.SetActive(true);
        for (int i = 0; i < skillImages.Length; i++) {
            skillImages[i].color = Color.white;
        }
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
        if(healthBar!=null)
            healthBar.value = judyControl.health;
    }

    public void HighlightImage()
    {
        
    }
}
