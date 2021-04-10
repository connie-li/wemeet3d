/*
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
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 100;
    public float speed = 10;
    static Animator anim;
    public Rigidbody char_RB;
    public GameObject panelSitDown;
    public Button yourButton;
    bool UserInput = true;
    bool sitDown = false;
    //public GameObject Camera;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        char_RB = GetComponent<Rigidbody>();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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
            if(UserInput)
            {
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0,0,translation);
            transform.Rotate(0,rotation,0);

            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
           // Vector3 movement = new Vector3(rotation, 0.0f, translation);
            //char_RB.AddForce(movement*speed);
            if(Input.GetButtonDown("Jump"))
            {
                anim.SetBool("isSitting",true);
            }

            if(anim.GetBool("isSitting")==true || sitDown == true)
            {
                if(translation != 0)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isSitting",false);

                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isSitting",true);
                    sitDown = false;

                }
            }
            else
            {
                if(translation != 0)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isSitting",false);

                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isSitting",false);
                }
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    private void OnCollisionEnter(Collision other)
     {
         if(other.gameObject.tag == "Chair")
         {
             float rotation = 178;
             float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
             float translation = 0;
            double temp = other.gameObject.transform.position.x + 1.196;//- 0.01;//-0.6-0.33;
            double temp1 = other.gameObject.transform.position.z + 0.2492;//+ 0.514;
            Debug.Log(other.gameObject.transform.position.x);
            Debug.Log(other.gameObject.transform.position.z);
            Vector3 position = new Vector3((float)temp,(float)-15,(float)temp1);//new Vector3((float)-0.93,(float)0.3,(float)-0.62);
            position.x += moveHorizontal * speed * Time.deltaTime;
            position.z += moveVertical * speed * Time.deltaTime;
            transform.position = position;
            rotation *= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 178, 0);
             anim.SetBool("isWalking", false);
             anim.SetBool("isIdle", false);
             anim.SetBool("isSitting",true);
             Debug.Log(anim.GetBool("isSiting"));
             sitDown = true;
            // transform.position.x = temp;
            // transform.position.z = temp1;
            // translation *= Time.deltaTime;
            // //rotation *= Time.deltaTime;
            // transform.Translate(0,0,translation);
            //transform.Rotate(0,rotation,0);
            //transform.Translate(0,0,translation);
            //float rotation = 178 * rotationSpeed;
            //transform.Rotate(0,rotation,0);
            //char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
            // float rotation = Input.GetAxis("Horizontal") * rotationSpeed;    
            // transform.Translate(0,0,translation);
            // transform.Rotate(0,rotation,0);
            // panelSitDown.SetActive(true);
            // anim.SetBool("isWalking", false);
            // anim.SetBool("isIdle", true);
            // anim.SetBool("isSitting",false);
            // UserInput = false;
         }

         if(other.gameObject.tag == "ChairBack")
         {
             float rotation = (float)3.124;
             float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
             float translation = 0;
            double temp = other.gameObject.transform.position.x - 1.18;//- 0.01;//-0.6-0.33;
            double temp1 = other.gameObject.transform.position.z - 0.6;//+ 0.514;
            Debug.Log(other.gameObject.transform.position.x);
            Debug.Log(other.gameObject.transform.position.z);
            Vector3 position = new Vector3((float)temp,(float)-15,(float)temp1);//new Vector3((float)-0.93,(float)0.3,(float)-0.62);
            position.x += moveHorizontal * speed * Time.deltaTime;
            position.z += moveVertical * speed * Time.deltaTime;
            transform.position = position;
            rotation *= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, (float)3.124, 0);
             anim.SetBool("isWalking", false);
             anim.SetBool("isIdle", false);
             anim.SetBool("isSitting",true);
             Debug.Log(anim.GetBool("isSiting"));
             sitDown = true;
            // transform.position.x = temp;
            // transform.position.z = temp1;
            // translation *= Time.deltaTime;
            // //rotation *= Time.deltaTime;
            // transform.Translate(0,0,translation);
            //transform.Rotate(0,rotation,0);
            //transform.Translate(0,0,translation);
            //float rotation = 178 * rotationSpeed;
            //transform.Rotate(0,rotation,0);
            //char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
            // float rotation = Input.GetAxis("Horizontal") * rotationSpeed;    
            // transform.Translate(0,0,translation);
            // transform.Rotate(0,rotation,0);
            // panelSitDown.SetActive(true);
            // anim.SetBool("isWalking", false);
            // anim.SetBool("isIdle", true);
            // anim.SetBool("isSitting",false);
            // UserInput = false;
         }

        if(other.gameObject.tag == "ChairRight")
         {
             float rotation = (float)91.119;
             float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
             float translation = 0;
            double temp = other.gameObject.transform.position.x - 0.817;//- 0.01;//-0.6-0.33;
            double temp1 = other.gameObject.transform.position.z + 1.1421;//+ 0.514;
            Debug.Log(other.gameObject.transform.position.x);
            Debug.Log(other.gameObject.transform.position.z);
            Vector3 position = new Vector3((float)temp,(float)-15,(float)temp1);//new Vector3((float)-0.93,(float)0.3,(float)-0.62);
            position.x += moveHorizontal * speed * Time.deltaTime;
            position.z += moveVertical * speed * Time.deltaTime;
            transform.position = position;
            rotation *= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, (float)91.119, 0);
             anim.SetBool("isWalking", false);
             anim.SetBool("isIdle", false);
             anim.SetBool("isSitting",true);
             Debug.Log(anim.GetBool("isSiting"));
             sitDown = true;
            // transform.position.x = temp;
            // transform.position.z = temp1;
            // translation *= Time.deltaTime;
            // //rotation *= Time.deltaTime;
            // transform.Translate(0,0,translation);
            //transform.Rotate(0,rotation,0);
            //transform.Translate(0,0,translation);
            //float rotation = 178 * rotationSpeed;
            //transform.Rotate(0,rotation,0);
            //char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
            // float rotation = Input.GetAxis("Horizontal") * rotationSpeed;    
            // transform.Translate(0,0,translation);
            // transform.Rotate(0,rotation,0);
            // panelSitDown.SetActive(true);
            // anim.SetBool("isWalking", false);
            // anim.SetBool("isIdle", true);
            // anim.SetBool("isSitting",false);
            // UserInput = false;
         }


         if(other.gameObject.tag == "ChairLeft")
         {
             float rotation = (float)-88.05;
             float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
             float translation = 0;
            double temp = other.gameObject.transform.position.x + 0.81;//- 0.01;//-0.6-0.33;
            double temp1 = other.gameObject.transform.position.z - 1.15;//+ 0.514;
            Debug.Log(other.gameObject.transform.position.x);
            Debug.Log(other.gameObject.transform.position.z);
            Vector3 position = new Vector3((float)temp,(float)-15,(float)temp1);//new Vector3((float)-0.93,(float)0.3,(float)-0.62);
            position.x += moveHorizontal * speed * Time.deltaTime;
            position.z += moveVertical * speed * Time.deltaTime;
            transform.position = position;
            rotation *= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, (float)-88.05, 0);
             anim.SetBool("isWalking", false);
             anim.SetBool("isIdle", false);
             anim.SetBool("isSitting",true);
             Debug.Log(anim.GetBool("isSiting"));
             sitDown = true;
            // transform.position.x = temp;
            // transform.position.z = temp1;
            // translation *= Time.deltaTime;
            // //rotation *= Time.deltaTime;
            // transform.Translate(0,0,translation);
            //transform.Rotate(0,rotation,0);
            //transform.Translate(0,0,translation);
            //float rotation = 178 * rotationSpeed;
            //transform.Rotate(0,rotation,0);
            //char_RB.MovePosition(char_RB.gameObject.transform.forward * speed);
            // float rotation = Input.GetAxis("Horizontal") * rotationSpeed;    
            // transform.Translate(0,0,translation);
            // transform.Rotate(0,rotation,0);
            // panelSitDown.SetActive(true);
            // anim.SetBool("isWalking", false);
            // anim.SetBool("isIdle", true);
            // anim.SetBool("isSitting",false);
            // UserInput = false;
         }






     }

     void TaskOnClick()
     {
         anim.SetBool("isSitting",true);
         Debug.Log("Clicked");
         UserInput = true;
         panelSitDown.SetActive(false);
     }


}