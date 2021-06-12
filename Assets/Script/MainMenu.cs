using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MainMenu : MonoBehaviourPunCallbacks
{

    public static string roomId;

    public Text username;
    public InputField roomIdInput;
    public Hashtable hashtable;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);

        PhotonNetwork.NickName = "Merry " + RandomString();

        username.text = PhotonNetwork.NickName;

        PhotonNetwork.AutomaticallySyncScene = true;

    }

    public void CreateRoom()
    {
        roomId = RandomString();

        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = 4, PublishUserId = true, BroadcastPropsChangeToAll = true};

        TypedLobby typedLobby = new TypedLobby(roomId, LobbyType.Default);

        PhotonNetwork.CreateRoom(roomId, roomOptions);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail to create room, so sad");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomIdInput.text);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("haha, join fail, u sucks");

    }

    public override void OnJoinedRoom()
    {   

        PhotonNetwork.LoadLevel("Scene/Lobby");
    }

    private string RandomString()
    {
        int length = 5;
        string result = "";

        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < length; i++)
        {
            int a = Random.Range(0, characters.Length);

            result = result + characters[a];
        }

        return result;
    }


}
