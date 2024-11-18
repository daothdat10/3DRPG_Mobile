
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public string scene = "<Insert scene name>";
    public float duration = 1.0f;
    public Color color = Color.black;

    public void PerformTransition()
    {
        Transition.LoadLevel(scene,1f,Color.black);
        
    }
}
                                                                                                                                                                                                                                                                                    