using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayerPrefs : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("music_on"))
            PlayerPrefs.SetInt("music_on", 1);
        if (!PlayerPrefs.HasKey("sound_on"))
            PlayerPrefs.SetInt("sound_on", 1);
        
    }
}
