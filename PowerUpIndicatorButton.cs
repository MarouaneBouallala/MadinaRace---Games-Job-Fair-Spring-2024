using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Power ups buttons UI funcationality
/// </summary>
public class PowerUpIndicatorButton : MonoBehaviour, IPointerDownHandler
{
    public enum powerUpName { alpha, nitro, arm, shield, magnet };
    public powerUpName powerUp;
    private GameObject player;
    private UIManager uiManager;
    public int alphaQuantity, nitroQuantity, armQuantity, shieldQuantity, magnetQuantity;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (powerUp == powerUpName.alpha && alphaQuantity > 0)
        {
            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isOnAlpha)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().GhostMode();
                PlayerPrefs.SetInt("alphaQuantity", PlayerPrefs.GetInt("alphaQuantity") - 1);
                return;
            }

        }
        if (powerUp == powerUpName.nitro && nitroQuantity > 0)
        {
            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().NitroMode();
                // play nitro button animation
                GameObject.Find("Canvas").GetComponent<UIManager>().NitroAnimation();
                // play nitro screen animation
                PlayerPrefs.SetInt("nitroQuantity", PlayerPrefs.GetInt("nitroQuantity") - 1);
                return;
            }
        }
        if (powerUp == powerUpName.arm && armQuantity > 0)
        {
            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isArmed)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().ArmedMode();
                PlayerPrefs.SetInt("armQuantity", PlayerPrefs.GetInt("armQuantity") - 1);
                return;
            }

        }
        if (powerUp == powerUpName.shield && PlayerPrefs.GetInt("shieldQuantity") > 0)
        {
            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isShielded)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().ShieldMode();
                GameObject.Find("Canvas").GetComponent<UIManager>().ShieldAnimation();
                PlayerPrefs.SetInt("shieldQuantity", PlayerPrefs.GetInt("shieldQuantity") - 1);
            }


        }
        if (powerUp == powerUpName.magnet && magnetQuantity > 0)
        {
            if (!GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isMagnet)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().MagnetMode();
                PlayerPrefs.SetInt("magnetQuantity", PlayerPrefs.GetInt("magnetQuantity") - 1);
                return;
            }


        }
    }

    // Start is called before the first frame update
    void Start()
    {
        alphaQuantity = PlayerPrefs.GetInt("alphaQuantity");
        nitroQuantity = PlayerPrefs.GetInt("nitroQuantity");
        armQuantity = PlayerPrefs.GetInt("armQuantity");
        shieldQuantity = PlayerPrefs.GetInt("shieldQuantity");
        magnetQuantity = PlayerPrefs.GetInt("magnetQuantity");

        PlayerPrefs.SetInt("alphaQuantity", 5);
        PlayerPrefs.SetInt("nitroQuantity", 5);
        PlayerPrefs.SetInt("armQuantity", 5);
        PlayerPrefs.SetInt("shieldQuantity", 5);
        PlayerPrefs.SetInt("magnetQuantity", 5);

    }
}
