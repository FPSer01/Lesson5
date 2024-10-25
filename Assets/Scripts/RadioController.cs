using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour, IInteractable
{
    private AudioSource musicSource;
    [SerializeField] bool isOn;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (isOn)
        {
            isOn = false;
            musicSource.Pause();
        }
        else
        {
            isOn = true;
            musicSource.Play();
        }
    }
}
