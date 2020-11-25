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


    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
     transform.position = player.transform.position + offset;
     yaw += speedH * Input.GetAxis("Mouse X");
     pitch -= speedV * Input.GetAxis("Mouse Y");
     transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}

