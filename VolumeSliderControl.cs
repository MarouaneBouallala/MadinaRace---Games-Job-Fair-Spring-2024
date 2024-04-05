using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VolumeSliderControl : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 touchStartPosition, touchEndPosition, touchDelta;
    public float mouvementSpeed = 0.1f;
    public Text soundLevelFigures;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("AudioListenerVolume", slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeVolume)
        SliderMouvement();

        soundLevelFigures.text = (AudioListener.volume * 100).ToString("F0") + "%";

    }

    public GameObject volumeSlider;
    public bool canChangeVolume;

    public void OnPointerDown(PointerEventData eventData)
    {
        canChangeVolume = true;
        volumeSlider.SetActive(canChangeVolume);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canChangeVolume = false;
        volumeSlider.SetActive(canChangeVolume);
    }

    private void TouchMouvement()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    touchEndPosition = touch.position;
                    touchDelta = touchEndPosition - touchStartPosition;
                    transform.Translate(touchDelta * mouvementSpeed * Time.deltaTime);
                    break;
            }
        }
    }

    public Slider slider;
    public RectTransform sliderHandle;

    void SliderMouvement()
    {
        // Get the pointer position in screen space
        Vector2 mousePos = Input.mousePosition;
        // Convert the screen space position to local position relative to the slider's RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(slider.GetComponent<RectTransform>(), mousePos, null, out Vector2 localPoint);

        // Map the x-position of the pointer to the slider value range
        float sliderValue = Mathf.InverseLerp(slider.GetComponent<RectTransform>().rect.xMin, slider.GetComponent<RectTransform>().rect.xMax, localPoint.x);
        slider.value = sliderValue;
        PlayerPrefs.SetFloat("AudioListenerVolume", sliderValue);
        AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume");
    }
}
