using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update

    private int team;


    void Start()
    {

    }

    public void setTeam(int t)
    {
        team = t;
    }

    public int getTeam()
    {
        return team;
    }
}
