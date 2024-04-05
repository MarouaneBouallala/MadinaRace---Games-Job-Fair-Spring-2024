
using UnityEngine;

public class Instantiators : MonoBehaviour
{
    public GameObject fuel, goldCoin;
    public GameObject[] powerUp, stones;
    public GameObject powerDown, barrier, roadBumper;
    public float instantiationFrequency = 0.75f;
    public int goldLimits = 600;
    // Start is called before the first frame update

    public void InvokeRepeatInstantiating()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying && !GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
        {
            CancelInvoke("GoldCoindAndFuel");
            InvokeRepeating("GoldCoindAndFuel", 0, instantiationFrequency);
        }

        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying && GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
        {
            CancelInvoke("GoldCoindAndFuel");
            InvokeRepeating("GoldCoindAndFuel", 0, instantiationFrequency / 2);
        }
    }


    public void CancelInvokeInstantiation()
    {
        CancelInvoke();
    }

    public void GiveFuel()
    {
        int randomIndex = Random.Range(1, 7);
        if (randomIndex == 1)
        {
            randomIndex = 0;
        }
        if (randomIndex == 2)
        {
            randomIndex = 0;
        }

        if (randomIndex == 5 && PlayerPrefs.GetInt("GoldCoins") > goldLimits)
        {
            Instantiate(powerUp[Random.Range(0, powerUp.Length)], new Vector3(-1.5f, transform.position.y), Quaternion.identity);
            randomIndex = 0;
        }
    }



    void GoldCoindAndFuel()
    {
        int randomIndex = Random.Range(0, 50);

        if (randomIndex != 1)
        {

            if (randomIndex >= 5)
            {
                Instantiate(goldCoin, new Vector3(1.5f, transform.position.y), Quaternion.identity);

                if (randomIndex > 25)
                    Instantiate(goldCoin, new Vector3(-1.5f, transform.position.y), Quaternion.identity);
            }
            if (randomIndex < 5)
            {
                GiveFuel();
            }
        }



        /// STONES
        Vector3 randomnessInPositionOffset = new Vector3(Random.Range(-0.12f, 0.12f), transform.position.y);
        if (randomIndex != 1)
        {

            if (randomIndex == 9 || randomIndex == 11)
            {
                GameObject.Find("Canvas").GetComponent<UIManager>().StoneAdvert();
                Instantiate(stones[Random.Range(0, stones.Length)], new Vector3(-1.5f, transform.position.y) + randomnessInPositionOffset, Quaternion.identity);
                randomIndex = 0;
            }

            if (randomIndex == 7 || randomIndex == 12)
            {
                GameObject.Find("Canvas").GetComponent<UIManager>().StoneAdvert();
                Instantiate(stones[Random.Range(0, stones.Length)], new Vector3(0, transform.position.y) + randomnessInPositionOffset, Quaternion.identity);
                randomIndex = 0;
            }


            if (randomIndex == 10 || randomIndex == 16)
            {
                GameObject.Find("Canvas").GetComponent<UIManager>().StoneAdvert();
                Instantiate(stones[Random.Range(0, stones.Length)], new Vector3(1.5f, transform.position.y) + randomnessInPositionOffset, Quaternion.identity);
                randomIndex = 0;
            }
        }


        /// ROAD BUMPER
        if (randomIndex == 1)
        {
            ///GameObject.Find("Canvas").GetComponent<UIManager>().StoneAdvert(); //
            Instantiate(roadBumper, new Vector3(0, transform.position.y), Quaternion.identity);
        }
    }
}
