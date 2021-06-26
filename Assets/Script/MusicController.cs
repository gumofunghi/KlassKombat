using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource source;
    private GameObject[] other;
    private bool NotFirst = false;
    void Awake()
    {
        other = GameObject.FindGameObjectsWithTag("Music");

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        source = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if (source.isPlaying) return;
        source.Play();
    }

    public void StopMusic()
    {
        source.Stop();
    }
}
