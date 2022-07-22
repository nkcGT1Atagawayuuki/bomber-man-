using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip expse;
    public AudioClip pup;
            
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExplotionSE()
    {
        audioSource.PlayOneShot(expse);
    }

    public void PowerUpSE()
    {
        audioSource.PlayOneShot(pup);
    }
}
