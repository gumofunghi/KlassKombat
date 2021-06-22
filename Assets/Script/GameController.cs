using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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
    private int[] nextQuestion = { 0, 0 }; // states: 0 - pending, 1 - incorrect, 2 - correct

    private Hashtable roomProperties = new Hashtable();
    void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            TopBar.SetHP(100, i);
        }
    }

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

        if (time <= 0 || currIndex >= qs.questions.Length)
        {
            GameOver();
        }
        else
        {
            time -= Time.deltaTime;
            timer.text = time.ToString("F0");

            question.text = qs.questions[currIndex].title;

            for (int i = 0; i < 4; i++)
            {
                answers[i].GetComponentInChildren<Text>().text = qs.questions[currIndex].choices[i];
            }
        }

    }


    public void Answered()
    {
        string answer = EventSystem.current.currentSelectedGameObject.name;

        for (int i = 0; i < 4; i++)
        {
            answers[i].GetComponentInChildren<Button>().interactable = false;
        }

        if (int.Parse(answer) == qs.questions[currIndex].answer)
        {
            PV.RPC("Action", RpcTarget.AllBuffered, true, player.GetComponent<PhotonView>().ViewID, !(bool)PhotonNetwork.LocalPlayer.CustomProperties["home"]);
        }
        else
        {
            PV.RPC("Action", RpcTarget.AllBuffered, false, player.GetComponent<PhotonView>().ViewID, false);
        }

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

    [PunRPC]
    void Action(bool isSuccess, int p, bool team)
    {

        GameObject result = PhotonView.Find(p).gameObject;

        if (isSuccess)
        {
            result.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("CheckMark_Simple_Icons_UI");

            int newHP = TopBar.GetHP(team ? 0 : 1);

            TopBar.SetHP(newHP - 5, team ? 0 : 1);

            nextQuestion[team ? 0 : 1] = 2;

        }
        else
        {
            result.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Cross_Simple_Icons_UI");

            nextQuestion[team ? 0 : 1] = 1;
        }


        if (nextQuestion[0] + nextQuestion[1] >= 2)
        {
            for (int u = 0; u < 2; u++)
            {
                entities[u].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("QuestionMark_Simple_Icons_UI");
            }

            for (int i = 0; i < 4; i++)
            {
                answers[i].GetComponentInChildren<Button>().interactable = true;
            }

            nextQuestion[0] = 0;
            nextQuestion[1] = 0;

            currIndex++;
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
