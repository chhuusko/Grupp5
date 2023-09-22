using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private AudioSource audioSource;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void InvokeStartGame()
    {
        Invoke("StartGame", 1);
    }

    public void QuitGame() 
    { 
        Application.Quit();
    }  

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCredits() 
    {
        creditsPanel.SetActive(false);
    }

    public void InvokeCloseCredits()
    {
        Invoke("CloseCredits", 0.2f);
    }

}

    