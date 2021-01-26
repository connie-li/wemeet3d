﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    Rigidbody rb;
    public float distance = 5;

    //public GameObject Camera;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;
    }

    void FixedUpdate()
     {
             float moveHorizontal = Input.GetAxis("Horizontal");
             float moveVertical = Input.GetAxis("Vertical");
 
             Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
 
             rb.AddForce(movement*speed);
             if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb.Sleep();
                rb.velocity = Vector3.zero;
             }
            rb.velocity = new Vector3(moveHorizontal*speed, 0.0f, moveVertical*speed);
            //Debug.Log("Text: " + transform.forward);

     }

}
*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
 {
     public float speed = 5;
     Rigidbody rb;
     public Text txtScore;
     int score = 0;
     bool isGameOver;
     public GameObject panelGameOver;
     void Start()
     {
         rb = GetComponent<Rigidbody>();
     }
 
     void FixedUpdate()
     {
         if (!isGameOver)
         {
             float moveHorizontal = Input.GetAxis("Horizontal");
             float moveVertical = Input.GetAxis("Vertical");
 
             Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
 
             rb.AddForce(movement*speed);
         }
     }

     private void OnTriggerEnter(Collider other)
     {
         if(other.gameObject.tag == "Coin")
         {
             Destroy(other.gameObject);
             score++;
             txtScore.text = "Score : " + score;
         }

         if(other.gameObject.tag == "Enemy")
         {
             //GameOver
             isGameOver = true;
             rb.velocity = Vector3.zero;
             rb.isKinematic = true;
             panelGameOver.SetActive(true);
         }
     }

     public void playAgainUI()
     {
         SceneManager.LoadScene("SceneOne");
     }
 }
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 10;
    public float speed = 2;
    static Animator anim;
    public Rigidbody char_RB;
    //public GameObject Camera;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        char_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    /*
    public Rigidbody char_RB;
 
 void FixedUpdate(){
     if(Input.GetAxis("Vertical")){
         char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
     }
 }
    */
    void Update()
    {
            //float translation = Input.GetAxis("Vertical") * speed;
            float translation = Input.GetAxis("Vertical") * speed;
            //char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;        //transform.position = transform.position + Camera.main.transform.forward * distance * Time.deltaTime;
            //Vector3 movement = new Vector3(rotation,0,translation);
            //movement = Vector3.ClampMagnitude(movement,speed);
            //movement *= Time.deltaTime;
            //transform.Translate(movement);
            
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0,0,translation);
            transform.Rotate(0,rotation,0);

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
           // Vector3 movement = new Vector3(rotation, 0.0f, translation);
            //char_RB.AddForce(movement*speed);

            if(translation != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
    }

}
