using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    [SerializeField] MagnetField magnet;
    
    public void Interact()
    {
        if (magnet.isMagnetOn)
        {
            magnet.isMagnetOn = false;
        }
        else
        {
            magnet.isMagnetOn = true;
        }
    }
}
