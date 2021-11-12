using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMvt : MonoBehaviour
{
    // Start is called before the first frame update
    private bool gyroEnabled;
    private Gyroscope gyroscope;
    public float speed;
    private Rigidbody rb;
    private bool objectIsMoving = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        gyroEnabled = EnableGyro();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acc= Input.acceleration;
        Vector3 position = rb.transform.position;

        if (rb.transform.position.z > 10 || rb.transform.position.y < -10)
        {
            objectIsMoving = false;
        }
            if (acc.z > .6 && !objectIsMoving)
        {
            objectIsMoving = true;
            rb.AddForce(transform.forward * speed * 2700 * Time.deltaTime);
            SoundManager.playBallSound();
        }
        //Debug.Log(position.x);
        position.x = Mathf.Clamp(position.x, -1f, 1f);

        //Debug.Log(gyroscope.attitude.eulerAngles.z);
       /*if (gyroscope.attitude.eulerAngles.z < 70)
        {
            //rb.transform.Translate(2 * Time.deltaTime, 0, 0);
            rb.AddForce(10 * Time.deltaTime, 0, 0);
        }
        if (gyroscope.attitude.eulerAngles.z > 120)
        {
            //rb.transform.Translate(-2 * Time.deltaTime, 0, 0);
            rb.AddForce(-10 * Time.deltaTime, 0, 0);
        }*/


        /*if (rb.transform.position.x < -8 )
        {
            rb.transform.position = new Vector3(-8, rb.transform.position.y, rb.transform.position.z);   
        }

        if(rb.transform.position.x > -1)
        {
            rb.transform.position = new Vector3(-1, rb.transform.position.y, rb.transform.position.z);   
        }*/

        //SoundManager.playBallSound();
        /*if(Input.GetKeyUp(KeyCode.Return))
        {
            rb.AddForce(0, 0, speed * 100000 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.transform.Translate(2 * Time.deltaTime, 0, 0);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pin")
        {
            Handheld.Vibrate();
            SoundManager.playStrikeSound();
            objectIsMoving = false;
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            return true;
        }
        return false;
    }
}
