using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text question;
    public Button[] answers;

    public GameObject[] entities;

    public Text timer;

    public QuestionRoot qs;

    public int currIndex;

    private PhotonView PV;

    private GameObject player;

    float time = 180;

    void Start()
    {
        currIndex = 0;
        string jsonString = File.ReadAllText("questions.json");
        qs = JsonUtility.FromJson<QuestionRoot>(jsonString);

        PV = GetComponent<PhotonView>();

        if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["home"])
        {
            player = PhotonNetwork.Instantiate("EntityLeft", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }
        else
        {
            player = PhotonNetwork.Instantiate("EntityRight", new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }

        PV.RPC("DisplayTeam", RpcTarget.AllBuffered);

    }

    // Update is called once per frame
    void Update()
    {

        if (time > 0)
        {
            time -= Time.deltaTime;
            timer.text = time.ToString("F0");
        }
        else
        {
            GameOver();
        }

        ProcessInputs();
        question.text = qs.questions[currIndex].title;

        for (int i = 0; i < 4; i++)
        {
            answers[i].GetComponentInChildren<Text>().text = qs.questions[currIndex].choices[i];
        }

    }

    void ProcessInputs()
    {
        if (Input.GetKeyDown("space"))
        {
            print(PhotonNetwork.NickName);
        }
    }

    public void Answered()
    {
        string answer = EventSystem.current.currentSelectedGameObject.name;

        if (int.Parse(answer) == qs.questions[currIndex].answer)
        {
            print("yesy");
        }
        else{
            print("NO");
        }

        // if (PV.IsMine)
        // {
        //     currIndex++;
        // }

    }

    [PunRPC]
    void DisplayTeam()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            Player Owner = p.GetPhotonView().Owner;

            if ((bool)Owner.CustomProperties["home"])
            {
                GameObject homeTeam = GameObject.Find("Home");
                p.transform.SetParent(homeTeam.transform, false);

            }
            else
            {
                GameObject awayTeam = GameObject.Find("Away");
                p.transform.SetParent(awayTeam.transform, false);
            }
        }
    }

    void GameOver()
    {
        print("we r end game now!");
    }

}


[System.Serializable]
public class Question
{

    public int id, answer;
    public string title;
    public string[] choices;

}

[System.Serializable]
public class QuestionRoot
{
    public Question[] questions;
}
