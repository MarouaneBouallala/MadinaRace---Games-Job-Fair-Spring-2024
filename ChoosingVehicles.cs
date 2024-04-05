using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoosingVehicles : MonoBehaviour, IPointerDownHandler
{

    public enum Vehicle
    {
        Car,
        Motorcycle,
        Truck,
        TukTuk,
       // A horse or a camel
    }

    public Vehicle vehicle;



    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerPrefs.SetString(tag, vehicle.ToString());
        GameObject.Find("SoundManager").GetComponent<SoundManager>().MenuChoiceSound();
    }
}
