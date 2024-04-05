using System.Collections;
using UnityEngine;

public class FuelVineAnimation : MonoBehaviour
{
    public GameObject playerCircle;
    private Vector3 startScale = new Vector3(200, 200, 0), endScale = new Vector3(0, 0, 0);
    public bool isCircleClosed;

    private void Update()
    {
        Transform playerTransform = GameObject.FindWithTag("Player").transform;
        playerCircle.transform.position = playerTransform.position;

    }

    private bool IsCircleClosed()
    {
        bool isCircleClosed = false;
        if (playerCircle.transform.localScale.x <= 0)
        {
            isCircleClosed = true;
        }
        else { isCircleClosed = false; }
        return isCircleClosed;
    }


    public void PlayerCircle(bool isOrderGiven)
    {
            playerCircle.GetComponent<Animator>().SetBool("CloseCircle", isOrderGiven);
    }
}
