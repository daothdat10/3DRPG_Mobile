using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwapper : MonoBehaviour
{
    public Color enabledColor;
    public Color disableColor;

    private bool m_swapped = true;

    private Image m_image;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    public void SwapColor()
    {
        if(m_swapped)
        {
            m_swapped = false;
            m_image.color = disableColor;
        }
        else
        {
            m_swapped = true;
            m_image.color = enabledColor;
        }
    }
}