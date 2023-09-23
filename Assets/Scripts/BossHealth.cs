using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    private AudioSource audioSource;

    public BossHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
     
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Invoke("InvokeStartMenu", 2);
        GetComponent<Animator>().SetTrigger("BossDeath");
        Destroy(gameObject, 2);

    }
    
    private void InvokeStartMenu()
    {
        SceneManager.LoadScene(0);
    }

}
