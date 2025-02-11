using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerPlayer : MonoBehaviour
{
    private GameData _gameData;
    [SerializeField] private players[] _player;
    [SerializeField] private MenuCharacter[] _menuCharacter;
    private TextMeshProUGUI txtCoin;
    [SerializeField] private Button[] buttons;


    private void Awake()
    {
       _gameData = SystemSave.Load();
       txtCoin = GameObject.Find("coinsHierarchy").GetComponent<TextMeshProUGUI>();
      
    }

    private void Start()
    {
        
        txtCoin.text = _gameData.totalCoins.ToString();
    
        
    }

    private void Update()
    {
        txtCoin.text = _gameData.totalCoins.ToString();
    }

    public void buyCharacter(int index)
    {
        if (_gameData==null)
        {
            Debug.Log("Game Data is null");
        }

        for (int i = 0; i < _menuCharacter.Length; i++)
        {
            foreach (var item in _player)
            {
                if (_gameData.totalCoins > item.BuyPlayersList[index].coins)
                {
                    _gameData.totalCoins -= item.BuyPlayersList[index].coins;
                    var playerUnlock = item.BuyPlayersList[index];
                    playerUnlock.isUnlocked = true;
                    
                    item.BuyPlayersList[index] = playerUnlock;
                    Debug.Log("Bạn đã mở khóa nhân vật:");
                    for (int j = 0; j < buttons.Length; j++)
                    {
                        if (j == index)
                        {
                            buttons[j].gameObject.SetActive(true); 
                        }
                        else
                        {
                            buttons[j].gameObject.SetActive(false ); 
                        }
                    }
                    
                }
            }
            
        }
        txtCoin.text = _gameData.totalCoins.ToString();
        SystemSave.Save(_gameData);
        
    }

  
}
