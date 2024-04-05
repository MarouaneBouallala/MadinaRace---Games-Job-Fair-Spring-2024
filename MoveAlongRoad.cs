using UnityEngine;

// this cript is for gameObjects to move along road.
public class MoveAlongRoad : MonoBehaviour
{
    private RoadMouvement roadScript;
    private GameManager gameManager;
    public float speed;
    void Start()
    {
        GameObject road = GameObject.FindWithTag("Road");
        if (road != null)
        {
            roadScript = road.GetComponent<RoadMouvement>();
        }

        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        speed = gameManager.RoadSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * gameManager.RoadSpeed() * Time.deltaTime);
    }
}
