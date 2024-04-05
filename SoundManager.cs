using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource, ostAudioSource, motorAudioSource;
    public AudioClip fuelClip, goldCoinClip, menuOnClip, menuOffClip, menuChoiceClip, countDownClip,
        goSoundClip, gameOverClip, sliderDragSuccessClip, cachingClip, coinDropClip, bulletHitStoneClip, ostClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ostAudioSource != null)
        if (!ostAudioSource.isPlaying)
        {
            ostAudioSource.Play();
        }
    }

    public void CountDownSound(float pitch)
    {
        if (countDownClip != null)
        {
            audioSource.PlayOneShot(countDownClip);
            audioSource.pitch = pitch;
        }
    }

    public void GoSound()
    {
        if (goSoundClip != null)
        {
            audioSource.PlayOneShot(goSoundClip, 0.75f);
        }
    }


    public void GoldCoinGained()
    {
        if (goldCoinClip != null)
            audioSource.PlayOneShot(goldCoinClip);
    }
    public void FuelRefillGained()
    {
        if (fuelClip != null)
            audioSource.PlayOneShot(fuelClip);
    }

    public void MenuOnSound()
    {
        if (menuOnClip != null)
            audioSource.PlayOneShot(menuOnClip);
    }

    public void MenuOffSound()
    {
        if (menuOffClip != null)
            audioSource.PlayOneShot(menuOffClip);
    }

    public void MenuChoiceSound()
    {
        if (menuChoiceClip != null)
            audioSource.PlayOneShot(menuChoiceClip);
    }

    public void GameOverSound()
    {
        if (gameOverClip != null)
            audioSource.PlayOneShot(gameOverClip);
    }

    public void SliderDragSuccessSound()
    {
        if (sliderDragSuccessClip != null)
            audioSource.PlayOneShot(sliderDragSuccessClip);
    }

    public void CachingSound()
    {
        if (cachingClip != null)
            audioSource.PlayOneShot(cachingClip);
    }

    public void CoinDropSound()
    {
        if (coinDropClip != null)
            audioSource.PlayOneShot(coinDropClip);
    }

    public void BulletHitStoneSound()
    {
        if (bulletHitStoneClip != null)
            audioSource.PlayOneShot(bulletHitStoneClip);
    }



    /// <summary>
    /// OST AUDIO SOURCE
    /// </summary>
    public void PlayOstClip() => ostAudioSource.Play();

    public void PauseOstClip() => ostAudioSource.Pause();

    public void StopOstClip() => ostAudioSource.Stop();

    public void OstClipVolume(float volume)
    {
        ostAudioSource.volume = volume;
    }

    public void OstClipVolumeFadeOut()
    {
        StartCoroutine(OstSourceVolumeFadeOutCor());
    }

    public float fadeDuration = 2;
    IEnumerator OstSourceVolumeFadeOutCor()
    {
        float startVolume = ostAudioSource.volume;
        // Gradually fade out the volume
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            ostAudioSource.volume = Mathf.Lerp(startVolume, 0.1f, timer / fadeDuration);
            yield return null;
        }

        // Ensure the volume is set to 0
        //ostAudioSource.volume = ostAudioSource.volume / 2;
    }

    ////////////////////////////
    ///

    public void PlayMotorClip() => motorAudioSource.Play();

    public void PauseMotorClip() => motorAudioSource.Pause();

    public void StopMotorClip() => motorAudioSource.Stop();

    public void MotorClipVolume(float volume)
    {
        ostAudioSource.volume = volume;
    }

    public void MotorClipVolumeFadeOut()
    {
        StartCoroutine(MotorSourceVolumeFadeOutCor());
    }

    public float motorSoundFadeDuration = 2;
    IEnumerator MotorSourceVolumeFadeOutCor()
    {
        float startVolume = motorAudioSource.volume;
        // Gradually fade out the volume
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            motorAudioSource.volume = Mathf.Lerp(startVolume, 0.1f, timer / fadeDuration);
            yield return null;
        }

    }
}
