using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public IEnumerator playerLogin(string username, string password){

        string alias = username + password;
        UnityWebRequest www = UnityWebRequest.Get(URL + "/getPlayer" + "?token=" + token + "&alias=" + alias );

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }
        else
        {
            
            Debug.Log(www.downloadHandler.text);
            
        }

    }

    public IEnumerator playerResgister(string username, string password)
    {
        string alias = username + password;
        string id = username;
        string fname = username;
        string lname = "";
        UnityWebRequest www = UnityWebRequest.Get(URL + "/createPlayer" + "?token=" + token + "&alias=" + alias + "&id=" + id + "&fname=" + fname + "&lname=" + lname);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError){
            Debug.Log(www.error);
        }
        else
        {
            
            Debug.Log(www.downloadHandler.text);
            
        }
    }
}
