using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Character", menuName = "Characters")]
public class MenuCharacter : ScriptableObject
{
    public string Player;
    
    public List<players> characters;
}
