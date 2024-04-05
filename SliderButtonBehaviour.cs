using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The refuel slider have an animation which indicates the user to slide a button, as soon as its touched the animation will be halted.
/// </summary>

public class SliderButtonBehaviour : MonoBehaviour, IPointerDownHandler
{
    public GameObject sliderParent;

    public void OnPointerDown(PointerEventData eventData)
    {
        sliderParent.GetComponent<SliderBehaviour>().HaltAnimation();
    }
}
