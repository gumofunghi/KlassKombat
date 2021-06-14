using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
