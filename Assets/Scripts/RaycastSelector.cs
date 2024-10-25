using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSelector : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private Transform direction;
    [SerializeField] private Transform grabObjPoint;

    private RaycastHit hit;

    // Нынешний и предыдущий объект с интерфейсом IHoverable
    private GameObject obj;
    private IHoverable obj_help;

    private IInteractable interactableObj;

    // Объект который держим
    [SerializeField] private GameObject heldObj;
    private IGrabbable grabbableObj;
    [SerializeField] private bool haveGrabbedObj = false;
    private bool takenFromSocket = false;

    void Update()
    {
        // Считываение взаимодействия через луч
        if (Physics.Raycast(direction.position, direction.forward, out hit, length)) 
        {
            // Вывод подсказок
            RaycastHover(hit);

            // Взаимодействие с объектами
            RaycastInteract(hit);

            // Помещение в сокет, если держим объект с IGrabbable
            if (haveGrabbedObj)
            {
                RaycastPutInSocket(hit);
            }
            // Берем из сокета, если не держим предмет
            else
            {
                RaycastPutOutOfSocket(hit);
            }
        }
        else
        {
            // Скрытие подсказки если нет пересечения луча
            if (obj_help != null)
            {
                obj_help.Hover(false);
            }
        }

        // Выбрасывание предмета
        if (haveGrabbedObj && !takenFromSocket)
        {
            RaycastDrop(CalculateDropPoint(length, hit));
        }
        // Взятие предмета
        else if (!haveGrabbedObj && !takenFromSocket)
        {
            if (hit.collider != null)
            {
                RaycastGrab(hit.collider.gameObject, true);
            }
        }

        takenFromSocket = false;
    }

    Vector3 CalculateDropPoint(float maxDistance, RaycastHit hit)
    {
        Vector3 dropPoint;

        if (hit.collider != null)
        {
            Vector3 norm = hit.normal;
            dropPoint = hit.point + norm * 0.025f;

            return dropPoint;
        }
        else
        {
            dropPoint = direction.position + direction.forward * length;
            return dropPoint;
        }
    }

    // Ложим в сокет
    void RaycastPutInSocket(RaycastHit rayHit)
    {
        if (rayHit.collider.GetComponent<ISocket>() != null && Input.GetKeyDown(KeyCode.E))
        {
            ISocket socket = rayHit.collider.GetComponent<ISocket>();
            socket.SetItemInSocket(heldObj);

            heldObj = null;
            haveGrabbedObj = false;

            Debug.Log("Снап в сокет сработал");
        }
    }

    // Берем из сокета
    void RaycastPutOutOfSocket(RaycastHit rayHit)
    {
        if (rayHit.collider.GetComponent<ISocket>() != null && Input.GetKeyDown(KeyCode.E))
        {
            ISocket socket = rayHit.collider.GetComponent<ISocket>();
            GameObject givenItem = socket.PutOutItem();

            if (givenItem != null && givenItem.GetComponent<IGrabbable>() != null)
            {
                RaycastGrab(givenItem, false);

                Debug.Log("Вытаскивание из сокета сработало");
            }

            takenFromSocket = true;
        }
    }

    void RaycastDrop(Vector3 point)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            haveGrabbedObj = false;
            heldObj = null;

            grabbableObj.Drop(point);
            grabbableObj = null;

            Debug.Log($"Выбрасывание сработало: {point}");
        }
    }

    void RaycastGrab(GameObject targetItem, bool useInput)
    {
        if (targetItem.GetComponent<IGrabbable>() != null)
        {
            if (useInput && Input.GetKeyDown(KeyCode.E))
            {
                haveGrabbedObj = true;
                heldObj = targetItem;

                grabbableObj = targetItem.GetComponent<IGrabbable>();
                grabbableObj.Grab(grabObjPoint);

                Debug.Log("Поднятие сработало");
            }
            else if (!useInput)
            {
                haveGrabbedObj = true;
                heldObj = targetItem;

                grabbableObj = targetItem.GetComponent<IGrabbable>();
                grabbableObj.Grab(grabObjPoint);

                Debug.Log("Поднятие сработало");
            }
        }
    }

    void RaycastInteract(RaycastHit rayHit)
    {
        if (rayHit.collider.GetComponent<IInteractable>() != null && Input.GetKeyDown(KeyCode.E))
        {
            interactableObj = rayHit.collider.gameObject.GetComponent<IInteractable>();
            interactableObj.Interact();
        }
    }

    void RaycastHover(RaycastHit rayHit)
    {
        if (rayHit.collider.GetComponent<IHoverable>() != null)
        {
            // Если наводиться на другой объект, перед этим луч был наведен на какой-либо объект
            if (rayHit.collider.gameObject != obj && obj != null)
            {
                // Скрывается подсказка прошлого объекта
                obj_help.Hover(false);
            }

            // Запись нового объекта и показываем его подсказку
            obj = rayHit.collider.gameObject;
            obj_help = obj.GetComponent<IHoverable>();
            obj_help.Hover(true);
        }
        else
        {
            // Скрытие подсказки если это не Hoverable объект
            if (obj_help != null)
            {
                obj_help.Hover(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(direction.position, direction.position + direction.forward * length);

    }
}
