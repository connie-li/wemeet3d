using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

namespace DapperDino.Mirror.Tutorials.CharacterSelection
{
    public class CharacterSelect : MonoBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;

        private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();

        public void Start()
        {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

                    characterInstance.SetActive(false);

                    characterInstances.Add(characterInstance);
                }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
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
