using UnityEngine;
using UnityEngine.UI;

public class ChoiceIndicatorPosition : MonoBehaviour
{
    public GameObject car, truck, tukTuk;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().enabled = false;
        Invoke("ShowUp", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString(tag) == "Car")
        {
            transform.position = car.transform.position;
        }

        if (PlayerPrefs.GetString(tag) == "Truck")
        {
            transform.position = truck.transform.position;
        }

        if (PlayerPrefs.GetString(tag) == "TukTuk")
        {
            transform.position = tukTuk.transform.position;
        }
    }

    public void ShowUp()
    {
        GetComponent<Image>().enabled = true;
    }
}
