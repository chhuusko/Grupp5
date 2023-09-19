using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private int hp = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float bounciness = 100f;
    [SerializeField] private float knockbackForce = 200f;
    [SerializeField] private float upwardForce = 100f;
    [SerializeField] private int damageDealt = 1;
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioClip attackHitSound;
    [SerializeField] private AudioClip pinkStarDeathSound;

    private SpriteRenderer rend;
    private bool canMove = true;
    private Animator anim;
    private Rigidbody2D rgbd;
    private AudioSource audioSource;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();  
        SetEnemyValues();
        audioSource = GetComponent<AudioSource>();
    }

    private void SetEnemyValues()
    {
        hp = data.hp;
        damage = data.damage;
        moveSpeed = data.moveSpeed;
    }

    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;

        if (hp <= 0)
        {
            Destroy(gameObject, 0.4f);
        }
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
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pinkStarDeathSound, 0.3f);
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounciness));
            anim.SetTrigger("Hit");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            canMove = false;
            Destroy(gameObject, 0.4f);

        }

        if (other.CompareTag("AttackArea"))
        {
            anim.SetTrigger("Dead");
            moveSpeed = 0;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(attackHitSound, 0.35f);
        }
    }
    
    public void destroy()
    {
        Destroy(gameObject, 1f);
    }

}

