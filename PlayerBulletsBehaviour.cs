using UnityEngine;

/// <summary>
/// Player will be able to shoot hostile lements and Gold coins.
/// </summary>
public struct Coords
{
    public float X { get; }
    public float Y { get; }

    public Coords(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}

public class PlayerBulletsBehaviour : MonoBehaviour
{
    public float speed = 10f;
    public float bulletLife = 1f;

    void Start()
    {
        Coords bulletPosition = new Coords(transform.position.x, transform.position.y);
        Debug.Log($"Bullet position: {bulletPosition}");
        Destroy(gameObject, bulletLife);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "GoldCoin") { }
        if (collision.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>() != null)
        {
            collision.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>().Response();
        }

        if (collision.tag == "Stone")
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().BulletHitStoneSound();
            Destroy(collision.gameObject, 0.1f);

        }

        if (collision.tag == "GoldCoin")
        {
            if (collision.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>() != null)
            {
                collision.gameObject.GetComponent<PowerUpsAndPrizesAndVillainAndObstacles>().Response();
            }
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10);
            // add to score
            Destroy(collision.gameObject, 0.4f);
        }

    }
}
