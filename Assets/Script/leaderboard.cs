using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class leaderboard : MonoBehaviour
{

    public static Tenenet tenenet;
    public GameObject rowPrefab;
    public Transform rowsParent;


    // Start is called before the first frame update
    void Start()
    {
        tenenet = GetComponent<Tenenet>();
        // StartCoroutine(tenenet.updatePlayerScore());
        StartCoroutine(tenenet.getScoreLeaderboard());

        // for(int i = 0; i < 5; i++){

        //     GameObject newRow = Instantiate(rowPrefab, rowsParent);
        //     TMP_Text[] texts = newRow.GetComponentsInChildren<TMP_Text>();
        //     texts[0].text = "Rank";
        //     texts[1].text = "Alias";
        //     texts[2].text = "Score";

        // }


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
