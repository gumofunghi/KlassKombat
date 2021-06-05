using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text question;
    public Button[] answers;

    public GameObject[] entities;

    public QuestionRoot qs;

    public int currIndex;

    private PhotonView pv;

    void Start()
    {
        currIndex = 0;
        string jsonString = File.ReadAllText("questions.json");
        qs = JsonUtility.FromJson<QuestionRoot>(jsonString);

        pv = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;

        // foreach(Player p in LobbyController.homeList){
        //     entities[0].GetComponentInChildren<Text>().text = p.NickName;
        // }

        // foreach(Player p in LobbyController.awayList){
        //     entities[1].GetComponentInChildren<Text>().text = p.NickName;
        // }

    }

    // Update is called once per frame
    void Update()
    {
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

    public void NextQuestion()
    {
        if (pv.IsMine)
        {
            currIndex++;
        }

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
