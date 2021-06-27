﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Tenenet : MonoBehaviour
{
    const string URL = "http://api.tenenet.net";
    const string token = "7e8e7ff7131ea47672476ec9baea8b3a";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //  private IEnumerator registerNewPlayer()
    // {
    //     UnityWebRequest www = UnityWebRequest.Get(URL + "/createPlayer" + "?token=" + token + "&alias=" + alias + "&id=" + id + "&fname=" + fname + "&lname=" + lname);

    //     yield return www.SendWebRequest();

    //     if (www.isNetworkError || www.isHttpError){
    //         Debug.Log(www.error);
    //     }
    //     else
    //     {

    //         Debug.Log(www.downloadHandler.text);

    //     }
    // }

    // private IEnumerator getPlayer()
    // {
    //     UnityWebRequest www = UnityWebRequest.Get(URL + "/getPlayer" + "?token=" + token + "&alias=" + alias );

    //     yield return www.SendWebRequest();

    //     if (www.isNetworkError || www.isHttpError){
    //         Debug.Log(www.error);
    //     }
    //     else
    //     {

    //         Debug.Log(www.downloadHandler.text);

    //     }
    // }

    public IEnumerator playerLogin(string username, string password)
    {

        string alias = username + password;
        UnityWebRequest www = UnityWebRequest.Get(URL + "/getPlayer" + "?token=" + token + "&alias=" + alias);

        yield return www.SendWebRequest();

        Response r = new Response();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);

            if (r.status == 1)
            {
                UserInfo.isLogin = true;
                UserInfo.username = r.user.id;
                UserInfo.highest = int.Parse(r.user.score[0].value);

                SceneManager.LoadScene("Scene/MainMenu");

            }

        }

    }

    public IEnumerator playerResgister(string username, string password, string firstname, string lastname)
    {
        string alias = username + password;
        string id = username;
        string fname = firstname;
        string lname = lastname;
        UnityWebRequest www = UnityWebRequest.Get(URL + "/createPlayer" + "?token=" + token + "&alias="
                                + alias + "&id=" + id + "&fname=" + fname + "&lname=" + lname);

        yield return www.SendWebRequest();

        Response r = new Response();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            r = JsonUtility.FromJson<Response>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);

            if (r.status == 1)
            {
                UserInfo.isLogin = true;
                UserInfo.username = r.user.id;
                //UserInfo.highest = int.Parse(r.user.score[0].value);

                SceneManager.LoadScene("Scene/MainMenu");

            }

        }
    }

    // public IEnumerator createMetric(){
    //     string metric = "high_score";
    //     string id = "high_score";
    //     string name = "high_score";
    //     // string name = "high_score";


    //     UnityWebRequest www = UnityWebRequest.Get(URL + "/insertPlayerActivity" + "?token=" + token + "&metric=" + metric + "&id=" + id + "&operator=add" + "&value" + value);

    //     yield return www.SendWebRequest();

    //     if (www.isNetworkError || www.isHttpError){
    //         Debug.Log(www.error);
    //     }
    //     else
    //     {

    //         Debug.Log(www.downloadHandler.text);

    //     }
    // }

    public IEnumerator updatePlayerScore()
    {
        string alias = "loli123456";
        string id = "high_score";
        int value = 10;

        UnityWebRequest www = UnityWebRequest.Get(URL + "/insertPlayerActivity" + "?token=" + token + "&alias=" + alias + "&id=" + id + "&operator=" + "add" + "&value=" + value);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log(www.downloadHandler.text);

        }
    }

    public IEnumerator getScoreLeaderboard()
    {
        string id = "lb_score";

        UnityWebRequest www = UnityWebRequest.Get(URL + "/getLeaderboard" + "?token=" + token + "&id=" + id);

        yield return www.SendWebRequest();

        Response r = new Response();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            r = JsonUtility.FromJson<Response>(www.downloadHandler.text);

            if (r.status == 1)
            {
                //print(r.message.data.Length);
                for(int i = 0; i < (r.message.data.Length); i++){

                    string alias = r.message.data[i].alias;
                    int rank = r.message.data[i].rank;

                    //print(alias + "  " + rank);
                }
            }

        }
    }

    [System.Serializable]
    public class Response
    {

        public User user;
        public int status;
        public Message message;

    }

    
    [System.Serializable]
    public class Message
    {
        public Data[] data;
    }

    [System.Serializable]
    public class Data
    {
        public string alias;
        public int rank;
    }

    [System.Serializable]
    public class User
    {
        public string id;
        public Score[] score;
    }

    [System.Serializable]
    public class Score
    {
        public string metric_id, metric_name, metric_type, value;

    }


}
