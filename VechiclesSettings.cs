using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VechiclesSettings : MonoBehaviour, IPointerDownHandler
{
    public GameObject vehiculesList, settingsMenu_About, vehiculesChoiceIndicator;

    public void OnPointerDown(PointerEventData eventData)
    {

        GameObject.Find("SoundManager").GetComponent<SoundManager>().MenuChoiceSound();
        if (vehiculesList.gameObject.activeSelf)
        {
            vehiculesList.SetActive(false);
            vehiculesChoiceIndicator.SetActive(false);
            return;
        }
        if (!vehiculesList.gameObject.activeSelf)
        {
            vehiculesList.SetActive(true);
            vehiculesChoiceIndicator.SetActive(true);
            settingsMenu_About.SetActive(false);
            return;
        }
    }
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
