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

            if (xpBar == null)
            {
                Debug.LogError("Không tìm thấy Slider. Đảm bảo XPSlider tồn tại và có component Slider!");
            }
            else
            {
                Debug.Log("Slider đã được gán tự động thành công.");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
