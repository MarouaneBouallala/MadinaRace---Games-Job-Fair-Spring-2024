using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This ais a beta script, (Not Implemented yet).
/// </summary>
public class MusicBox : MonoBehaviour
{
    public Slider songsSlider, volumeSlider;
    public AudioSource audioSource, audioSource1, audioSource2, audioSource3, audioSource4;
    public AudioClip ost1, ost2, ost3, ost4;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (songsSlider != null)
        {
            if (songsSlider.value == 0)
                audioSource.PlayOneShot(ost1, 1);
            if (songsSlider.value == 1)
                audioSource.PlayOneShot(ost2, 1);
            if (songsSlider.value == 2)
                audioSource.PlayOneShot(ost3, 1);
            if (songsSlider.value == 3)
                audioSource.PlayOneShot(ost4, 1);
        }


    }

    void PlayRadioStyle()
    {
        //  channel

        // 1
        audioSource1.volume = 1 - songsSlider.value;

        // 2
        audioSource2.volume = 2 - songsSlider.value;

        // 3
        audioSource3.volume = 3 - songsSlider.value;

        // 4
        audioSource4.volume = 4 - songsSlider.value;

    }

    }

