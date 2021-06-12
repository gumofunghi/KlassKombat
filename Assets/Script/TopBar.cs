using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBar : MonoBehaviour
{
    public GameObject[] HPBar;
    static int[] HPValue = new int[2];

    static public void SetHP(int v, int index)
    {
        HPValue[index] = v;
    }

    static public int GetHP(int index)
    {
        return HPValue[index];
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            HPBar[i].GetComponentInChildren<Text>().text = HPValue[i].ToString();
        }
    }


}
