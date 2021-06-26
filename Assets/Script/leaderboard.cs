using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leaderboard : MonoBehaviour
{

    public static Tenenet tenenet;
    // Start is called before the first frame update
    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        // StartCoroutine(tenenet.updatePlayerScore());
        StartCoroutine(tenenet.getScoreLeaderboard());


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BackButton(string page)
    {
        SceneManager.LoadScene(page);
    }
}
