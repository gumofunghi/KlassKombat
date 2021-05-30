using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{

    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;
    public static Tenenet tenenet;

    // Start is called before the first frame update
    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(tenenet.playerLogin(UsernameInput.text, PasswordInput.text));
        });
    }

   
}
