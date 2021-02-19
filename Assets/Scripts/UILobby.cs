using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorBasics {

    public class UILobby : MonoBehaviour {

        public static UILobby instance = null;

        [Header ("Host Join")]
        [SerializeField] InputField joinMatchInput;
        [SerializeField] Button joinButton;
        [SerializeField] Button hostButton;
        [SerializeField] GameObject lobbyCanvas;
        [SerializeField] GameObject hostCanvas;
        [SerializeField] GameObject joinCanvas;


        [Header ("Lobby")]
        //[SerializeField] Transform UIPlayerParent;
        [SerializeField] GameObject UIPlayerPrefab;
        [SerializeField] Text matchIDText;
        [SerializeField] GameObject beginGameButton;
        [SerializeField] GameObject PlayerPrefab;

        void Awake()
        {
          if (instance == null) {
              instance = this;
          } else if (instance != this){
              Destroy(gameObject);
          }
        }

        void Start () {
          Debug.Log ($"<color = green>UI LObby is start</color>");
            instance = this;
        }

        public void Host () {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            //Player p = new Player();
          //  Player p = PlayerPrefab.AddComponent<Player>();
            //p.HostGame();

            Player.localPlayer.HostGame();


        }

        public void HostSuccess (bool success, string matchID) {
            if (success) {
                lobbyCanvas.SetActive(true);
                hostCanvas.SetActive(false);
                joinCanvas.SetActive(false);
                Debug.Log ($"<color = green>before Spawn</color>");
                SpawnPlayerUIPrefab (Player.localPlayer);
                Debug.Log ($"<color = green>after spawn</color>");
                matchIDText.text = matchID;
                beginGameButton.SetActive (true);
            } else {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void Join () {
            joinMatchInput.interactable = false;
            joinButton.interactable = false;
            hostButton.interactable = false;

            Player.localPlayer.JoinGame (joinMatchInput.text.ToUpper ());
        }

        public void JoinSuccess (bool success, string matchID) {
            if (success) {
                lobbyCanvas.SetActive(true);
                hostCanvas.SetActive(false);
                joinCanvas.SetActive(false);
                SpawnPlayerUIPrefab (Player.localPlayer);
                matchIDText.text = matchID;
            } else {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void SpawnPlayerUIPrefab (Player player) {
          Debug.Log ($"<color = green>INSIDE Spawn</color>");
            GameObject newUIPlayer = Instantiate (UIPlayerPrefab);
              Debug.Log ($"<color = green>AFTER instanciate</color>");
            newUIPlayer.GetComponent<UIPlayer> ().SetPlayer (player);
          //  newUIPlayer.transform.SetSiblingIndex (player.playerIndex - 1);
        }

        public void BeginGame () {
            Player.localPlayer.BeginGame ();
        }

    }
}
