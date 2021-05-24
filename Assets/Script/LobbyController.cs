using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static LobbyController lobby;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("haha, join fail, u sucks");

    }

    public void CreateRoom()
    {
        // int roomName = randomString();

        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        PhotonNetwork.CreateRoom("123456", roomOptions);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail to create room, so sad");
        CreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("yeah ");
    }

    public void JoinRoom()
    {
        
        string roomName = "123456";

        if(PhotonNetwork.JoinRoom(roomName)){
            Debug.Log("joined yeah=h");
        }
        else{
            Debug.Log("joined failed, u suc");
        }

    }



    // Update is called once per frame
    void Update()
    {

    }


    private string randomString()
    {
        int length = 5;
        string result = "";

        string characters = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < length; i++)
        {
            int a = Random.Range(0, characters.Length);

            result = result + characters[a];
        }

        return result;
    }
}
