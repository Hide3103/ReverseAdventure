using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSystem : MonoBehaviour
{ 
    private AudioSource audioSource;
    private AudioSource audioSource2;

    public AudioClip ResultSound;
    public AudioClip Stgae1Sound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.clip = ResultSound;

        audioSource2 = this.gameObject.GetComponent<AudioSource>();
        audioSource2.clip = Stgae1Sound;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameSystem.IsGoal == true)
        {
            audioSource.Play();
            audioSource2.Stop();
        }
        else
        {
            audioSource.Stop();
            audioSource2.Play();
        }
    }
}
