using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{

    public GameObject overlay;
    public Text username;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion);
        PhotonNetwork.NickName = UserInfo.username;
        username.text = PhotonNetwork.NickName;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateGame(string CreateName)
    {
        //Debug.Log("CREATE");
        //SceneManager.LoadScene(CreateName);

        string createdRoomID = RandomString();
        RoomOptions roomOptions = new RoomOptions() { IsVisible = false, IsOpen = true, MaxPlayers = 4, PublishUserId = true, BroadcastPropsChangeToAll = true };
        TypedLobby typedLobby = new TypedLobby(createdRoomID, LobbyType.Default);
        PhotonNetwork.CreateRoom(createdRoomID, roomOptions);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail to create room, so sad");
    }

    public void JoinGameDialog()
    {
        overlay.SetActive(!overlay.active);

    }

    public void JoinGame()
    {
        string roomID = GameObject.Find("RoomID").GetComponentInChildren<Text>().text;

        if (roomID != "")
        {

            PhotonNetwork.JoinRoom(roomID);

        }

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("haha, join fail, u sucks");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Scene/Lobby");
    }

    public void LeaderboardGame(string LeaderboardName)
    {
        SceneManager.LoadScene(LeaderboardName);

    }
    public void AchievementGame(string AchievementName)
    {
        SceneManager.LoadScene(AchievementName);

    }
    public void Logout(string LoginPage)
    {   
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(LoginPage);
    }
    public void SettingGame(string SettingName)
    {
        SceneManager.LoadScene(SettingName);

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }
    private string RandomString()
    {
        int length = 5;
        string result = "";

        // string characters = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        for (int i = 0; i < length; i++)
        {
            int a = Random.Range(0, characters.Length);
            result = result + characters[a];
        }
        return result;
    }

}


