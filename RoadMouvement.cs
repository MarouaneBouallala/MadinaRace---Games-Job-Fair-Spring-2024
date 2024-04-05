using UnityEngine;

/// <summary>
/// this serve as a speed regulator if isNitroBoosted 
/// In brief this controls road speed.
/// </summary>
public class RoadMouvement : MonoBehaviour
{
    public float yMinBound, nitroCoe = 1.2f;
    public float speed = 10;
    public Vector3 startPosition;
    public bool isPlaying;
    void Start()
    {
        Application.targetFrameRate = 1000;
    }

    void FixedUpdate()
    {
        isPlaying = GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying;

        if (isPlaying && !GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            GameObject.Find("GameManager").GetComponent<GameManager>().roadSpeed = speed;
            if (transform.position.y < yMinBound)
                transform.position = startPosition;
        }

        if (isPlaying && GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().isNitroBoosted)
        {
            transform.Translate(Vector3.down * speed * GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().roadSpeedIfNitroCoe * Time.deltaTime);
            GameObject.Find("GameManager").GetComponent<GameManager>().roadSpeed = speed * GameObject.FindWithTag("Player").GetComponent<PlayerLogic>().roadSpeedIfNitroCoe;
            if (transform.position.y < yMinBound)
                transform.position = startPosition;
        }

    }
}
