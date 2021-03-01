using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace MirrorBasics {

public class NetworkManagerEdit : NetworkManager
{
    public void BeingHost()
    {
      StartHost();
      Debug.Log ($"After START HOST networkmanager");
      CallUIHost();
    //  UILobby.instance.Host();
    }

    public void CallUIHost()
    {
      StartCoroutine(DelayedCallUIHost());
    }

    IEnumerator DelayedCallUIHost()
    {
      yield return new WaitForEndOfFrame();
      UILobby.instance.Host();
    }


    public void BeingClient()
    {
      StartClient();
      Debug.Log ($"After START CLIENT networkmanager");
      CallUIClient();
    //  UILobby.instance.Host();
    }

    public void CallUIClient()
    {
      Debug.Log ($"Function Callclietn is called");
      StartCoroutine(DelayedCallUIClient());
    }

    IEnumerator DelayedCallUIClient()
    {
      yield return new WaitForSeconds(4);
      Debug.Log ($"End of wait");
      UILobby.instance.Join();
    }


}

}
