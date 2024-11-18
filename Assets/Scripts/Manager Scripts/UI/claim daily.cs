using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClaimDaily : MonoBehaviour
{
    [SerializeField] private Image focus;
    [SerializeField] private Image giftSpot;
    [SerializeField] private Sprite collectedSprite;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private TextMeshProUGUI titletext;
    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI timeLeft;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI currentDateText;
    [SerializeField] private GlobalTimeScript globalTime;

    private int nextDay;

    void Start()
    {
        nextDay = PlayerPrefs.GetInt("nextdays", 1);

        DateTime lastClaimTime;
        string lastTime = PlayerPrefs.GetString("LastClaimTime", "");
        if (!string.IsNullOrEmpty(lastTime))
        {
            lastClaimTime = DateTime.Parse(lastTime);
        }
        else
        {
            lastClaimTime = DateTime.MinValue;
        }

        DateTime startTime = globalTime.GetStartDateTime();
        currentTimeText.text = startTime.Hour + ":" + startTime.Minute;
        currentDateText.text = startTime.Day + ":" + startTime.Month + ":" + startTime.Year;

        if (DateTime.Today > lastClaimTime)
        {
            claimButton.interactable = true;
        }
        else
        {
            claimButton.interactable = false;
            timeLeft.text = GetTimeToNextClaim();
        }
    }

    private string GetTimeToNextClaim()
    {
        TimeSpan timeRemaining = DateTime.Today.AddDays(1) - globalTime.GetDateTimeNow();
        int hours = Mathf.FloorToInt((float)timeRemaining.TotalHours);
        int minutes = Mathf.FloorToInt((float)timeRemaining.TotalMinutes) % 60;

        return hours + " : " + minutes;
    }

    public void OnClaimButtonPressed()
    {
        DateTime claimTime = DateTime.Parse(PlayerPrefs.GetString("LastClaimTime", DateTime.MinValue.ToString()));
        if (DateTime.Today > claimTime)
        {
            nextDay++;
        }
        else
        {
            nextDay = 1;
        }

        PlayerPrefs.SetString("LastClaimTime", globalTime.GetRealDateTimeFromAPI().ToString());
        PlayerPrefs.SetInt("nextdays", nextDay);
        ClaimGift();
    }

    public void ClaimGift()
    {
        claimButton.interactable = false;
        checkMark.SetActive(true);
        giftSpot.sprite = collectedSprite;
        focus.enabled = false;
        titletext.text = "Daily Login Rewards<color=#f6e19c> 1 </color>/ 5";
        timeLeft.text = GetTimeToNextClaim();
    }
}
