using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using io.agora.rtm.demo;
using agora_rtm;

namespace MirrorBasics {

    public class Player : NetworkBehaviour {

        public static Player localPlayer = null;
        [SyncVar] public string matchID;
        [SyncVar] public string meetingPassword;
        [SyncVar] public int playerIndex;

        NetworkMatchChecker networkMatchChecker;
        RtmChatManager rtmChat;

        void Start () {
            Debug.Log ($"<color = green>Player is start</color>");
            networkMatchChecker = GetComponent<NetworkMatchChecker> ();
            if (localPlayer == null) {
                localPlayer = this;
                Debug.Log ($"<color = Instanciating </color>");
            } else {
                UILobby.instance.SpawnPlayerUIPrefab (this);
            }
        }

      //  void Start()
      //  {
      //      Debug.Log ($"<color = green>activating</color>");
        //  gameObject.SetActive(true);
      //  }

        /*
            HOST MATCH
        */

        public void HostGame () {
          Debug.Log ($"<color = green>hereeeee</color>");
            string matchID = MatchMaker.GetRandomMatchID ();
            string meetingPassword = MatchMaker.GetRandomPassword ();
            Debug.Log("WTF");
            if (gameObject.activeSelf)
            {
            	Debug.Log ($"<color = green>YEP, ACTIVE</color>");
            }else
            {
              Debug.Log ($"<color = green>NOT ACTIVE</color>");
            }

            CmdHostGame (matchID, meetingPassword);
        }

        [Command]
        void CmdHostGame (string _matchID, string _meetingPassword ) {
            matchID = _matchID;
            meetingPassword = _meetingPassword;
            if (MatchMaker.instance.HostGame (_matchID, gameObject, out playerIndex, _meetingPassword)) {
                Debug.Log ($"<color = green>Game hosted successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid ();
                TargetHostGame (true, _matchID, playerIndex, _meetingPassword);
            } else {
                Debug.Log ($"<color = red>Game hosted failed</color>");
                TargetHostGame (false, _matchID, playerIndex, _meetingPassword);
            }
        }

        [TargetRpc]
        void TargetHostGame (bool success, string _matchID, int _playerIndex, string _meetingPassword) {
            playerIndex = _playerIndex;
            matchID = _matchID;
            meetingPassword = _meetingPassword;
            Debug.Log ($"MatchID: {matchID} == {_matchID}");
            UILobby.instance.HostSuccess (success, _matchID, _meetingPassword);

            var channelName = "test";
            Debug.Log("Check1");
            RtmClientEventHandler clientEventHandler = new RtmClientEventHandler();
            RtmChannel channel;
            RtmCallEventHandler callEventHandler = new RtmCallEventHandler();
            RtmClient rtmClient = new RtmClient("5b605d324a8d4fe082de1236e69872af", clientEventHandler);
            RtmChannelEventHandler channelEventHandler = new RtmChannelEventHandler();
            channel = rtmClient.CreateChannel(channelName, channelEventHandler);
            Debug.Log("Chec2");
            channel.Join();
            //rtmChat.JoinChannel()

        }

        /*
            JOIN MATCH
        */

        public void JoinGame (string _inputID, string _inputPassword) {
            CmdJoinGame (_inputID,_inputPassword );
            //rtmChat.JoinChannel();
            //RtmChannel channel;
            //channel.Join();
        }

        [Command]
        void CmdJoinGame (string _matchID, string _meetingPassword) {
            matchID = _matchID;
            meetingPassword = _meetingPassword;
            if (MatchMaker.instance.JoinGame (_matchID, gameObject, out playerIndex, _meetingPassword)) {
                Debug.Log ($"<color = green>Game Joined successfully</color>");
                networkMatchChecker.matchId = _matchID.ToGuid ();
                TargetJoinGame (true, _matchID, playerIndex, _meetingPassword);
            } else {
                Debug.Log ($"<color = red>Game Joined failed</color>");
                TargetJoinGame (false, _matchID, playerIndex, _meetingPassword);
            }
        }

        [TargetRpc]
        void TargetJoinGame (bool success, string _matchID, int _playerIndex, string _meetingPassword) {
            playerIndex = _playerIndex;
            matchID = _matchID;
            meetingPassword = _meetingPassword;
            Debug.Log ($"MatchID: {matchID} == {_matchID}");
            Debug.Log ($"PasswordID: {meetingPassword} == {_meetingPassword}");
            if(!success)
            {
                Debug.Log("Password is wrong. Please try again");
            }
                UILobby.instance.JoinSuccess (success, _matchID, _meetingPassword);
        }

        /*
            BEGIN MATCH
        */

        public void BeginGame () {
            CmdBeginGame ();
        }

        [Command]
        void CmdBeginGame () {
            MatchMaker.instance.BeginGame (matchID);
            Debug.Log ($"<color = red>Game Beginning</color>");
        }

        public void StartGame () {
            TargetBeginGame ();
        }

        [TargetRpc]
        void TargetBeginGame () {
            Debug.Log ($"MatchID: {matchID} | Beginning");
            //Additively load game scene
            SceneManager.LoadScene ("Test", LoadSceneMode.Additive);
        }

        /*[TargetRpc]
        void TargetShowMessage(string msg,string peer,string username,MessageDisplay messageDisplay,string displayMsg,RtmChannel channel,RtmClient rtmClient){
          Debug.Log("check");
          //string peer = "[channel:" + ChannelName + "]";


          //messageDisplay.AddTextToDisplay(displayMsg, Message.MessageType.PlayerMessage);
          channel.SendMessage(rtmClient.CreateMessage(msg));

        }

        public void showMessage(string msg,string peer,string username,MessageDisplay messageDisplay,string displayMsg,RtmChannel channel,RtmClient rtmClient){
          TargetShowMessage(msg,peer,username,messageDisplay,displayMsg,channel,rtmClient);
        }*/

    }

}
