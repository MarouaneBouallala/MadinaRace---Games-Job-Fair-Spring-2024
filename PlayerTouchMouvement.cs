using UnityEngine;

public class PlayerTouchMouvement : MonoBehaviour
{
    private bool isTouching = false;
    public float xAxisLimit_Right, xAxisLimit_Left, yAxisLimit_Top, yAxisLimit_Bottom;

    void Update()
    {
        // Check if there are any touches
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Assuming only one touch for simplicity

            // Check if touch phase is began
            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
            }
            // Check if touch phase is moved and if a touch is currently active
            else if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the movement distance in y-axis
                float deltaY = touch.deltaPosition.y;

                // Update object position in y-axis
                transform.Translate(0f, deltaY * Time.deltaTime, 0f);
            }
            // Check if touch phase is ended
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }

        StayWithinBounds();
    }

    public void StayWithinBounds()
    {
        if (transform.position.y > yAxisLimit_Top)
            transform.position = new Vector3(transform.position.x, yAxisLimit_Top, 0);
        if (transform.position.y < yAxisLimit_Bottom)
            transform.position = new Vector3(transform.position.x, yAxisLimit_Bottom, 0);

        if (transform.position.x > xAxisLimit_Right)
            transform.position = new Vector3(xAxisLimit_Right, transform.position.y, 0);
        if (transform.position.x < xAxisLimit_Left)
            transform.position = new Vector3(xAxisLimit_Left, transform.position.y, 0);
    }
}
