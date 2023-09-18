using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private Animator anim;
    private bool hasPlayedAnimation = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip chainPulledSound;
    [SerializeField] private AudioClip pillarThumpSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasPlayedAnimation)
        {
            audioSource.PlayOneShot(chainPulledSound, 0.2f);
            hasPlayedAnimation = true;
            anim.SetTrigger("Move");
        }
    }

    private void pillarThump()
    {
        audioSource.PlayOneShot(pillarThumpSound, 8);
    }

}
