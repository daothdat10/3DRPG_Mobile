using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwappper: MonoBehaviour
{
    public Sprite enableSprite;
    public Sprite disableSprite;

    private bool m_swapped = true;

    private Image m_image;

    public void Awake()
    {
        m_image = GetComponent<Image>();
    }
    public void SwapSprite()
    {
        if (m_swapped)
        {
            m_swapped = false;
            m_image.sprite = disableSprite;
        }
        else
        {
            m_swapped = true;
            m_image.sprite = enableSprite;
        }
    }
}
