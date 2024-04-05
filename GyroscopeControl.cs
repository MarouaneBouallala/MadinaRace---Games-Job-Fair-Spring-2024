using UnityEngine;

public class GyroscopeControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    public float mouvementCoeficient;

    void Start()
    {
        // Check if gyroscope is available
        gyroEnabled = SystemInfo.supportsGyroscope;

        if (gyroEnabled)
        {
            // Enable the gyroscope
            gyro = Input.gyro;
            gyro.enabled = true;
        }
        else
        {
            return;
        }
    }

    void Update()
    {
        if (gyroEnabled)
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
            {
                // Get gyroscope input
                Vector3 rotationRate = gyro.rotationRateUnbiased;

                // Check if the device is in portrait mode
                if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
                {
                    // Move the game object along the X-axis based on gyroscope input on the Y-axis
                    transform.position += new Vector3(rotationRate.y * mouvementCoeficient, 0f, 0f) * Time.deltaTime;
                }
                else
                {
                    // Device is not in portrait mode, handle accordingly
                    Debug.Log("Device is not in portrait mode.");
                }
            }
    }
}
