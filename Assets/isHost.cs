using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class isHost :  MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            transform.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
}
