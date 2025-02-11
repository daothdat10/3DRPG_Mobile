using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDate : MonoBehaviour
{
    public Button getdateButton;

    private void Start()
    {
        getdateButton.onClick.AddListener((() =>
        {
            StartCoroutine(Main.instance.Web.GetDate());
        }));
    }
}
