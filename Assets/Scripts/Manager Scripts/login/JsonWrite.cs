
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JsonWrite : MonoBehaviour
{
    public TMP_InputField nameinputField;
    public TMP_InputField passwordinputField;

    public void Login()
    {
        WeaponData data = new WeaponData();
        data.name = nameinputField.text;
        data.password = passwordinputField.text;

        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.dataPath + "/WeaponDataFile.json",json);

    }

    public void Signin()
    {
        string json = File.ReadAllText(Application.dataPath + "/WeaponDataFile.json");
        WeaponData data = JsonUtility.FromJson<WeaponData>(json);

        nameinputField.text = data.name;
        passwordinputField.text = data.password;
    }
}
