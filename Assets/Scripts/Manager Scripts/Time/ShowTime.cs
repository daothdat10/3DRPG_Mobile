using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTime : MonoBehaviour
{
    public TextMeshProUGUI time;
    public TextMeshProUGUI date;
    public GlobalTimeScript globalTime;

    public void Awake()
    {
        DateTime startTime = globalTime.GetStartDateTime();
        time.text = startTime.Hour + ":" + startTime.Minute ;
        date.text =startTime.Day+":"+ startTime.Month + ":" + startTime.Year;
    }
}
