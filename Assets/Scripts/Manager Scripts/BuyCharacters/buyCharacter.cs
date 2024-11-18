using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class buyCharacter : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private int[] charPrices;
    
    [SerializeField] Button choice;
    
    [SerializeField] private Button Buy;
    
    
    private TextMeshProUGUI coinstext;
    private int characters;

    

    private void Awake()
    {
        

        UpdateUI();
        gameData = SystemSave.Load();
        

    }
    private void Start()
    {
        
        coinstext = GameObject.Find("coinsHierarchy").GetComponent<TextMeshProUGUI>();
        characters = gameData.player;
        

    }

   

    private void Update()
    {

        if (Buy.gameObject.activeInHierarchy)
        {
            Buy.interactable = (gameData.totalCoins > charPrices[characters]);
        }
        
    }
    
    
   

    public void BuyCharacter(int index)
    {

        
        if (gameData.totalCoins >= charPrices[index])
        {
            gameData.totalCoins -= charPrices[index];

            gameData.charUnlocked[index] = true;

            characters = index;
            coinstext.text = gameData.totalCoins.ToString() ;
            SystemSave.Save(gameData);
            UpdateUI(); 
            Debug.Log("Mua Nhan Vat Thanh Cong " + index);
            
        }
        else
        {
           
            Debug.Log("Khong Du Tien.");
            
        }
       
    }
    private void UpdateUI()
    {

        if (gameData.charUnlocked[characters])
        {
            choice.gameObject.SetActive(true);
            Buy.gameObject.SetActive(false);

        }
        else
        {
            choice.gameObject.SetActive(false);
            Buy.gameObject.SetActive(true);

            coinstext.text = charPrices[characters] + "$";
        }
    }


}
