using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
     private Slider xpBar;
    

    public void xpLevelUp(float currentXP, float maxXP)
    {
        xpBar.value = currentXP/maxXP;
    }
    void Start()
    {
        if (xpBar == null)
        {
            xpBar = GameObject.Find("XPSlider")?.GetComponent<Slider>();

           
        }
    }


    
}
