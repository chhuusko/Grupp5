using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private AudioClip killzoneSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //When player enters volume they get killed
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player")) 
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(killzoneSound, 0.1f);
            other.gameObject.GetComponent<PlayerMovement>().Respawn();
            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }
    }
}
