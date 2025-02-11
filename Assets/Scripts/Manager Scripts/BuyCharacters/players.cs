using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class players: ScriptableObject
{
    public List<buyPlayers> BuyPlayersList;
}

[System.Serializable]
public struct buyPlayers
{
    public GameObject player;
    public bool isUnlocked;
    public string playerName;
    public int coins;
   
}
