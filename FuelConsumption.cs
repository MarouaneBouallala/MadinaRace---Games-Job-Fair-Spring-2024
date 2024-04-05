using System.Collections;
using UnityEngine;

public class FuelConsumption : MonoBehaviour
{
    public bool isPlaying, isNeedingRefuel;
    public float timeBeingPlaying, fuelQuantity, fuelConsumptionPerFrame, fuelLeft = 45f, fuelAdvertThreshold = -40f;
    public GameObject gameOn, gameOff;
    public int fuelRefills;

    // Update is called once per frame
    void Update()
    {
        isPlaying = GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying;
        FuelUpdate();
    }

    private void FuelUpdate()
    {
        if (isPlaying)
        {
            if (fuelLeft > -45f)
            {
                isNeedingRefuel = false;
                fuelLeft -= fuelConsumptionPerFrame * Time.deltaTime;
                GameObject.Find("Canvas").GetComponent<UIManager>().UpdateFuelIndicator(fuelLeft * -1);
            }

            if (fuelLeft < fuelAdvertThreshold)
            {
               Debug.Log("Time To Refill");
                if (!isNeedingRefuel & PlayerPrefs.GetInt("fuelRefills") >= 1)
                    {
                        isNeedingRefuel = true;
                        GameObject.Find("Canvas").GetComponent<UIManager>().ShowRefuelSlider();
                        StartCoroutine(OptingOutForNotRefuelingCor());
                    }
                if (PlayerPrefs.GetInt("fuelRefills") <= 0)
                    {
                        Debug.Log("GameOver fuel is over and you have no refills");
                        isPlaying = false;
                        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
                    }
            }
        }
    }


    IEnumerator OptingOutForNotRefuelingCor()
    {
        Debug.Log("Game Over You Didn't Refuel.");
        yield return new WaitForSeconds(10);
        if (isNeedingRefuel)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

}
