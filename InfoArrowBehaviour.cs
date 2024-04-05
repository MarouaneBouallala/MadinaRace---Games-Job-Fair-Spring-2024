using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoArrowBehaviour : MonoBehaviour
{
    public float minimumDistanceFromThePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        if (Vector3.Distance(this.transform.position, GameObject.FindWithTag("Player").transform.position) < minimumDistanceFromThePlayer)
        {
            DestroyImmediate(gameObject);
        }

        //ClampToScreenBounds();
    }

    void ClampToScreenBounds()
    {
        if (transform.position.x < -1)
        {
            
        }
        if (transform.position.x > 1)
        {

        }
    }
}
