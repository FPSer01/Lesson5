using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField : MonoBehaviour
{
    [SerializeField] float magnetForce;
    [SerializeField] float magnetRadius;

    public bool isMagnetOn;

    void Update()
    {
        if (isMagnetOn)
        {
            MagnetObjs();
        }
    }

    void MagnetObjs()
    {
        Collider[] objs = Physics.OverlapSphere(transform.position, magnetRadius);

        if (objs.Length > 0)
        {
            foreach (Collider obj in objs)
            {
                IMagnetInteractable obj_rb = obj.GetComponent<IMagnetInteractable>();

                if (obj_rb != null)
                {
                    obj_rb.MagnetTo(transform.position - obj.transform.position, magnetForce);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
