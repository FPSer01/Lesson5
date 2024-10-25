using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    CharacterController controller;
    public float speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 move = gameObject.transform.forward * Input.GetAxisRaw("Vertical") + gameObject.transform.right * Input.GetAxisRaw("Horizontal");
        controller.Move(move * Time.fixedDeltaTime * speed);
    }
}
