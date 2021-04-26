using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using MirrorBasics;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class CharacterSelect : NetworkBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;
        [SerializeField] private NetworkManager nm = null;

        private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();

        public override void OnStartClient()
        {
            if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);
                      //  NetworkServer.Spawn(characterInstance);
                        //characterInstance.GetComponent<NetworkIdentity>().AssignClientAuthority(characterInstance.GetComponent<NetworkIdentity>();
                      // characterInstance.GetComponent<NetworkIdentity>().AssignClientAuthority(characterInstance.GetComponent<NetworkIdentity>().connectionToClient);

                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
        }

        private void Update()
        {
            characterPreviewParent.RotateAround(
                characterPreviewParent.position,
                characterPreviewParent.up,
                turnSpeed * Time.deltaTime);
        }


        public void Select()
        {
            //characterInstances[1].GetComponent<NetworkIdentity>().AssignClientAuthority(characterInstances[1].GetComponent<NetworkIdentity>().connectionToClient);
            //CmdAssignNetworkAuthority(GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
            //NetworkIdentity.AssignClientAuthority(this.);
            CmdSelect(currentCharacterIndex, characters);
            characterSelectDisplay.SetActive(false);
        }

      //  [Command]
        public void CmdSelect(int characterIndex, Character[] characters,  NetworkConnectionToClient sender = null)
        {
          GameObject characterInstance = Instantiate(characters[0].GameplayCharacterPrefab);
          //nm.GetComponent<NetworkManagerEdit>().getPlayerPrefab();
          //short cindex = short(CharacterIndex);
          //nm.GetComponent<NetworkManagerEdit>().OnServerAddPlayer(sender, characterIndex);
          //  characterInstance.GetComponent<NetworkIdentity>().AssignClientAuthority(characterInstance.GetComponent<NetworkIdentity>().connectionToClient);
          //  NetworkServer.Spawn(characterInstance, sender);
        //  nm.GetComponent<NetworkManagerEdit>().setPlayerPrefab(sender, characterInstance);
          NetworkServer.AddPlayerForConnection(sender, characterInstance);


        }



        public void Right()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }

        public void Left()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex--;
            if (currentCharacterIndex < 0)
            {
                currentCharacterIndex += characterInstances.Count;
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }
    }
}
