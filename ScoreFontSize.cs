using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The score panel will be scaled up, let's scale the score text with it and let's make keep the score text centred.
/// </summary>
public class ScoreFontSize : MonoBehaviour
{
    public float scaleToFontSizeFactor, sizeDeltaFactor;
    void Update()
    {
        GetComponent<Text>().fontSize = (int)(transform.parent.GetComponent<RectTransform>().rect.width * scaleToFontSizeFactor);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, transform.parent.GetComponent<RectTransform>().rect.width * sizeDeltaFactor);
    }
}
