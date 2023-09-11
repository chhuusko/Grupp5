using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    //When player enters volume they get killed
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
