using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pau_con_res : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Button Continue;
    [SerializeField] Button Pau;
    [SerializeField] Button home;
    [SerializeField] Button adsButton;
    private GameObject player;

    private int playerCoins = 0;

    public TextMeshProUGUI coinstxt;

    public GameData gameData;

    public void Awake()
    {
        gameData = SystemSave.Load();
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Time.timeScale = 0;
            gameData.totalCoins += playerCoins;

            SystemSave.Save(gameData);
            adsButton.gameObject.SetActive(true);
            background.SetActive(true);
            home.gameObject.SetActive(true);
        }
        coinstxt.text = playerCoins.ToString() + "x";
    }

    public void Home(string Lobby)
    {
        SceneManager.LoadScene(Lobby);
        Time.timeScale = 1f;  
    }

    public void tinue()
    {
        Time.timeScale = 1.0f;
    }
    public void pau()
    {
        Time.timeScale = 0;
    }
    



    public void CoinColected()
    {
        playerCoins++;
    }
    public void ads() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        Time.timeScale = 1f;
    }

}
