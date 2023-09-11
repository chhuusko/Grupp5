using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private Transform leftFoot, rightFoot;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip[] jumpSounds;
    [SerializeField] private GameObject keyParticles, dustParticles;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color greenHealth, lighterGreenHealth, yellowHealth, orangeHealth, redHealth;
    [SerializeField] private TMP_Text keyText;
    
    private float horizontalValue;
    private float rayDistance = 0.25f;
    private bool isGrounded;
    private bool canMove;
    private int startingHealth = 5;
    private int currentHealth = 0;
    public int keysCollected = 0;

    private Rigidbody2D rgbd;
    private SpriteRenderer rend;
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        currentHealth = startingHealth;
        keyText.text = "" + keysCollected;
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {          
        horizontalValue = Input.GetAxis("Horizontal");    
        
        // Flips the sprite to face the direction we are moving it

        if(horizontalValue < 0) 
        {
            FlipSprite(true);
        }
    
        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }  

        if (Input.GetButtonDown("Jump") && CheckIfGrounded() == true)
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());

    }

    private void FixedUpdate()
    {
        //if NOTcanMove
        if(!canMove)
        {
            return;
        }

        //For more smooth movement and response
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed * Time.deltaTime, rgbd.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            keysCollected++;
            keyText.text = "" + keysCollected;
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(pickupSound, 0.25f);
            Instantiate(keyParticles, other.transform.position, Quaternion.identity);
            
        }

        if (other.CompareTag("RedPotion"))
        {
            RestoreHealth(other.gameObject);
           
        }
    }

    private void FlipSprite(bool direction)
    {
        rend.flipX = direction;
    }

    private void Jump()
    {
        //Adds force vertically making the sprite jump on jump input

        rgbd.AddForce(new Vector2(0, jumpForce));
        int randomValue = Random.Range(0, jumpSounds.Length);
        audioSource.PlayOneShot(jumpSounds[randomValue], 0.20f);
        Instantiate(dustParticles, transform.position, dustParticles.transform.localRotation);
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        UpdateHealthBar();

        if(currentHealth <= 0)
        {
            Respawn();
        }
    }

    public void TakeKnockback(float knockbackForce, float knockbackUpwards)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, knockbackUpwards));
        Invoke("CanMoveAgain", 0.25f);
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        UpdateHealthBar();
        transform.position = spawnPosition.position;
        rgbd.velocity  = Vector2.zero;
    }

    private void RestoreHealth(GameObject redPotion)
    {
        if (currentHealth >= startingHealth) 
        {
            return;
        }
        else
        {
            int healthToRestore = redPotion.GetComponent<RedPotion>().healthAmount;
            currentHealth += healthToRestore;
            UpdateHealthBar();
            Destroy(redPotion);

            if(currentHealth >= startingHealth)
            {
                currentHealth = startingHealth;
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;

        if(currentHealth >= 5)
        {
            fillColor.color = greenHealth;
        }
        if (currentHealth == 4)
        {
            fillColor.color = lighterGreenHealth;
        }
        if (currentHealth == 3)
        {
            fillColor.color = yellowHealth;
        }
        if (currentHealth == 2)
        {
            fillColor.color = orangeHealth;
        }
        if (currentHealth == 1)
        {
            fillColor.color = redHealth;
        }
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);


        //Draws the ray

        //Debug.DrawRay(leftFoot.position, Vector2.down * rayDistance, Color.blue, 0.25f);
        //Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);

        //Checks if either foot is on ground

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
