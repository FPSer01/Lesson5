using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetInteractable
{
    void MagnetTo(Vector3 target, float force);
}
