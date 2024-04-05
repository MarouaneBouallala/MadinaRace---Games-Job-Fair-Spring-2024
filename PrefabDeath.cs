using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDeath : MonoBehaviour
{
    public float deathInterval = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deathInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
