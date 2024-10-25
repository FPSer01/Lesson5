using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        Instantiate(player, transform.position, transform.rotation);
    }
}
