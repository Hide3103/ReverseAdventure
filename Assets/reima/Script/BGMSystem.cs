using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSystem : MonoBehaviour
{
    public AudioClip sound;

    AudioSource audioSource;

    bool SoundOn = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.IsGoal==true&&SoundOn)
        {
            audioSource.PlayOneShot(sound);

            SoundOn = false;
        }
    }
}
