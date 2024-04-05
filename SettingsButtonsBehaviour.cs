using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsButtonsBehaviour : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
    }

    public GameObject settingsMenu, settingsMenuElement_Vehicles, settingsMenuElement_about;
    public void SettingsMenu()
    {
        if (settingsMenu != null)
        {
            if (settingsMenu.activeSelf == false)
            {
                settingsMenu.SetActive(true);
                return;
            }
            else if (settingsMenu.activeSelf == true)
            {
                settingsMenu.SetActive(false);
                settingsMenuElement_about.SetActive(false);
                settingsMenuElement_Vehicles.SetActive(false);
                return;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.name == "SettingsButton")
        {
            SettingsMenu();
        }

        GameObject.Find("SoundManager").GetComponent<SoundManager>().MenuChoiceSound();
    }

   
}