using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float timeBeforePlayStart = 5f;
    public float roadSpeed, earlyTime = 10f, startTime;
    public bool earlyInTheGame = true, justReachedTopScore;

    void Start()
    {
        Application.targetFrameRate = 120;
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume");
        StartGame();
    }

    void Update()

    {
        // this will serve to give player incentives at an early time.
        if (earlyInTheGame)
        {
            earlyTime -= Time.deltaTime;
            if (earlyTime < 0f)
                earlyInTheGame = false;
        }


        // Update the top score numbers as soon as the score exceeds highest recorded score.
        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("TopScore"))
        {
            // Set the actual score as top score
            PlayerPrefs.SetInt("TopScore", PlayerPrefs.GetInt("Score"));

            if (!justReachedTopScore)
                // Celebrate the new high score with some effects on the "high score" board for a determined duration of 15f (Check the Coroutine).
                StartCoroutine(OnTopScoredCor());
        }


        ShowPauseMenu();
    }
    IEnumerator OnTopScoredCor()
    {
        justReachedTopScore = true;
        GameObject.Find("TopScoreMask").GetComponent<Animator>().SetTrigger("ReachedTopScore");
        yield return new WaitForSeconds(15f);
        justReachedTopScore = false;
        GameObject.Find("TopScoreMask").GetComponent<Animator>().SetTrigger("Idle");
    }
    public float RoadSpeed()
    {
        // this stops the road and based on isPlaying (isPlaying will stop instantiations throughout the game)
        if (isPlaying)
        {
            return roadSpeed;
        }
        else if (!isPlaying)
        {
            roadSpeed = 0;
        }
        return roadSpeed;
    }

    void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    IEnumerator StartGameCor()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Canvas").GetComponent<UIManager>().StartCounter();
        yield return new WaitForSeconds(timeBeforePlayStart);
        isPlaying = true;
        if (GameObject.FindWithTag("PrizeInstantiator") != null)
            GameObject.FindWithTag("PrizeInstantiator").GetComponent<Instantiators>().InvokeRepeatInstantiating();
    }

    public GameObject pauseMenu;
    public float fadeDuration = 2f;
    public void ShowPauseMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
            pauseMenu.SetActive(true);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().MenuOnSound();
            GameObject.Find("SoundManager").GetComponent<SoundManager>().OstClipVolume(0.2f);
            GameObject.Find("SoundManager").GetComponent<SoundManager>().MotorClipVolume(0.4f);
        }
    }

    IEnumerator VolumeFadeOut()
    {
        // In case a smooth sound transition is needed
        float startVolume = AudioListener.volume;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }
        AudioListener.volume = AudioListener.volume / 2;

    }

    public void HidePauseMenu()
    {
        ResumeGame();
        pauseMenu.SetActive(false);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().MenuOffSound();
        GameObject.Find("SoundManager").GetComponent<SoundManager>().OstClipVolume(1);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().MotorClipVolume(1);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        isPlaying = false;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        isPlaying = true;
    }


    public bool isPlaying;

    public void GameOver()
    {
        isPlaying = false;
        GameObject.FindWithTag("PrizeInstantiator").GetComponent<Instantiators>().CancelInvokeInstantiation();
        StartCoroutine(GameOverCor());
    }


    IEnumerator GameOverCor()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().OstClipVolumeFadeOut();
        yield return new WaitForSeconds(1f);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().MotorClipVolumeFadeOut();
        if (PlayerPrefs.GetString("isViberEnabled") == "True")
        {
            Handheld.Vibrate();
        }
        yield return new WaitForSeconds(2f);
        GameObject.Find("SoundManager").GetComponent<SoundManager>().GameOverSound();
        // this can be moved to UIManager
        GameObject.Find("OutOfFuelVine").GetComponent<FuelVineAnimation>().PlayerCircle(true);
        GameObject.Find("Canvas").GetComponent<UIManager>().GameOverPanel();
        yield return new WaitForSeconds(16f);
        // w can move on to another scene (not playable scene)
        //SceneManager.LoadScene(2);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}