using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPrefab : MonoBehaviour
{
    // this script responsible for destroying any gameObject, mostly prefabs, that are passed the player and are not neded anymore.
    void Update()
    {
        if (transform.position.y < -8)
            DestroyImmediate(gameObject);
    }

   
}
