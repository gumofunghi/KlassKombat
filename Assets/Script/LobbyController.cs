using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public static LobbyController lobby;
    public static string roomId;
    public static List<RoomInfo> roomList = new List<RoomInfo>();
    public Text roomIdText;
    public InputField roomIdInput;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);

        PhotonNetwork.JoinLobby();

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList.Count);

        foreach (var room in roomList)
        {
            Debug.Log(room.Name);
        }
    }

    public void CreateRoom()
    {
        roomId = RandomString();

        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = 4 };
        TypedLobby typedLobby = new TypedLobby(roomId, LobbyType.Default); //3

        PhotonNetwork.CreateRoom(roomId, roomOptions);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail to create room, so sad");
    }

    public override void OnCreatedRoom()
    {   Debug.Log(roomId);
        roomIdText.text = "Current Room ID: " + roomId;
        OnRoomListUpdate(roomList);
    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(roomIdInput.text);
        // int t = PhotonNetwork.room.PlayerCount();
        // Debug.Log(t);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("haha, join fail, u sucks");

    }

    public override void OnJoinedRoom()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("joined as host");
        }
        else
        {
            Debug.Log("join room nia");
        }

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        roomIdText.text = "Current Room ID: ";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private string RandomString()
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
