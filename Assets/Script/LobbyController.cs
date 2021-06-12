using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbyController : MonoBehaviourPunCallbacks
{

    private PhotonView PV;
    public Text roomIdText;
    public GameObject homeTeam;
    public GameObject awayTeam;
    private GameObject PlayerName;

    void Start()
    {
        roomIdText.text = PhotonNetwork.CurrentRoom.Name;

        PV = GetComponent<PhotonView>();

        PlayerName = PhotonNetwork.Instantiate("PlayerName", new Vector3(-200f, 250f, 0f), Quaternion.identity, 0);

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            Hashtable playerProperties = new Hashtable() { { "home", true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
        else
        {
            Hashtable playerProperties = new Hashtable() { { "home", false } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }


    }

    public override void OnPlayerPropertiesUpdate(Player target, Hashtable changedProps)
    {
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

    public void SwitchTeam()
    {
        Hashtable playerProperties = new Hashtable() { { "home", !(bool)PhotonNetwork.LocalPlayer.CustomProperties["home"] } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public override void OnPlayerLeftRoom(Player quitter)
    {
    }

    [PunRPC]
    void TeamAssignment()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("PlayerName");

        foreach (GameObject g in gs)
        {
            Player Owner = g.GetPhotonView().Owner;

            if ((bool)Owner.CustomProperties["home"])
            {
                g.GetComponentInChildren<Text>().text = Owner.NickName;
                g.transform.SetParent(homeTeam.transform, false);
            }
            else
            {
                g.GetComponentInChildren<Text>().text = Owner.NickName;
                g.transform.SetParent(awayTeam.transform, false);
            }

        }

    }



}
