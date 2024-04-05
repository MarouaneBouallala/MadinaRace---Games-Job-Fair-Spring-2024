using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public bool isShielded, isNitroBoosted, isArmed, isMagnet, isOnAlpha, isShooting;
    public float roadSpeedIfNitroCoe = 1.2f;
    private int shieldOp;

    private RoadMouvement roadScript;
    void Start()
    {
        alphaQuantity = PlayerPrefs.GetInt("alphaQuantity");
        nitroQuantity = PlayerPrefs.GetInt("nitroQuantity");
        armQuantity = PlayerPrefs.GetInt("armQuantity");
        shieldQuantity = PlayerPrefs.GetInt("shieldQuantity");
        magnetQuantity = PlayerPrefs.GetInt("magnetQuantity");
    }
    void Update()
    {

        if (!GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
        {
            CancelInvoke("PlayerShoot");
        }


        if (transform.position.y > 0.99f)
        {
            GameObject.Find("Canvas").GetComponent<UIManager>().BonusZone();
        }

        if (isShielded)
        {

            if (shieldCorDuration > 0)
            {
                shieldCorDuration -= Time.deltaTime;
            }


        }

        if (isNitroBoosted)
        {
            GameObject road = GameObject.FindWithTag("Road");
            if (road != null)
            {
                roadScript = road.GetComponent<RoadMouvement>();
            }

        }
        TheArm();


        if (isMagnet)
        {
            TheMagnet();
        }

        if (isOnAlpha)
        {
            GetComponent<Animator>().SetTrigger("GhostEffect");
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (!isOnAlpha)
        {
            GetComponent<Animator>().SetTrigger("Normal");
            GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    public int alphaQuantity, nitroQuantity, armQuantity, shieldQuantity, magnetQuantity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>() != null)
        {
            other.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>().Response();
        }

        if (other.tag == "Alpha")
        {
            PlayerPrefs.SetInt("alphaQuantity", alphaQuantity + 1);

        }
        if (other.tag == "Shooter")
        {
            PlayerPrefs.SetInt("armQuantity", armQuantity + 1);

        }
        if (other.tag == "Magnet")
        {
            PlayerPrefs.SetInt("magnetQuantity", alphaQuantity + 1);
        }
        if (other.tag == "Nitro")
        {
            PlayerPrefs.SetInt("nitroQuantity", nitroQuantity + 1);
        }
        if (other.tag == "Shield")
        {
            PlayerPrefs.SetInt("shieldQuantity", shieldQuantity + 1);
        }

        if (other.tag == "Stone" || other.tag == "RoadBumper")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            GameObject.Find("Canvas").GetComponent<UIManager>().AccidentPanel(other.bounds.center);
        }
    }

    public float shieldCorDuration, nitroCorDuration, armCorDuration, magnetCorDuration, ghostCorDuration;

    public void ShieldMode()
    {
        StartCoroutine(ShieldCor());
    }
    IEnumerator ShieldCor()
    {
        isShielded = true;
        yield return new WaitForSeconds(shieldCorDuration);
        isShielded = false;
        shieldCorDuration = 10;
    }


    public void NitroMode()
    {
        StartCoroutine(NitroCor());
    }
    IEnumerator NitroCor()
    {
        isNitroBoosted = true;
        yield return new WaitForSeconds(nitroCorDuration);
        isNitroBoosted = false;
    }

    public void ArmedMode()
    {
        StartCoroutine(ArmCor());
    }
    IEnumerator ArmCor()
    {
        isArmed = true;
        yield return new WaitForSeconds(armCorDuration);
        isArmed = false;
    }

    public void MagnetMode()
    {
        StartCoroutine(MagnetCor());
    }
    IEnumerator MagnetCor()
    {
        isMagnet = true;
        yield return new WaitForSeconds(magnetCorDuration);
        isMagnet = false;
    }

    public void GhostMode()
    {
        StartCoroutine(GhostCor());
    }
    IEnumerator GhostCor()
    {
        isOnAlpha = true;
        yield return new WaitForSeconds(ghostCorDuration);
        isOnAlpha = false;
    }

    public float shootingFrecuency = 1f;
    public void TheArm()
    {
            if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

        if (isArmed && !isShooting && GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
        {
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {

                InvokeRepeating("PlayerShoot", 0.1f, shootingFrecuency);
                isShooting = true;
            }
        }

        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            CancelInvoke("PlayerShoot");
            isShooting = false;
        }
        }
    }



    public GameObject bullet;
    private void PlayerShoot()
    {
        isShooting = true;
        Instantiate(bullet, transform.position, Quaternion.identity);
    }


    public string[] powerUpTags;
    public float xDistance = 10f;
    public float speed = 2f;

    public void TheMagnet()
    {
        foreach (var tag in powerUpTags)
        {
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(tag);

            foreach (var obj in objArray)
            {
                float distance = Vector3.Distance(obj.transform.position, transform.position);

                if (distance <= xDistance)
                {
                    Vector3 direction = (transform.position - obj.transform.position).normalized;
                    obj.transform.position += direction * speed * Time.deltaTime;
                }
            }
        }
    }



}

