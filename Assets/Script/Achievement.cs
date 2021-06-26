using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achievement : MonoBehaviour
{   
    public void BackButton(string page)
    {
        SceneManager.LoadScene(page);
    }

}
