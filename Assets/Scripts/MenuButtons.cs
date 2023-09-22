using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private AudioClip buttonHoverSound;
    [SerializeField] private AudioClip buttonClickSound;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonHoverSound()
    {
        audioSource.PlayOneShot(buttonHoverSound, 0.2f);

    }
    public void ButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound, 0.3f);
    }


}
