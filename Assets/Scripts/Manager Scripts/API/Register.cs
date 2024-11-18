using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public TMP_InputField nameInput, passInput, confirmpassInput;
    
    public Button registerButton;
    
    void Start()
    {
        
        registerButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.instance.Web.RegisterUser(nameInput.text, passInput.text, confirmpassInput.text));
        });

    }

    
}
