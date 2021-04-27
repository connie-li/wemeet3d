using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace MirrorBasics {

  public class SceneSelect : MonoBehaviour
  {

    [SerializeField] Button forestScene;
    [SerializeField] Button conferenceScene;
    [SerializeField] Button moonScene;

    Button sceneSelected = null;

      public void SelectForest()
      {
        Debug.Log("Selected forest ");
        sceneSelected = forestScene;
      }

      public void SelectConference()
      {
        Debug.Log("Selected conference !");
        sceneSelected = conferenceScene;
      }

      public void SelectMoon()
      {
        Debug.Log("Selected moon!");
        sceneSelected = moonScene;
      }

      public bool SceneWasSelected()
      {
        if(sceneSelected != null)
        {
            return true;
        }
        return false;
      }

      public string getSelectedScene()
      {
        if(sceneSelected == forestScene)
        {
          return "forest"; //change this to forestscene
        } else if(sceneSelected == conferenceScene)
        {
          return "conference-room-new";
        }else if(sceneSelected == moonScene)
        {
          return "moon"; //change this to moonScene
        }else
        {
          return "main-menu"; //this should never return because the scenewasselected function is called before
        }
      }


  }

}
