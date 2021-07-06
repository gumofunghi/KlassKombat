using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class Tenenet : MonoBehaviour
{
    const string URL = "http://api.tenenet.net";
    const string token = "fa217b002c55d2fcc51a1d4c7e3bddd7";

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

        string alias = username + "|" + password;
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
                UserInfo.username = r.message.id;
                UserInfo.alias = r.message.alias;
        
                //UserInfo.highest = int.Parse(r.user.score[0].value);

                SceneManager.LoadScene("Scene/MainMenu");

            }



        }

    }

    public IEnumerator playerResgister(string username, string password, string firstname, string lastname)
    {
        string alias = username + "|" + password;
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
                UserInfo.username = r.message.id;
                UserInfo.alias = alias;
    
                //UserInfo.highest = int.Parse(r.user.score[0].value);

                SceneManager.LoadScene("Scene/MainMenu");

            }

        }
    }

    // public IEnumerator updatePlayerAchievement(){
    //     string metric = "player_achievement";
    //     // string id = "high_score";
    //     // string name = "high_score";
    //     // string name = "high_score";


    //     UnityWebRequest www = UnityWebRequest.Get(URL + "/insertPlayerActivity" + "?token=" + token + "&metric=" + metric + "&id=" + id + "&operator=add" + "&value=" + value);

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
        string alias = "ramen|123456";
        string id = "high_score";
        int value = 30;

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
        string id = "lboard_score";

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
                GameObject[] rankings = GameObject.FindGameObjectsWithTag("Rank");

                for (int i = 0; i < 5; i++)
                {
                    
                    
                    string[] p = r.message.data[i].alias.Split('|');
                    string ranking = r.message.data[i].rank;

                    // UnityWebRequest www_player = UnityWebRequest.Get(URL + "/getPlayer" + "?token=" + token + "&alias=" + r.message.data[i].alias);
                    // Response player = new Response();
                    // player = JsonUtility.FromJson<Response>(www_player.downloadHandler.text);

                    // string score = player.message.data[0].score[0].value;

                    if (i <= r.message.data.Length)
                    {
                        rankings[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = ranking;
                        rankings[i].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = p[0];
                        rankings[i].transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = p[0];
                    }

                }
            }

        }
    }

    public IEnumerator getPlayerAchievements()
    {
        //string alias = UserInfo.alias;
        string alias = "sushi|123456";



        UnityWebRequest www = UnityWebRequest.Get(URL + "/getPlayer" + "?token=" + token + "&alias=" + alias);

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
                GameObject[] achievements = GameObject.FindGameObjectsWithTag("Achievement");

                string[] p = r.message.score[5].value.Split(',');

                for (int i = 0; i < p.Length ; i++)
                {
                    
                    if(p[i]=="a1")
                        achievements[0].transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = "Achieved";
                    

                }
            }

        }
    }

    [System.Serializable]
    public class Response
    {

        // public User user;
        public int status;
        public Message message;

    }

    [System.Serializable]
    public class Message
    {
        public Data[] data;
        public string id;
        public string alias;
        public Score[] score;
    }

    [System.Serializable]
    public class Data
    {
        public string alias;
        public string rank;
        public Score[] score;
    }

    [System.Serializable]
    public class Score
    {
        public string metric_id, metric_name, metric_type, value;

    }


}
