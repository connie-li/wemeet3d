using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using MirrorBasics;
using UnityEngine.UI;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class CharacterSelect : NetworkBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private GameObject char1;
        [SerializeField] private GameObject char2;
        [SerializeField] private GameObject char3;
        [SerializeField] private GameObject char4;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;
        [SerializeField] private NetworkManager nm = null;
        [SerializeField] public GameObject selectionCanvas;

        private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();
        private List<GameObject> characterImgs = new List<GameObject>();

        public void Start()
        {
          Debug.Log("Starting character select");
            if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);
                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }
            }

            characterImgs.Add(char1);
            characterImgs.Add(char2);
            characterImgs.Add(char3);
            characterImgs.Add(char4);

            characterInstances[currentCharacterIndex].SetActive(true);
            characterImgs[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
            selectionCanvas.SetActive(false);
        }


        public void Restart()
        {
          Debug.Log("Starting character select");
            if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);
                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
          //  selectionCanvas.SetActive(false);
        }

        private void Update()
        {
            characterPreviewParent.RotateAround(
                characterPreviewParent.position,
                characterPreviewParent.up,
                turnSpeed * Time.deltaTime);
        }

        public void DeleteCharacterOptions()
        {
          foreach (Transform child in characterPreviewParent)
          {
              GameObject.Destroy(child.gameObject);
            }
        }

        public void Select()
        {
            CmdSelect(currentCharacterIndex, characters);
            characterSelectDisplay.SetActive(false);
        }

      //  [Command]
        public void CmdSelect(int characterIndex, Character[] characters)
        {
          GameObject characterInstance = characters[characterIndex].GameplayCharacterPrefab;
          NetworkConnection conn = Player.localPlayer.GetConnection();
          nm.GetComponent<NetworkManagerEdit>().ReplacePlayer(characterInstance, conn);
        }


        public void Right()
        {
            characterInstances[currentCharacterIndex].SetActive(false);
            characterImgs[currentCharacterIndex].SetActive(false);

            currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

            characterInstances[currentCharacterIndex].SetActive(true);
            characterImgs[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }

        public void Left()
        {
            characterInstances[currentCharacterIndex].SetActive(false);
            characterImgs[currentCharacterIndex].SetActive(false);

            currentCharacterIndex--;
            if (currentCharacterIndex < 0)
            {
                currentCharacterIndex += characterInstances.Count;
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterImgs[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }
    }
}
