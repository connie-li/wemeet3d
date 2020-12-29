using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Vector3 offset;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public float distance = 10;

    public float speed = 2.0f;
    public float rotationSpeed = 2.0f;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    //transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;


    private void LateUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
     Vector3 forward = player.transform.forward;
     forward.z = 0;
     //transform.forward = forward;


     float moveHorizontal = Input.GetAxis("Horizontal");
     float moveVertical = Input.GetAxis("Vertical");
     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
     //Vector3 rotation = transform.eulerAngles;
     //rotation.z += Input.GetAxis("Horizontal") * distance;
     //rotation.x -= Input.GetAxis("Vertical") * distance;

     //transform.rotation = player.transform.rotation; //+ offset;
     //transform.eulerAngles = forward;
     //transform.position = player.transform.position * distance * Time.deltaTime;

     transform.position = player.transform.position + offset;
     float translation = player.transform.position.z * speed;
     float rotation = player.transform.position.x * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        //transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        //transform.Rotate(0, rotation, 0);
        transform.LookAt(player.transform.position + offset);
     /*
     yaw += speedH * Input.GetAxis("Mouse X");
     pitch -= speedV * Input.GetAxis("Mouse Y");
     transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    */
   

    }
}

