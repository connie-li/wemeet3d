using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace MirrorBasics {
  public class EnterMeetingCheck : MonoBehaviour
  {
      [SerializeField] NetworkManager networkManager;
      [SerializeField] Button enterButton;

      public void Start()
      {
        if (!Application.isBatchMode)
        {
          Debug.Log ("Entering meeting...");
          enterButton.interactable = true;
        }else{
          Debug.Log ("Server cannot enter a meeting!");
          enterButton.interactable = false;
        }
      }
  }
}
