using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosing : MonoBehaviour
{
    [SerializeField] private AudioClip doorClosingSound;
    
    private AudioSource audioSource;
    private Animator anim;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.7f, 1.1f);
        audioSource.PlayOneShot(doorClosingSound, 0.5f);
        anim = GetComponent<Animator>();
        anim.Play("DoorClosing");
    }


}
