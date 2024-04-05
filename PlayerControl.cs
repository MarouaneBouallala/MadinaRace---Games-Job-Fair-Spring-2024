using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    private bool gyroEnabled;
    private Gyroscope gyro;
    public float speed = 1.0f;
    public Text gyroInputInfo;


    void CheckForGyroControl()
    {
        // Check if gyroscope is available
        gyroEnabled = SystemInfo.supportsGyroscope;

        if (gyroEnabled)
        {
            gyro = Input.gyro;
            // Enable the gyroscope
            gyro.enabled = true;
        }
        else
        {
            Debug.Log("Gyroscope not supported.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CheckForGyroControl();
    }

    void Testing()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlaying)
        {
            PlayerMouvement();
            Testing();
        }
    }

    public bool isPlaying = false;

    void PlayerMouvement()
    {
        if (gyroEnabled)
        {
            // Access gyroscope rotation
            Quaternion gyroRotation = gyro.attitude;

            // Convert quaternion rotation to euler angles
            Vector3 eulerRotation = gyroRotation.eulerAngles;

            // Get y rotation
            float yRotation = eulerRotation.y;

            /******************************/

            Vector3 gyroInput = new Vector3(yRotation, transform.position.y, 0f);
            transform.position = gyroInput;
        }
    }
}
