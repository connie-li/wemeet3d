using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Mirror;
using UnityEngine;

namespace MirrorBasics {

    [System.Serializable]
    public class Match {
        public string matchID;
        public string meetingPassword;
        public bool meetingStarted;
        public SyncListGameObject players = new SyncListGameObject ();

        public Match (string matchID, GameObject player, string meetingPassword) {
            this.matchID = matchID;
            this.meetingPassword = meetingPassword;
            this.meetingStarted = false;
            players.Add (player);
        }

        public Match () { }
    }

    [System.Serializable]
    public class SyncListGameObject : SyncList<GameObject> { }

    [System.Serializable]
    public class SyncListMatch : SyncList<Match> { }

    public class MatchMaker : NetworkBehaviour {

        public static MatchMaker instance = null;

        public SyncListMatch matches = new SyncListMatch ();
        public SyncListString matchIDs = new SyncListString ();

        //[SerializeField] GameObject turnManagerPrefab;

        void Awake () {
            instance = this;
        }

        public bool HostGame (string _matchID, GameObject _player, out int playerIndex, string _meetingPassword) {
            playerIndex = -1;

            if (!matchIDs.Contains (_matchID)) {
                matchIDs.Add (_matchID);
                matches.Add (new Match (_matchID, _player,_meetingPassword));
                Debug.Log ($"Match generated");
                playerIndex = 1;
                return true;
            } else {
                Debug.Log ($"Match ID already exists");
                return false;
            }
        }

        public bool checkIfMeetingStarted(string _matchID)
        {
            if (matchIDs.Contains (_matchID)) {
              for (int i = 0; i < matches.Count; i++) {
                  if (matches[i].matchID == _matchID) {
                      Debug.Log ($"Meeting ID");
                      Debug.Log (matches[i].matchID);
                      Debug.Log (matches[i].meetingStarted);
                    if (matches[i].meetingStarted == true)
                    {
                      Debug.Log ($"Found meeting started");
                      return true;
                    }
                  }
                }
            }
            Debug.Log ($"Found meeting NOT started");
            return false;
        }

        public void markMeetingAsStarted(string _matchID)
        {
          Debug.Log ($"iNSIDE MARKING METING");
            if (matchIDs.Contains (_matchID)) {
              for (int i = 0; i < matches.Count; i++) {
                  if (matches[i].matchID == _matchID) {
                    if (matches[i].meetingStarted == true)
                    {
                      matches[i].meetingStarted = true;
                    }
                  }
                }
            }
        }

        public bool JoinGame (string _matchID, GameObject _player, out int playerIndex, string _meetingPassword) {
            playerIndex = -1;

            if (matchIDs.Contains (_matchID)) {

                for (int i = 0; i < matches.Count; i++) {
                    if (matches[i].matchID == _matchID) {
                      if(matches[i].meetingPassword == _meetingPassword)
                      {
                        matches[i].players.Add (_player);
                        playerIndex = matches[i].players.Count;
                        Debug.Log ($"Match joined");
                        return true;
                      }else
                      {
                          Debug.Log ($"Wrong password");
                      }
                    }
                }
                return false;
            } else {
                Debug.Log ($"Match ID does not exist");
                return false;
            }
        }

        public void BeginGame (string _matchID) {
            //GameObject newTurnManager = Instantiate (turnManagerPrefab);
            //NetworkServer.Spawn (newTurnManager);
            //newTurnManager.GetComponent<NetworkMatchChecker> ().matchId = _matchID.ToGuid ();
            //TurnManager turnManager = newTurnManager.GetComponent<TurnManager> ();

            for (int i = 0; i < matches.Count; i++) {
                if (matches[i].matchID == _matchID) {
                    matches[i].meetingStarted = true; //marking as started
                    Debug.Log ($"MAKING THIS MATCH TRUE FOR STARTED");
                    Debug.Log (matches[i].matchID);
                    Debug.Log (matches[i].meetingStarted);
                    foreach (var player in matches[i].players) {
                        Player _player = player.GetComponent<Player> ();
                        //turnManager.AddPlayer (_player);
                        _player.StartGame ();
                    }
                    break;
                }
            }

        }

        public static string GetRandomMatchID () {
            string _id = string.Empty;
            for (int i = 0; i < 5; i++) {
                int random = UnityEngine.Random.Range (0, 36);
                if (random < 26) {
                    _id += (char) (random + 65);
                } else {
                    _id += (random - 26).ToString ();
                }
            }
            Debug.Log ($"Random Match ID: {_id}");
            return _id;
        }

        public static string GetRandomPassword () {
            string _id = string.Empty;
            for (int i = 0; i < 5; i++) {
                int random = UnityEngine.Random.Range (0, 36);
                if (random < 26) {
                    _id += (char) (random + 65);
                } else {
                    _id += (random - 26).ToString ();
                }
            }
            Debug.Log ($"Random Password: {_id}");
            return _id;
        }

    }

    public static class MatchExtensions {
        public static Guid ToGuid (this string id) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider ();
            byte[] inputBytes = Encoding.Default.GetBytes (id);
            byte[] hashBytes = provider.ComputeHash (inputBytes);

            return new Guid (hashBytes);
        }
    }

}
