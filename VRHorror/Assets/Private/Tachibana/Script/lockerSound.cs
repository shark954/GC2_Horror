using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockerSound : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        if (audioSource != null) 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.Play();
    }
}
