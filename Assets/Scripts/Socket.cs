using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Socket : MonoBehaviour, ISocket
{
    [SerializeField] GameObject currentItem;

    public void SetItemInSocket(GameObject item)
    {
        if (item.GetComponent<IGrabbable>() != null && currentItem == null)
        {
            currentItem = item;
            IGrabbable grabbableItem = item.GetComponent<IGrabbable>();

            grabbableItem.TurnOffInteraction(true);
            grabbableItem.ChangeGrabStatus(false);
            item.transform.SetParent(transform);

            item.transform.position = transform.position;
            item.transform.rotation = transform.rotation;
        }
    }

    public GameObject PutOutItem()
    {
        if (currentItem != null && currentItem.GetComponent<IGrabbable>() != null)
        {
            IGrabbable grabbableItem = currentItem.GetComponent<IGrabbable>();

            grabbableItem.TurnOffInteraction(false);
            grabbableItem.ChangeGrabStatus(false);

            currentItem.transform.SetParent(null);

            GameObject putOutItem = currentItem;
            currentItem = null;

            return putOutItem;
        }
        else
        {
            return null;
        }
    }
}
