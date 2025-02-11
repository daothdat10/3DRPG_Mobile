using System;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


public class CollectingCoin : MonoBehaviour
{
    [SerializeField] private GameObject PileOfCoinParent;
    
    [SerializeField] private AudioSource CointSound;
    
    [SerializeField] private TextMeshProUGUI Counter;

    [SerializeField] private Vector3[] InitialPos;

    [SerializeField] private Quaternion[] InitialRotation;

    [SerializeField] private int CoinNo;
    
    private GameData _gameData;

    private void Awake()
    {
        
    }

    private void Start()
    {
        _gameData = SystemSave.Load();
        Counter.text = _gameData.totalCoins.ToString();
        InitialPos = new Vector3[CoinNo];
        InitialRotation = new Quaternion[CoinNo];

        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            InitialPos[i] = PileOfCoinParent.transform.GetChild(i).position;
            InitialRotation[i] = PileOfCoinParent.transform.GetChild(i).rotation;
        }
    }


    private void Reset()
    {
        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).position = InitialPos[i];


            PileOfCoinParent.transform.GetChild(i).rotation = InitialRotation[i];
        }
    }

    public void RewardPileOfCoin(int noCoin)
    {
        Reset();

        var delay = 0f;

        PileOfCoinParent.SetActive(true);

        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay + 0.5f).SetEase(Ease.OutBack);


            PileOfCoinParent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(25.4f, 492f), 3f).SetDelay(delay + 1.8f).SetEase(Ease.InBack);


            PileOfCoinParent.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash).OnComplete(CountCoinsByComplete);


            PileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.8f).SetDelay(delay + 0.8f).SetEase(Ease.OutBack);
            CointSound.Play();

            delay += 0.1f;
        }
        


    }

    void CountCoinsByComplete()
    {
        _gameData.totalCoins += 1;
        SystemSave.Save(_gameData);
        
        Counter.text = PlayerPrefs.GetInt("CountCoin").ToString();
    }

    IEnumerator CountCoins(int coinNo)
    {
        yield return new WaitForSecondsRealtime(1f);

        var timer = 0f;

        for (int i = 0; i < coinNo; i++)
        {
            _gameData.totalCoins += 1;

            Counter.text = _gameData.totalCoins.ToString();

            timer += 1;
            
            yield return new WaitForSecondsRealtime(timer);
        }
        
    }
}