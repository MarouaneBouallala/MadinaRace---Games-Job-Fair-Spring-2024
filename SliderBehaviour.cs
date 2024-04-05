using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Th slider will color lerp beased on it's value.
/// </summary>
public class SliderBehaviour : MonoBehaviour
{
    private Animator anim;
    public Image fillImage;
    public Color colorStart, colorEnd;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       fillImage.color = Color.Lerp(colorStart, colorEnd, GetComponent<Slider>().value);
    }

    public void HaltAnimation()
    {
        anim.enabled = false;
    }

    
}
