using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// this is under testing.
/// For main menu functionality
/// </summary>
public class LevelManager : MonoBehaviour
{
    public int firstLevelIndexNumber;
    public float intervalToFirstLevel;
    // Start is called before the first frame update

    public GameObject tukTuk, car, semiTruck;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;


        Screen.sleepTimeout = SleepTimeout.SystemSetting;

        // Load Ad

        string playerVehicle = PlayerPrefs.GetString(tag);


        if (playerVehicle == "Car" && car != null)
        {
            Instantiate(car, new Vector3(0, -2f), Quaternion.identity);
        }

        if (playerVehicle == "TukTuk" && tukTuk != null)
        {
            Instantiate(tukTuk, new Vector3(0, -2f), Quaternion.identity);
        }

        if (playerVehicle == "Truck" && semiTruck != null)
        {
            Instantiate(semiTruck, new Vector3(0, -2f), Quaternion.identity);
        }

        if (aboutText != null)
            fullText = aboutText.GetComponent<Text>().text;
    }

    public void RepeatSameLevel()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(actualScene.buildIndex);
    }

    public void ReplayAfterLoss()
    {
        StartCoroutine(ReplayAfterLossCor());
    }

    public GameObject fadeOutToBlack, gameOverByAccidentPanel;
    public float fadeDuration = 2f; // Duration over which to fade out
    IEnumerator ReplayAfterLossCor()
    {
        
        float startVolume = PlayerPrefs.GetFloat("AudioListenerVolume"); // Store the initial volume

        // Gradually fade out the volume
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        // fade out to black

        fadeOutToBlack.SetActive(true);
        yield return new WaitForSeconds(.5f);
        gameOverByAccidentPanel.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        RepeatSameLevel();
    }


    // Update is called once per frame
    void Update()
    {
        BackToMainMenuSceneFromLevel();
    }



    public GameObject aboutText, SettingMenuElement_Vehicle, chooseCarError;
    public void AboutSectionToggle()
    {
        if (aboutText != null && SettingMenuElement_Vehicle != null)
        {

            // aboutText.SetActive(!aboutText.activeSelf);
            SettingMenuElement_Vehicle.SetActive(false);

            if (!aboutText.activeSelf)
            {
                Debug.Log("Activate about, Start Writing.");
                aboutText.SetActive(true);
                //StartCoroutine(TypeWriteEffect());
            }
            else if (aboutText.activeSelf)
            {
                Debug.Log("Deactivate abour section, Stop Writing.");
                //aboutTextText.text = " ";
                //StopCoroutine(TypeWriteEffect());
                aboutText.SetActive(false);
            }
        }
    }

    public float textSpeed = 0.1f;
    public Text aboutTextText;
    private string fullText;
    IEnumerator AboutSectionTypeWriterEffect()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            aboutTextText.text += fullText[i];
            yield return new WaitForSeconds(5f);
        }
    }


    IEnumerator TypeWriteEffect()
    {
        fullText = "About About About About About About About ";
        for (int i = 0; i < fullText.Length; i++)
        {
            char currentChar = fullText[i];
            aboutTextText.text += currentChar;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public AudioClip startGameButton;
    public AudioSource audioSource;


    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().pauseMenu.SetActive(false);
    }


    public void GoToFirstLevel()
    {

        if (PlayerPrefs.HasKey(tag))
        {
            audioSource.PlayOneShot(startGameButton, 1f);
            StartCoroutine(LoadSceneAsyncCoroutine());
        }
        else if (!PlayerPrefs.HasKey(tag))
        {
            StartCoroutine(ChooseVehErrorCor());
        }
    }

    IEnumerator ChooseVehErrorCor()
    {
        if (chooseCarError)
        {
            chooseCarError.SetActive(true);
            yield return new WaitForSeconds(1f);
            chooseCarError.SetActive(false);
        }
    }

    public void BackToMainMenuSceneFromLevel()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            int actualScene = SceneManager.GetActiveScene().buildIndex;

            if (actualScene == 0)
            {
                // popup : do you want to quit game
                Application.Quit();
            }
        }

    }



    public string sceneNameToLoad;
    public Image loadingBar;

    private IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Reset loading bar fill amount to 0
        loadingBar.fillAmount = 0f;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            // Update the loading progress on the loading bar
            loadingBar.fillAmount = asyncLoad.progress;

            yield return null;
        }

        // Ensure loading bar is completely filled
        if (asyncLoad.isDone)
            loadingBar.fillAmount = 1f;

        yield return new WaitForSeconds(1f);
    }



}
