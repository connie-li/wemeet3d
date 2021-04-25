using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class CharacterSelect : NetworkBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;

        private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();
        public bool hasAuthority => netIdentity.hasAuthority;
        public override void OnStartClient()
        {
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
            CmdSelect(currentCharacterIndex);
            characterSelectDisplay.SetActive(false);
        }

        [ClientRpc]
        public void CmdSelect(int characterIndex)
        {
            GameObject characterInstance = Instantiate(characters[characterIndex].GameplayCharacterPrefab);
            NetworkServer.Spawn(characterInstance);
        }

        public void Right()
        {
            characterInstances[0].SetActive(false);

            currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

            characterInstances[1].SetActive(true);
            characterNameText.text = characters[1].CharacterName;
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