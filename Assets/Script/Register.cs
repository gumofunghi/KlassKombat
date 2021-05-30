using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Register : MonoBehaviour
{

    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button RegisterButton;
    public static Tenenet tenenet;

    // Start is called before the first frame update
    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        RegisterButton.onClick.AddListener(() => {
            StartCoroutine(tenenet.playerResgister(UsernameInput.text, PasswordInput.text));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
