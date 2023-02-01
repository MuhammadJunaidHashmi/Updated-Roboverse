using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{

    public AudioSource src;
    public AudioSource background;
    public AudioClip clip;


    void Start()
    {
        src.clip = clip;
        StartCoroutine(waitSound());
    }


    IEnumerator waitSound()
    {
        yield return new WaitForSeconds(12f);
        background.volume = 0.1f;
        src.Play();
        yield return new WaitForSeconds(5f);
        background.volume = 1f;
    }

}
