using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding GameObject has a Health or BossHealth component
        Health health = other.GetComponent<Health>();
        BossHealth bossHealth = other.GetComponent<BossHealth>();

        if (health != null)
        {
            // It's a regular enemy or player
            health.Damage(damage);
        }
        else if (bossHealth != null)
        {
            // It's the boss
            bossHealth.takeDamage(damage);
        }
    }
}