using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviourPunCallbacks
{
    public GameObject gameCamera;
    public Text question;
    public Button[] answers;
    public GameObject[] entities;
    public GameObject[] fighters;
    public GameObject gameOver;
    public Text timer;
    public QuestionRoot qs;
    public int currIndex;
    private PhotonView PV;
    private GameObject player;
    float time = 180;
    private int[] nextQuestion = { 0, 0 }; // states: 0 - pending, 1 - incorrect, 2 - correct
    private int[] score = { 0, 0 };
    private const int dmg = 10;

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

        // if (time <= 0 || currIndex >= qs.questions.Length)
        if (currIndex >= qs.questions.Length)
        {
            //if (PhotonNetwork.IsMasterClient)
                //PV.RPC("GameOver", RpcTarget.AllBuffered, 2);
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
            int crit = Crit();

            PV.RPC("Action", RpcTarget.AllBuffered, true, player.GetComponent<PhotonView>().ViewID, crit, !(bool)PhotonNetwork.LocalPlayer.CustomProperties["home"]);
        }
        else
        {
            PV.RPC("Action", RpcTarget.AllBuffered, false, player.GetComponent<PhotonView>().ViewID, 0, false);
        }

    }
    public int Crit()
    {

        float critChance = 0.4f;

        float randValue = Random.value;

        if (randValue < (1f - critChance))
        {
            return 1;
        }
        else
        {

            return 2;
        }
    }

    public IEnumerator WaitForAttacks(float duration, int newHP, bool oppo)
    {
        yield return new WaitForSeconds(duration);

        fighters[oppo ? 1 : 0].GetComponent<Animator>().SetTrigger("attack");

        TopBar.SetHP(newHP, oppo ? 0 : 1);

        // check if opponent die
        if (TopBar.GetHP(oppo ? 0 : 1) <= 0)
        {
            print("someone dieded");
            PV.RPC("GameOver", RpcTarget.AllBuffered, oppo ? 1 : 0);
        }

        score[oppo ? 1 : 0] += 1;
        UpdateQuestion();


    }
    public void BackButton(string page)
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(page);
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
    void Action(bool isSuccess, int p, int crit, bool oppo)
    {

        GameObject result = PhotonView.Find(p).gameObject;

        if (isSuccess)
        {
            result.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("CheckMark_Simple_Icons_UI");

            int HP = TopBar.GetHP(oppo ? 0 : 1);

            if (crit >= 2)
            {
                gameCamera.GetComponent<Animator>().SetTrigger(oppo ? "awayAction" : "homeAction");
                StartCoroutine(WaitForAttacks(1f, HP - dmg * crit, oppo));
            }
            else
            {
                StartCoroutine(WaitForAttacks(0, HP - dmg * crit, oppo));
            }


        }
        else
        {
            result.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Cross_Simple_Icons_UI");

            nextQuestion[oppo ? 1 : 0] = 1;

            if (nextQuestion[0] + nextQuestion[1] >= 2)
            {
                UpdateQuestion();

            }

        }


    }

    void UpdateQuestion()
    {

        for (int u = 0; u < 2; u++)
        {
            entities[u].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("QuestionMark_Simple_Icons_UI");
            nextQuestion[u] = 0;
        }

        for (int i = 0; i < 4; i++)
        {
            answers[i].GetComponentInChildren<Button>().interactable = true;
        }


        currIndex++;
        print(currIndex + "   " + qs.questions.Length);

    }


    [PunRPC]
    void GameOver(int winner)
    {

        gameOver.SetActive(true);


        if (winner < 2)
        {
            gameOver.transform.GetChild(1).GetChild(winner == 0 ? 2 : 3).GetChild(1).GetComponent<Text>().text = "WIN";

        }

        for (int i = 0; i < 1; i++)
        {
            fighters[i].GetComponent<Animator>().SetTrigger("end");
            gameOver.transform.GetChild(1).GetChild(i + 2).GetChild(3).GetComponent<TMP_Text>().text = "Correct " + score[i].ToString();
        }

        //gameOver.transform.GetChild(1).GetComponent<TMP_Text>().text = time.ToString("F0");


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
