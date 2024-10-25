using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensetivity;

    public GameObject player;
    public Vector3 followOffset;

    Vector2 rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.fixedDeltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.fixedDeltaTime;

        rotation.y += _mouseX;
        rotation.x -= _mouseY;
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);

        // Rotation
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        player.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        // Follow player position
        transform.position = player.transform.position + followOffset;
    }
}
