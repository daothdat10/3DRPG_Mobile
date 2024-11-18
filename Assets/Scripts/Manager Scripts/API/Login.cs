using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_InputField usernameInput, passwordInput;
    
    public Button loginButton;
    
    void Start()
    {
        loginButton.onClick.AddListener((() =>
                {
                   StartCoroutine( Main.instance.Web.Login(usernameInput.text, passwordInput.text));
                }
                
                ));
    }

    
}
