using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public Text nameText;

    void Start()
    {
        nameText.text = gameObject.GetPhotonView().Owner.NickName;

    }

}
