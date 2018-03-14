using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TrailRenderer ray;
    private TrailRenderer myray;
    


    // Use this for initialization
    public float speed = 0.2f;
    public float horizontalSpeed = 100.0F;
    public float verticalSpeed = 100.0F;
    float z = 0;
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

     
        if (moveVertical > 0 ||  moveHorizontal > 0)
        {
            transform.position = transform.position + transform.forward * speed;

        }

        if (Input.GetKeyDown("space"))
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            myray = Instantiate<TrailRenderer>(ray);
            myray.transform.parent = transform; 
            z = 0;
        }

        if (Input.GetKey(KeyCode.E))
        {
            z++;
            myray.transform.localPosition = new Vector3(0, 0, 3+ z);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Destroy(GameObject.FindGameObjectWithTag("Ray"));
            Destroy(GameObject.FindGameObjectWithTag("Ray"));

        }






        /*
         * 
        float h = horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float v = verticalSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(-v * 2, transform.rotation.y + h, 0);
         * 
        Debug.Log(Input.GetAxis("Mouse X") * -5);
        if (Input.GetAxis("Mouse X") != 0)
        {
            transform.Rotate(0, 0, Input.GetAxis("Mouse X") * -5, Space.Self);
        }

        Debug.Log(Input.GetAxis("Mouse Y") * -5);
        if (Input.GetAxis("Mouse Y") != 0)
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -5, 0, 0, Space.Self);
        }
        */
        // rb.AddForce(movement * speed);
    }

}
