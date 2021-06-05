using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{

    private PhotonView PV;
    public Text roomIdText;
    public Text homeTeam;
    public Text awayTeam;


    void Start()
    {
        roomIdText.text = PhotonNetwork.CurrentRoom.Name;

        PV = GetComponent<PhotonView>();

        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            playerProperties["team"] = 0;
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
        }
        else
        {
            playerProperties["team"] = 1;
            PhotonNetwork.LocalPlayer.CustomProperties = playerProperties;
        }

        if (PV.IsMine)
        {
            PV.RPC("TeamAssignment", RpcTarget.AllBuffered);
        }


    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        PhotonNetwork.LoadLevel("Scene/MainMenu");

    }

    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Scene/MainGame");
            }
        }
    }

    public override void OnPlayerLeftRoom(Player quitter)
    {
        // foreach (Player player in PhotonNetwork.PlayerList)
        // {
        //     playerList.text += player.NickName + "\n";

        // }
    }

    [PunRPC]
    void TeamAssignment()
    {

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["team"] == 0)
        {
            homeTeam.text += "\n" + PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            awayTeam.text += "\n" + PhotonNetwork.LocalPlayer.NickName;
        }

    }

}
