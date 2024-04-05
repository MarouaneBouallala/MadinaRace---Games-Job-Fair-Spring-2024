using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DealApproved : MonoBehaviour, IPointerUpHandler
{
    public int price, fuelQuantity, addToScore;
    public void OnPointerUp(PointerEventData eventData)
    {
        DealMade();
    }

    IEnumerator SuccessfulDeal()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().CachingSound();
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().CoinDropSound();
        int score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.SetInt("Score", score + addToScore);
    }

    private void NotEnoughFunds()
    {
        // The Player will be adverted if there is not enough funds
    }


    public void DealMade()
    {
        int goldCoinBalance = PlayerPrefs.GetInt("GoldCoins");
        Debug.Log("Deal Approved");
        if (goldCoinBalance >= price)
            {
                StartCoroutine(SuccessfulDeal());
                    PlayerPrefs.SetInt("GoldCoins", goldCoinBalance - price);
                    int fuelRefills = PlayerPrefs.GetInt("fuelRefills");
                    PlayerPrefs.SetInt("fuelRefills", fuelRefills + fuelQuantity);
                    GameObject.Find("SoundManager").GetComponent<SoundManager>().FuelRefillGained();
            }
        else if (goldCoinBalance < price)
            {
                Debug.Log("Collect more coins");
            }
    }
}



