using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


namespace MirrorBasics {
  public class BallControllerTest : NetworkBehaviour
  {
    //  public float rotationSpeed = 100;
      [SerializeField] private Vector3 movement = new Vector3();
      [SerializeField] public GameObject myCam;

      //void Start()
    //  {
          //char_RB = GetComponent<Rigidbody>();

    //  }

      [Client]
      void Update()
      {

      //Client code
      if(hasAuthority){
          myCam.SetActive(true);
        }

            if(!hasAuthority) {return; }

            if(!Input.GetKeyDown(KeyCode.Space)) {return; }

            transform.Translate(movement);

        }
  }
}
