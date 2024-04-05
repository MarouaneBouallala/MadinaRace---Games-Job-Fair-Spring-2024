using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// /// IN EXCHANGE FOR GOLD PLAYER GETS TO CHOOSE 
/// 1 _ From pre-made deals.
/// 2 _ Or from random deals generated with the use of a script "Deals" which is attached to 'Deals' gameObject .
/// (Once the deal panel/button is clicked the transaction will be made based on the offer).

public class GeneratedDealPanel : MonoBehaviour, IPointerUpHandler
{
    public float timeToDisappear = .1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("Deals").GetComponent<Deals>().GenerateDeal();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject.Find("Deals").GetComponent<Deals>().ApproveGeneratedDeal();
        StartCoroutine(DeactivateObject());
    }


    IEnumerator DeactivateObject()
    {
        yield return new WaitForSeconds(timeToDisappear);
        this.gameObject.SetActive(false);
    }

    



}
