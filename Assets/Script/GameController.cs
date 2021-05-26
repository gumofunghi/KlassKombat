using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void ProcessInputs()
    {
        if (Input.GetKeyDown("space"))
        {
            print(PhotonNetwork.NickName);
        }
    }
}
