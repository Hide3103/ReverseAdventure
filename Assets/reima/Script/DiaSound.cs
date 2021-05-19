using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaSound : MonoBehaviour
{

    AudioSource juwelAudio;
    public AudioClip SE_GetJuwel;

    public static bool SoundOn = false;
    // Start is called before the first frame update
    void Start()
    {
        juwelAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundOn)
        {
            Debug.Log("なったよ");
            juwelAudio.PlayOneShot(SE_GetJuwel);
            SoundOn = false;
        }
    }

}
