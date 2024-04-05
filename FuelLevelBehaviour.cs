using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FuelLevelBehaviour : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isDraggable = false;

    public void OnPointerUp(PointerEventData eventData)
    {
        isDraggable = false;
        transform.position = Vector3.Lerp(transform.position, new Vector3(145, -140), 2f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraggable = true;
        transform.position = new Vector3(Input.mousePosition.x, 0, 0);
    }


   
}
