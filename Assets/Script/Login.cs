using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    public static Tenenet tenenet;

    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(tenenet.playerLogin(UsernameInput.text, PasswordInput.text));
        });


    }



    public void toRegisterPage()
    {
        SceneManager.LoadScene("RegisterPage");
    }

   
}
