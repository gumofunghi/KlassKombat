using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{   
    // singleton initialization 
    private static UserInfo instance = null;
    public static UserInfo Instance
    {
        get; private set;

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {

        {
            if (instance == null)
            {
                instance = FindObjectOfType<UserInfo>();

                if (instance == null)
                {

                    GameObject obj = new GameObject();
                    obj.name = "UserInfo";
                    //instance = obj.AddComponenet<UserInfo>();
                    DontDestroyOnLoad(obj);

                }

            }

            Instance = instance;

        }
    }

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public static bool isLogin;
    public static string alias;
    public static string username;
    public static int highest;

}
