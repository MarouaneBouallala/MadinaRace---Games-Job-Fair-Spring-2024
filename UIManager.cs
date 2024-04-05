using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject fuelIndicator, scoreAnimatedPanel, gameOverPanel, accidentPanel, stoneInTheWayAdvert, bonusZoneScore, bonusZoneRedArea;
    public Text fuelRefillsText, scoreText, goldCoinsText, goldCoinsTextShadow, topScoreText;
    public bool sliderIsSuccessful = false;
    public GameObject[] counterImage;
    public float iterationInterval, minDelay = 20f, maxDelay = 40f, refuelSliderAppearanceDuration = 10f;
    public GameObject shieldOpportunities;
    public Image newShield, oldShield;

    public void StartCounter()
    {
        StartCoroutine(IterateCounterSprites());
    }
    private IEnumerator IterateCounterSprites()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().CountDownSound(1f);
        counterImage[0].SetActive(true);
        yield return new WaitForSeconds(iterationInterval);
        counterImage[0].SetActive(false);
        yield return new WaitForSeconds(iterationInterval);

        GameObject.Find("SoundManager").GetComponent<SoundManager>().CountDownSound(1.4f);
        counterImage[1].SetActive(true);
        yield return new WaitForSeconds(iterationInterval);
        counterImage[1].SetActive(false);
        yield return new WaitForSeconds(iterationInterval);

        GameObject.Find("SoundManager").GetComponent<SoundManager>().CountDownSound(1.6f);
        counterImage[2].SetActive(true);
        yield return new WaitForSeconds(iterationInterval);
        counterImage[2].SetActive(false);
        yield return new WaitForSeconds(iterationInterval);

        GameObject.Find("SoundManager").GetComponent<SoundManager>().GoSound();
        counterImage[3].SetActive(true);
        yield return new WaitForSeconds(iterationInterval);
        counterImage[3].SetActive(false);
        yield return new WaitForSeconds(iterationInterval);


        InvokeRepeating("AnimateScorePanel", Random.Range(minDelay, maxDelay), Random.Range(minDelay, maxDelay));
    }

    public void BonusZone()
    {
        StartCoroutine(BonusZoneCor());
    }

    IEnumerator BonusZoneCor()
    {
        bonusZoneScore.SetActive(true);
        yield return new WaitForSeconds(1f);
        bonusZoneRedArea.SetActive(true);
        yield return new WaitForSeconds(2f);
        bonusZoneScore.SetActive(false);
        yield return new WaitForSeconds(1f);
        bonusZoneRedArea.SetActive(false);
    }

    void Update()
    {
        fuelRefillsText.text = PlayerPrefs.GetInt("fuelRefills").ToString();
        goldCoinsText.text = PlayerPrefs.GetInt("GoldCoins").ToString();
        goldCoinsTextShadow.text = PlayerPrefs.GetInt("GoldCoins").ToString();

        UpdateScore();

        PowerUpsAvailability();
        ShieldAnimation();
        MagnetAnimation();
        IndicatorsQuantityUpdateText();


        if (!GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
        {
            bonusZoneScore.SetActive(false);
            bonusZoneRedArea.SetActive(false);
        }
    }

    public float fuelMod = 180f;
    public void UpdateFuelIndicator(float fuel)
    {
        if (fuelIndicator != null)
        {
            fuelIndicator.transform.rotation = Quaternion.Euler(0f, 0f, fuel);
            fuelIndicator.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(fuel, -45f, 45f));
        }
    }

    void UpdateScore()
    {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        topScoreText.text = PlayerPrefs.GetInt("TopScore").ToString();
    }

    public void StoneAdvert()
    {
        StartCoroutine(StoneAdvertCor());
    }

    public float advertWarningFrecuency = 0.5f;
    IEnumerator StoneAdvertCor()
    {
        stoneInTheWayAdvert.SetActive(true);
        yield return new WaitForSeconds(advertWarningFrecuency);
        stoneInTheWayAdvert.SetActive(false);
        yield return new WaitForSeconds(advertWarningFrecuency);
        stoneInTheWayAdvert.SetActive(true);
        yield return new WaitForSeconds(advertWarningFrecuency);
        stoneInTheWayAdvert.SetActive(false);
    }

    public void AnimateScorePanel()
    {
        scoreAnimatedPanel.GetComponent<Animator>().SetTrigger("ScoreBoardAnimation");
    }

    public void ShowRefuelSlider()
    {
        StartCoroutine(ShowRefillSlider());
    }

    IEnumerator FuelIndicatorBlink()
    {
        fuelIndicator.SetActive(false);
        yield return new WaitForSeconds(1f);
        fuelIndicator.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FuelIndicatorBlink());
    }

    public Slider refuelSlider;
    public GameObject refuelSliderParent;

    public void SliderRefuel()
    {
        if (refuelSlider != null)
        {
            if (refuelSlider.value > 0.84f)
            {
                int fuelRefills = PlayerPrefs.GetInt("fuelRefills");
                PlayerPrefs.SetInt("fuelRefills", fuelRefills - 1);
                StartCoroutine(SliderDraggingSuccessCor());
                GameObject.FindWithTag("Player").GetComponent<FuelConsumption>().fuelLeft = 45f;
            }
        }
    }



    IEnumerator SliderDraggingSuccessCor()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SliderDragSuccessSound();
        GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying = true;
        refuelSlider.interactable = false;
        yield return new WaitForSeconds(0.75f);
        refuelSliderParent.SetActive(false);
        GameObject.FindWithTag("Player").GetComponent<FuelConsumption>().isNeedingRefuel = false;
    }

    IEnumerator ShowRefillSlider()
    {
        yield return new WaitForSeconds(0.5f);
        refuelSliderParent.SetActive(true);
        refuelSlider.interactable = true;
        refuelSlider.value = 0f;
        yield return new WaitForSeconds(refuelSliderAppearanceDuration);
        refuelSliderParent.SetActive(false);
    }

    public GameObject player, armIndicator, shieldIndicator, ghostIndicator, nitroIndicator, magnetIndicator;
    private float fillAmount;
    public void Indicators()
    {
        PlayerLogic playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();

        if (playerLogic.isArmed)
        {
            Color armColor = armIndicator.GetComponent<Image>().color;
            armIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else
        {
            Color armColor = armIndicator.GetComponent<Image>().color;
            armIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }


        if (playerLogic.isShielded)
        {
            Color shieldColor = shieldIndicator.GetComponent<Image>().color;
            shieldIndicator.GetComponent<Image>().color = new Color(shieldColor.r, shieldColor.g, shieldColor.b, 1f);
        }
        else
        {
            Color shieldColor = shieldIndicator.GetComponent<Image>().color;
            shieldIndicator.GetComponent<Image>().color = new Color(shieldColor.r, shieldColor.g, shieldColor.b, 0.2f);
        }


        if (playerLogic.isNitroBoosted)
        {
            Color nitroColor = nitroIndicator.GetComponent<Image>().color;
            nitroIndicator.GetComponent<Image>().color = new Color(nitroColor.r, nitroColor.g, nitroColor.b, 1f);
        }
        else
        {
            Color nitroColor = nitroIndicator.GetComponent<Image>().color;
            nitroIndicator.GetComponent<Image>().color = new Color(nitroColor.r, nitroColor.g, nitroColor.b, 0.5f);
        }

        if (playerLogic.isMagnet)
        {
            Color magnetColor = magnetIndicator.GetComponent<Image>().color;
            magnetIndicator.GetComponent<Image>().color = new Color(magnetColor.r, magnetColor.g, magnetColor.b, 0.5f);
        }

        if (!playerLogic.isOnAlpha)
        {
            Color ghostColor = ghostIndicator.GetComponent<Image>().color;
            ghostIndicator.GetComponent<Image>().color = new Color(ghostColor.r, ghostColor.g, ghostColor.b, 0.5f);
        }



    }

    public GameObject boomPanel;
    public void AccidentPanel(Vector3 collisionPos)
    {
        //accidentPanel.SetActive(true);
        Instantiate(boomPanel, collisionPos, Quaternion.identity);
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        CancelInvoke("AnimateScorePanel");
    }

    public void PowerUpsAvailability()
    {
        if (PlayerPrefs.GetInt("alphaQuantity") > 0)
        {
            Color armColor = ghostIndicator.GetComponent<Image>().color;
            ghostIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else
        {
            Color armColor = ghostIndicator.GetComponent<Image>().color;
            ghostIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }


        if (PlayerPrefs.GetInt("nitroQuantity") > 0)
        {
            Color armColor = nitroIndicator.GetComponent<Image>().color;
            nitroIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else
        {
            Color armColor = nitroIndicator.GetComponent<Image>().color;
            nitroIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }


        if (PlayerPrefs.GetInt("armQuantity") > 0)
        {
            Color armColor = armIndicator.GetComponent<Image>().color;
            armIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else
        {
            Color armColor = armIndicator.GetComponent<Image>().color;
            armIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }


        if (PlayerPrefs.GetInt("shieldQuantity") > 0)
        {
            Color armColor = shieldIndicator.GetComponent<Image>().color;
            shieldIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else if (PlayerPrefs.GetInt("shieldQuantity") == 0 && !GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isShielded)
        {
            Color armColor = shieldIndicator.GetComponent<Image>().color;
            shieldIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }

        if (PlayerPrefs.GetInt("magnetQuantity") > 0)
        {
            Color armColor = magnetIndicator.GetComponent<Image>().color;
            magnetIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 1f);
        }
        else
        {
            Color armColor = magnetIndicator.GetComponent<Image>().color;
            magnetIndicator.GetComponent<Image>().color = new Color(armColor.r, armColor.g, armColor.b, 0.2f);
        }
    }



    /// ANIMATIONs
    /// 
    [Header("* Power On Adverts *")]
    public GameObject nitroAnimationPrefab;
    public void NitroAnimation()
    {
        Animator animator = nitroIndicator.transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("AnimateNitro");

        Instantiate(nitroAnimationPrefab, transform);
    }

    public void ShieldAnimation()
    {
        PlayerLogic playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        Image shieldImage = shieldIndicator.GetComponent<Image>();

        if (!playerLogic.isShielded)
        {
            shieldImage.fillAmount = 1f; // Shield is not active, fillAmount is 1
        }
        else if (playerLogic.isShielded)
        {
            shieldImage.fillAmount -= Time.deltaTime / 10;
            shieldImage.fillAmount = Mathf.Max(0f, shieldImage.fillAmount);
        }
    }

    public void MagnetAnimation()
    {
        magnetIndicator.GetComponent<Animator>().SetBool("isMagnet", GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isMagnet);
    }

    public Text magnetIndicatorText, ghostIndicatorText, shieldIndicatorText, fireBallIndicatorText, nitroIndicatorText;
    public void IndicatorsQuantityUpdateText()
    {
        magnetIndicatorText.text = PlayerPrefs.GetInt("magnetQuantity").ToString();
        ghostIndicatorText.text = PlayerPrefs.GetInt("alphaQuantity").ToString();
        shieldIndicatorText.text = PlayerPrefs.GetInt("shieldQuantity").ToString();
        fireBallIndicatorText.text = PlayerPrefs.GetInt("armQuantity").ToString();
        nitroIndicatorText.text = PlayerPrefs.GetInt("nitroQuantity").ToString();
    }
}

