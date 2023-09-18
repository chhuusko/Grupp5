using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShell : MonoBehaviour
{

    [SerializeField] private float jumpForce;
    [SerializeField] private AudioClip[] seaShellSounds;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2 (playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            int randomValue = Random.Range(0, seaShellSounds.Length);
            audioSource.PlayOneShot(seaShellSounds[randomValue], 0.20f);
        }
    }
}
