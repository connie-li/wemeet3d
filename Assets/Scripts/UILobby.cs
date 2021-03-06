using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace MirrorBasics {

    public class UILobby : MonoBehaviour {

        public static UILobby instance = null;
        //public TextMeshProUGUI RoomPass;
        [Header ("Host Join")]
        [SerializeField] InputField joinMatchInput;
        [SerializeField] InputField passwordInput;
        [SerializeField] Button joinButton;
        [SerializeField] Button hostButton;
        [SerializeField] GameObject lobbyClient;
        [SerializeField] GameObject lobbyHost;
        [SerializeField] GameObject hostCanvas;
        [SerializeField] GameObject joinCanvas;
        [SerializeField] TextMeshProUGUI RoomPass;

        [Header ("Lobby")]
        //[SerializeField] Transform UIPlayerParent;
        [SerializeField] GameObject UIPlayerPrefab;
        [SerializeField] Text matchIDText;
        [SerializeField] Text passwordText;
        [SerializeField] GameObject beginGameButton;
        [SerializeField] GameObject PlayerPrefab;

        [Header ("MainGAME")]
        [SerializeField] GameObject mainCanvas;
        [SerializeField] TextMeshProUGUI errorMessageSceneSelection;
        [SerializeField] SceneSelect ChooseRoomGroup;

        [Header ("Selection")]
        [SerializeField] Transform characterPreview;
        [SerializeField] Material skybox_moon;
        [SerializeField] Material skybox_forest;
        [SerializeField] Material skybox_conf;

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

        public void waitALittle()
        {
          StartCoroutine(waiter());
        }

        IEnumerator waiter()
        {
          //Wait for 4 seconds
          yield return new WaitForSeconds(4);
        }

        public void Host () {
          //  joinMatchInput.interactable = false;
          //  joinButton.interactable = false;
            //hostButton.interactable = false;

            //Player p = new Player();
          //  Player p = PlayerPrefab.AddComponent<Player>();
            //p.HostGame();

            Player.localPlayer.HostGame();


        }

        public void HostSuccess (bool success, string matchID, string meetingPassword) {
            if (success) {
                lobbyHost.SetActive(true);
                hostCanvas.SetActive(false);
                joinCanvas.SetActive(false);
                Debug.Log ($"<color = green>before Spawn</color>");
                SpawnPlayerUIPrefab (Player.localPlayer);
                Debug.Log ($"<color = green>after spawn</color>");
                matchIDText.text = matchID;
                passwordText.text = meetingPassword;
                beginGameButton.SetActive (true);
            } else {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
            }
        }

        public void Join () {
          //  joinMatchInput.interactable = false;
          //  joinButton.interactable = false;
          //  hostButton.interactable = false;
            if(Player.localPlayer == null)
            {
              Debug.Log ($"Failed to Connect");
            }
            else
            {Player.localPlayer.JoinGame (joinMatchInput.text.ToUpper (), passwordInput.text.ToUpper ());}
        }

        public void JoinSuccess (bool success, string matchID, string meetingPassword) {
            if (success) {
                lobbyClient.SetActive(true);
                hostCanvas.SetActive(false);
                joinCanvas.SetActive(false);
                SpawnPlayerUIPrefab (Player.localPlayer);
                matchIDText.text = matchID;
                passwordText.text = meetingPassword;
                RoomPass.text = "";
                //to check if meeting already meeting already started
                Debug.Log("Checking if started in next line");
              //  Debug.Log (Player.localPlayer.checkIfStarted(matchID));
            //  if(ChooseRoomGroup.SceneWasSelected() == true)
              //{
                  //string selectedScene = ChooseRoomGroup.getSelectedScene();
                  Player.localPlayer.joinIfStarted(matchID);
              //}

            } else {
                joinMatchInput.interactable = true;
                joinButton.interactable = true;
                hostButton.interactable = true;
                RoomPass.text = "The room code or password you entered is incorrect";

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
          if(ChooseRoomGroup.SceneWasSelected() == true)
          {
              string selectedScene = ChooseRoomGroup.getSelectedScene();
              if (selectedScene == "moon")
              {
                RenderSettings.skybox = skybox_moon;
              }

              if (selectedScene == "forest")
              {
                RenderSettings.skybox = skybox_forest;
              }

              if (selectedScene == "conference-room-new")
              {
                RenderSettings.skybox = skybox_conf;
              }
              
              Player.localPlayer.BeginGame (selectedScene);
          }
          else
          {
            errorMessageSceneSelection.text = "Please select a room";
          }
        


        }

        public void removeCanvas()
        {
          mainCanvas.SetActive(false);
        }

        public void DeleteUnusedCharacters()
        {
          foreach (Transform child in characterPreview)
          {
              GameObject.Destroy(child.gameObject);
            }
        }

    }
}
