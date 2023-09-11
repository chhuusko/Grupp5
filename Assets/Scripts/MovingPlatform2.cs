using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    [SerializeField] private Transform target3, target4;
    [SerializeField] private float moveSpeed = 2.0f;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = target4;
    }


    void FixedUpdate()
    {
        if (transform.position == target3.position)
        {
            currentTarget = target4;
        }

        if (transform.position == target4.position)
        {
            currentTarget = target3;
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.position.x > transform.position.x)
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
