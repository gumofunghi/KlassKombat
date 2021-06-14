using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{

    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField FirstNameInput;
    public TMP_InputField LastNameInput;
    public Button RegisterButton;
    public static Tenenet tenenet;

    // Start is called before the first frame update
    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        RegisterButton.onClick.AddListener(() => {
            StartCoroutine(tenenet.playerResgister(UsernameInput.text, PasswordInput.text, FirstNameInput.text, LastNameInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void toLoginPage()
    {
        SceneManager.LoadScene("LoginPage");
    }
}
