using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text question;
    public Button[] answers;

    public QuestionRoot qs;

    public int currIndex;

    void Start()
    {
        currIndex = 0;
        string jsonString = File.ReadAllText("Assets/questions.json");
        qs = JsonUtility.FromJson<QuestionRoot>(jsonString);

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        question.text = qs.questions[currIndex].title;

        for(int i = 0; i < 4; i++)
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
        currIndex++;
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
