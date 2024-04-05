using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I like to keep score on an indivdual gameObject.
/// </summary>
public class ScoreKeeper : MonoBehaviour
{
    public int score;

    public void UpdateScore(int newPoints)
    {
        score = PlayerPrefs.GetInt("Score");
        score += newPoints;
        PlayerPrefs.SetInt("Score", score);
    }
}
