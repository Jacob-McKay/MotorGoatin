using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Transform udder_base;
    public Text gyroEnabledText;
    public Text gyroReadText;
    public Text accelReadText;
    public Text scoreText;
    public float speed = 1;
    public Button playbutton;
    public float scoreSmoothing = 10;

    public int pointsTilNextBleet = 10;

    private float rawProgress = 0; 
    private int points = 0; 
    private bool motorGoatinInProgress = false;
    private AudioSource goatBleet1;
    private AudioSource goatBleet2;

	// Use this for initialization
	void Start () {
        goatBleet1 = gameObject.GetComponents<AudioSource>()[0];
        goatBleet2 = gameObject.GetComponents<AudioSource>()[1];
    }

    // Update is called once per frame
    void Update () {
        if (motorGoatinInProgress)
        {
            Debug.Log("Is anything happening?");
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("JEAHHHHH BOIIII");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                }
            }

            //float xDelta = Input.GetAxis("Mouse X");
            //float yDelta = Input.GetAxis("Mouse Y");

            //udder_base.rotation = Quaternion.Euler(udder_base.eulerAngles + new Vector3(yDelta, 0, xDelta));
            Input.gyro.enabled = true;
            if (Input.gyro.enabled)
            {
                gyroEnabledText.text = "YUP GYRO IS ONNN";
                var gyroRotationRate = Input.gyro.rotationRateUnbiased;
                rawProgress += (Mathf.Abs(gyroRotationRate.x) + Mathf.Abs(gyroRotationRate.y) + Mathf.Abs(gyroRotationRate.z));
                points = (int) Mathf.Floor(rawProgress / scoreSmoothing);
                if (points >= pointsTilNextBleet)
                {
                    if (Mathf.RoundToInt(Random.value) == 0)
                    {
                        goatBleet1.Play();
                    } else
                    {
                        goatBleet2.Play();
                    }
                    pointsTilNextBleet += 10;
                }
                scoreText.text = points.ToString();
                gyroReadText.text = gyroRotationRate.ToString();
                udder_base.rotation = Quaternion.Euler(udder_base.eulerAngles + new Vector3(gyroRotationRate.x, gyroRotationRate.z, gyroRotationRate.y));
            }
            else
            {
                gyroEnabledText.text = "GYRO IS OFF :,(";
            }
        }

        //var acceleration = Input.acceleration;
        //accelReadText.text = acceleration.ToString();

        //// find speed based on delta
        //float curSpeed = Time.deltaTime * speed;
        //var currentRotation = udder_base.rotation.eulerAngles;
        //// first update the current rotation angles with input from acceleration axis
        //currentRotation.y += Input.acceleration.x * curSpeed;
        //currentRotation.x += Input.acceleration.y * curSpeed;

        //// then rotate this object accordingly to the new angle
        //udder_base.Rotate(currentRotation);
    }

    public void CommenceMotorGoatin()
    {
        motorGoatinInProgress = true;
        playbutton.gameObject.SetActive(false);
    }
}
