using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
     {
             float moveHorizontal = Input.GetAxis("Horizontal");
             float moveVertical = Input.GetAxis("Vertical");
 
             Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
 
             rb.AddForce(movement*speed);
             /*if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.Sleep();
                rb.velocity = Vector3.zero;
             }*/
            rb.velocity = new Vector3(moveHorizontal*speed, 0.0f, moveVertical*speed);

     }

}
