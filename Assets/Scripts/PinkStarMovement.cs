using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageDealt = 1;
    private SpriteRenderer rend;
    private bool canMove = true;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();  
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2 (moveSpeed, 0) * Time.deltaTime);


        if(moveSpeed > 0 )
        {
            rend.flipX = true;
        }
        if(moveSpeed < 0)
        {
            rend.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //turns the sprite on collision

        if (other.gameObject.CompareTag("EnemyBlocks")) 
        {
            moveSpeed = -moveSpeed;

        }

        if (other.gameObject.CompareTag("PinkStar"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            
            other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damageDealt);
            moveSpeed = -moveSpeed;

            if(other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
            }

            else
            {
                other.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //makes the player bounce and get new momentum upwards when landing on top of PinkStar then destroying PinkStar

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
            GetComponent<Animator>().SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;
            Destroy(gameObject, 0.4f);   
        }
    } 

}
