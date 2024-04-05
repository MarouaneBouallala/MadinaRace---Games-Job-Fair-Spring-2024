using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// All the offers or valued here based on "Tiers" : low value tier, middle value tier and high value tier.
/// </summary>
public class Deals : MonoBehaviour
{

    public GameObject[] deal;
    public Transform canvasTransform;
    public float dealShowInterval = 4;
    public int dealGoldValue, dealFuelValue, dealPowerUpID, dealPowerUpValue;
    public Text dealGoldValueText, dealFuelValueText, magnetPowerUpText, ghostPowerUpText, shieldPowerUpText, fireBallPowerUpText, nitroPowerUpText;

    public int[] goldValuesArray = { 1, 2, 5, 10, 15, 20, 30, 40, 45, 50, 55, 60, 65, 70, 75, 80, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700, 750, 800, 850, 900, 950, 1000, 1250, 1500, 1750, 2000 };
    public int[] fuelValuesArray = { 1, 2, 5, 6, 7, 9, 10, 11, 12, 13, 15, 20, 21, 24, 25, 26, 28, 30, 35, 40, 45, 50, 55, 65, 100 };
    void Start()
    {
        canvasTransform = GameObject.Find("Canvas").GetComponent<Transform>();
        StartCoroutine(ShowDealCor());
    }
    void Update()
    {

    }


    IEnumerator ShowDealCor()
    {
        float waitForDeal = Random.Range(15, 20);
        yield return new WaitForSeconds(waitForDeal);
        int dealIndex = Random.Range(0, deal.Length);
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
            deal[dealIndex].SetActive(true);
        yield return new WaitForSeconds(dealShowInterval);
        deal[dealIndex].SetActive(false);
        float waitForNextDeal = Random.Range(20, 40);
        yield return new WaitForSeconds(waitForNextDeal);
        StartCoroutine(ShowDealCor());
    }

    private int dealFuelValueIndex;
    private int dealGoldValueIndex;
    private int magnetPowerUpQuantity, ghostPowerUpQuantity, shieldPowerUpQuantity, fireBallPowerUpQuantity, nitroPowerUpQuantity;

    public void GenerateDeal()
    {
        StartCoroutine(DealGeneratorCor());
    }

    IEnumerator DealGeneratorCor()
    {
        dealGoldValueIndex = Random.Range(0, goldValuesArray.Length);

        if (dealGoldValueIndex <= 5)
        {
            dealFuelValueIndex = Random.Range(0, 5);
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }
        else if (dealGoldValueIndex <= 10)
        {
            dealFuelValueIndex = Random.Range(5, 7);
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }
        else if (dealGoldValueIndex <= 15)
        {
            dealFuelValueIndex = Random.Range(7, 10);
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }
        else if (dealGoldValueIndex <= 20)
        {
            dealFuelValueIndex = Random.Range(10, 16);
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }
        else
        {
            dealFuelValueIndex = Random.Range(16, 18);
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }

        // If dealFuelValue is 0 then fuel value 1
        if (dealFuelValueIndex == 0)
        {
            dealFuelValueIndex = 1;
            dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();
        }

        // Set text values
        dealGoldValueText.text = goldValuesArray[dealGoldValueIndex].ToString();
        dealFuelValueText.text = fuelValuesArray[dealFuelValueIndex].ToString();


        magnetPowerUpQuantity = Random.Range(0, 5);
        magnetPowerUpText.text = magnetPowerUpQuantity.ToString();

        ghostPowerUpQuantity = Random.Range(0, 5);
        ghostPowerUpText.text = ghostPowerUpQuantity.ToString();

        shieldPowerUpQuantity = Random.Range(0, 5);
        shieldPowerUpText.text = shieldPowerUpQuantity.ToString();

        fireBallPowerUpQuantity = Random.Range(0, 5);
        fireBallPowerUpText.text = fireBallPowerUpQuantity.ToString();

        nitroPowerUpQuantity = Random.Range(0, 5);
        nitroPowerUpText.text = nitroPowerUpQuantity.ToString();




        yield return new WaitForSeconds(2f);
        yield return null;
    }

    public void GenerateFuelDeal()
    {

    }

    public void ApproveGeneratedDeal()
    {
        int goldCoinBalance = PlayerPrefs.GetInt("GoldCoins");
        Debug.Log("Deal Approved");
        if (goldCoinBalance >= goldValuesArray[dealGoldValueIndex])
        {
            PlayerPrefs.SetInt("GoldCoins", goldCoinBalance - goldValuesArray[dealGoldValueIndex]);
            int fuelRefills = PlayerPrefs.GetInt("fuelRefills");
            PlayerPrefs.SetInt("fuelRefills", fuelRefills + fuelValuesArray[dealFuelValueIndex]);
            PlayerPrefs.GetInt("alphaQuantity");
            PlayerPrefs.SetInt("magnetQuantity", PlayerPrefs.GetInt("magnetQuantity") + magnetPowerUpQuantity);
            PlayerPrefs.SetInt("alphaQuantity", PlayerPrefs.GetInt("alphaQuantity") + ghostPowerUpQuantity);
            PlayerPrefs.SetInt("shieldQuantity", PlayerPrefs.GetInt("shieldQuantity") + shieldPowerUpQuantity);
            PlayerPrefs.SetInt("armQuantity", PlayerPrefs.GetInt("nitroQuantity") + fireBallPowerUpQuantity);
            PlayerPrefs.SetInt("nitroQuantity", PlayerPrefs.GetInt("nitroQuantity") + nitroPowerUpQuantity);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().FuelRefillGained();
        }
        else if (goldCoinBalance < goldValuesArray[dealGoldValueIndex])
        {
            Debug.Log("Collect more coins");
        }
    }


}
