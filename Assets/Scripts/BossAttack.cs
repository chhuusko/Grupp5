using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public int attackDamage = 2; // The damage the boss deals to the player
    public LayerMask playerLayer; // Set this to the layer that the player is on
    [SerializeField] private float knockbackForce = 1000f; // Adjust this value to control the strength of the horizontal knockback
    [SerializeField] private float upwardForce = 500f; // Adjust this value to control the strength of the upward knockback
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Initialize the audio source component
        audioSource = GetComponent<AudioSource>();

        // Check if audioSource is null, and print a message if it is
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on boss GameObject.");
        }
    }

    private void PerformAttack()
    {
        // Check for the player in range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        // Deal damage and apply knockback to all hit players
        foreach (Collider2D playerCollider in hitPlayers)
        {
            PlayerMovement player = playerCollider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Deal damage to the player
                player.TakeDamage(attackDamage);

                // Play the attack sound with a volume of 0.5
                if (audioSource != null && attackSound != null)
                {
                    audioSource.PlayOneShot(attackSound, 0.5f);
                }

                if (player.transform.position.x > transform.position.x)
                {
                    player.gameObject.GetComponent<PlayerMovement>().TakeKnockback(knockbackForce, upwardForce);
                }
                else
                {
                    player.gameObject.GetComponent<PlayerMovement>().TakeKnockback(-knockbackForce, upwardForce);
                }
            }
        }
    }
}