using System.Collections;
using UnityEngine;

/// <summary>
/// this script contains a beta version of singltons that will serve to enhance the playability.
/// </summary>
public class PowerUpsAndPrizesAndVillainAndObstacles : MonoBehaviour
{
    public bool isPowerUp, isGoldCoin, isVillain, isObstacle, isRefuel, isTriggered;
    public int prize, addPointsToScore, nitroBoostCoe;
    private int dangerZoneMultiplier;
    public float timeToInstantiateArrow, timeForGoldCoinToGoToGoldScoreUI = 1, playerDangerYAxisPosition;
    // Start is called before the first frame update
    void Start()
    {


        if (GameObject.Find("GameManager").GetComponent<GameManager>().earlyInTheGame)
            StartCoroutine(InfoArrowInstantiationCor());



    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -9)
        {
            DestroyImmediate(gameObject);
        }
    }


    public int PlayerInDangerZone()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Transform>().position.y >= playerDangerYAxisPosition)
        {
            dangerZoneMultiplier = 3;
        }
        else if (GameObject.FindWithTag("Player").GetComponent<Transform>().position.y < playerDangerYAxisPosition)
        {
            dangerZoneMultiplier = 1;
        }
        return dangerZoneMultiplier;
    }

    public void Response()
    {



        if (isPowerUp)
        {

            // Add score
        }

        if (isGoldCoin && !isTriggered)
        {
            isTriggered = true;
            transform.position = Vector3.Lerp(transform.position, Camera.main.WorldToScreenPoint(GameObject.Find("GoldCoinsText").transform.position), timeForGoldCoinToGoToGoldScoreUI);
            int goldCoins = PlayerPrefs.GetInt("GoldCoins");
            PlayerPrefs.SetInt("GoldCoins", goldCoins + 1);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().GoldCoinGained();
            StartCoroutine(GoldCoinCor());

            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + addPointsToScore * PlayerInDangerZone());
            else if (GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + addPointsToScore * nitroBoostCoe * PlayerInDangerZone());




            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isMagnet)
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + addPointsToScore);
            else if (GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isMagnet)
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + addPointsToScore * nitroBoostCoe);
        }

        if (isRefuel)
        {
            int fuelRefills = PlayerPrefs.GetInt("fuelRefills");
            PlayerPrefs.SetInt("fuelRefills", fuelRefills + 1);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().FuelRefillGained();
        }

        if (isVillain)
        {
            // Add score
        }

        if (isObstacle)
        {
            // Add score
        }
    }

    IEnumerator GoldCoinCor()
    {
        GetComponent<Animator>().SetBool("isTriggered", isTriggered);
        yield return new WaitForSeconds(timeForGoldCoinToGoToGoldScoreUI);
    }






    public GameObject arrowRightSide, arrowLeftSide;
    public Vector3 arrowRightSideOffset, arrowLeftSideOffset;

    IEnumerator InfoArrowInstantiationCor()
    {
        // to instantiat an INFO panel in the beginning of the game for the user to undrstand the purpose of this gameObject
        yield return null;
        /*
        // an Indicator hav to be served to the user in order to giv information on the role of the game object

        yield return new WaitForSeconds(timeToInstantiateArrow);

        if (transform.position.x >= 0)
        {
            Instantiate(arrowLeftSide, transform.position + arrowRightSideOffset, Quaternion.identity, transform);
        }
        if (transform.position.x < 0)
        {
            Instantiate(arrow
        */
    }

}
