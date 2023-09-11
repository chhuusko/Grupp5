using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecked : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;
    [SerializeField] private float timeToLoad = 3.0f;

    private Animator anim;
    private bool levelIsLoading = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMovement>().keysCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(false);
                finishedText.SetActive(true);


                if(SceneManager.GetActiveScene().buildIndex == 1)              
                    anim.SetTrigger("Door");
                               
                if (SceneManager.GetActiveScene().buildIndex == 2)
                    anim.SetTrigger("Chest");
                
                
                Invoke("LoadNextLevel", timeToLoad);
                levelIsLoading = true;
                
            }
            else
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(false);
                unfinishedText.SetActive(true);
               
            }
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    } 

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
           
        }
    }
}
