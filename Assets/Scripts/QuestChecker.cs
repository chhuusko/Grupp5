using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestChecked : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;
    [SerializeField] private float timeToLoad = 3.5f;
    [SerializeField] private AudioClip doorOpeningSound;
    [SerializeField] private AudioClip chestOpeningSound;
    
    private AudioSource audioSource;
    private Animator anim;
    private Animator animExitDoor;
    private bool levelIsLoading = false;
    private GameObject ExitDoor;

    DoorOpening doorOpeningScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        doorOpeningScript = GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<DoorOpening>();
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
                {
                    anim.SetTrigger("Door");
                    audioSource.pitch = Random.Range(0.7f, 1.1f);
                    audioSource.PlayOneShot(doorOpeningSound, 0.5f);
                }              
                    
                               
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    anim.SetTrigger("Chest");
                    audioSource.PlayOneShot(chestOpeningSound, 0.3f);
                    doorOpeningScript.InvokeExitDoor();
                }

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
