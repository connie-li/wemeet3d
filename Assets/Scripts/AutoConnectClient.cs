using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AutoConnectClient : MonoBehaviour
{

  [SerializeField] NetworkManager networkManager;

  public void Start()
  {
    if (!Application.isBatchMode)
    {
      Debug.Log ("Client Build");
      networkManager.StartClient();
    }else{
      Debug.Log ("Server Build");
    }
  }

    public void JoinLocal ()
    {
      networkManager.networkAddress = "140.186.107.42";
      networkManager.StartClient();

    }
}
