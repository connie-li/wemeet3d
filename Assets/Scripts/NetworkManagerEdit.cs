using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using DapperDino.Mirror.Tutorials.CharacterSelection;


namespace MirrorBasics {

public class NetworkManagerEdit : NetworkManager
{
  [SerializeField] GameObject prefab;

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

    public GameObject getPlayerPrefab()
    {
      return playerPrefab;
    }

    public void setPlayerPrefab(NetworkConnection conn, GameObject newPlayerPrefab)
    {
      //playerPrefab = newPlayerPrefab;
      NetworkServer.AddPlayerForConnection(conn, newPlayerPrefab);
    }

    public virtual void OnServerAddPlayer(NetworkConnection conn, int playerControllerId)
    {
      var player = (GameObject)GameObject.Instantiate(playerPrefab);
      NetworkServer.AddPlayerForConnection(conn, player);

    }

  //  public override void OnClientConnect(NetworkConnection conn)
  //    {
  //        base.OnClientConnect(conn);
  //    }


    public void ReplacePlayer(int characterIndex, Character[] characters, NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
      //NetworkConnection conn = NetworkBehaviour.connectionToClient();
      GameObject characterInstance = prefab;

      // Cache a reference to the current player object
      GameObject oldPlayer = conn.identity.gameObject;

      // Instantiate the new player object and broadcast to clients
      bool replaced = NetworkServer.ReplacePlayerForConnection(conn, Instantiate(characterInstance), true);
      Debug.Log("It has been replaced?");
      Debug.Log(replaced);

      // Remove the previous player object that's now been replaced
      NetworkServer.Destroy(oldPlayer);

    }


}

}
