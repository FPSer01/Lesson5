using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISocket
{
    void SetItemInSocket(GameObject item);
    GameObject PutOutItem();
}
