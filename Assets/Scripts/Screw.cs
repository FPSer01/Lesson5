using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : MonoBehaviour, IMagnetInteractable, IGrabbable
{
    Rigidbody rb;
    CapsuleCollider col;

    [SerializeField] bool isGrabbed = false;

    // Правильные углы поворота болта
    Quaternion originalRotation;

    void Awake()
    {
        originalRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    public void MagnetTo(Vector3 target, float force)
    {
        rb.AddForce(target * force, ForceMode.Force);
    }

    public void Grab(Transform holdPoint)
    {
        if (!isGrabbed)
        {
            isGrabbed = true;

            TurnOffInteraction(true);

            transform.position = holdPoint.position;
            transform.SetParent(holdPoint);
            transform.rotation = originalRotation;
        }
    }

    public void Drop(Vector3 dropPoint)
    {
        if (isGrabbed)
        {
            isGrabbed = false;

            TurnOffInteraction(false);

            transform.SetParent(null);
            transform.position = dropPoint;
            transform.rotation = originalRotation;
        }
    }

    public void TurnOffInteraction(bool value)
    {
        col.enabled = !value;

        if (value)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void ChangeGrabStatus(bool isItemGrabbed)
    {
        isGrabbed = isItemGrabbed;
    }
}
