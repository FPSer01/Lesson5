using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void Grab(Transform holdPoint);

    void Drop(Vector3 dropPoint);

    void TurnOffInteraction(bool value);

    void ChangeGrabStatus(bool isItemGrabbed);
}
