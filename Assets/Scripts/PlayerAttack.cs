using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AudioClip attackHitSound;
    [SerializeField] private AudioClip[] attackSounds;
    
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.1f;
    private float timer = 0.1f;
    private AudioSource audioSource;
    


    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            int randomValue = Random.Range(0, attackSounds.Length);
            audioSource.PlayOneShot(attackSounds[randomValue], 0.35f);
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }

    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
       
    }

}
