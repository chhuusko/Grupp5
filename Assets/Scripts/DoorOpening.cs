using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpeningSound;

    private AudioSource audioSource;
    private Animator anim;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ExitDoor()
    {
        audioSource.pitch = Random.Range(0.7f, 1.1f);
        audioSource.PlayOneShot(doorOpeningSound, 0.5f);
        anim = GetComponent<Animator>();
        anim.Play("DoorOpening");
    }

    public void InvokeExitDoor()
    {
        Invoke("ExitDoor", 1);
    }
}
