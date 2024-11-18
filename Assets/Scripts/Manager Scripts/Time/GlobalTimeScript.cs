using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Text.RegularExpressions;


public class GlobalTimeScript : MonoBehaviour
{
    const string API_URL = "http://localhost:8080/unityback/GetDate.php";
    DateTime currentDateTime = DateTime.Now;

    private void Start()
    {
        StartCoroutine(GetRealDateTimeFromAPI());

    }

    public DateTime GetStartDateTime()
    {
        return currentDateTime;
    }

    public DateTime GetDateTimeNow()
    {
        StartCoroutine(GetRealDateTimeFromAPI());
        return currentDateTime;
    }

    public IEnumerator GetRealDateTimeFromAPI()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(API_URL);
        yield return webRequest.SendWebRequest();

        if(webRequest.isNetworkError|| webRequest.isHttpError)
        {
            Debug.Log("Error: "+webRequest.error);
        }
        else
        {
            string timeDate = webRequest.downloadHandler.text;
            currentDateTime = ParseDateTime(timeDate);
        }
    }


    DateTime ParseDateTime(string dateTime)
    {
        try
        {
            // Thay đổi regex để khớp với định dạng YYYY-MM-DD và HH:MM:SS (nếu đây là định dạng API trả về)
            string date = Regex.Match(dateTime, @"\d{4}-\d{2}-\d{2}").Value;
            string time = Regex.Match(dateTime, @"\d{2}:\d{2}:\d{2}").Value;

            if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(time))
            {
                Debug.LogError("Date or time could not be parsed. Input string: " + dateTime);
                return DateTime.MinValue;
            }

            // Kết hợp date và time để tạo thành chuỗi ngày giờ hoàn chỉnh
            string dateTimeString = string.Format("{0} {1}", date, time);
            return DateTime.Parse(dateTimeString);
        }
        catch (FormatException ex)
        {
            Debug.LogError("Failed to parse date time. Error: " + ex.Message + ". Input string: " + dateTime);
            return DateTime.MinValue;
        }
    }

}
