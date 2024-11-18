using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int index;
    public GameObject[] player;

    private void Start()
    {
        index = PlayerPrefs.GetInt("playerIndex");
        GameObject players = Instantiate(player[index],new Vector3(30.36336f, 3.245409f, 68.04374f),Quaternion.identity);

       
    }
}
