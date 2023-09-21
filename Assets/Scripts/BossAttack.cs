using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Adjust this based on your game's requirements.
    public int attackDamage = 2;    // The damage the boss deals to the player.
    public LayerMask playerLayer;    // Set this to the layer that the player is on.

    private void PerformAttack()
    {
        // Perform your boss's attack logic here.

        // Check for the player in range.
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        // Deal damage to all hit players.
        foreach (Collider2D playerCollider in hitPlayers)
        {
            PlayerMovement player = playerCollider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                // Deal damage to the player.
                player.TakeDamage(attackDamage);
            }
        }
    }
}