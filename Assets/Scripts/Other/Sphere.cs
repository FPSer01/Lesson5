using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour, IInteractable
{
    public void Interact() 
    {
        if (transform.localScale.magnitude < 10) 
        {
            transform.localScale *= 1.0005f;
        }
    }
}
