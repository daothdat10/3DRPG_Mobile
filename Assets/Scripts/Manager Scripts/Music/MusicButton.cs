using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    private SpriteSwappper m_spriteSwappper;
    private bool m_on;

    private void Awake()
    {
        m_spriteSwappper = GetComponent<SpriteSwappper>();
        m_on = PlayerPrefs.GetInt("music_on") == 1;
        if(!m_on)
            m_spriteSwappper.SwapSprite();
    }

    public void Toggle()
    {
        m_on = true;
        var backGroundMusic = GameObject.Find("BacKGroundMusic").GetComponent<AudioSource>();
        backGroundMusic.volume = m_on ? 1 : 0;
        PlayerPrefs.SetInt("music_on", m_on ? 1 : 0);
    }
    public void ToggleSprite()
    {
        m_on = true;
        m_spriteSwappper.SwapSprite();
    }
}
