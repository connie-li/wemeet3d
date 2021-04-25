using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector_script : MonoBehaviour
{
  public GameObject Remy;
  public GameObject Stefani;
  public GameObject Sophie;
  public GameObject Adam;
  public GameObject David;
  public GameObject Megan;
  private Vector3 CharacterPosition;
  private Vector3 Offscreen;
  private int CharacterInt = 1;
  private SpriteRenderer RemyRender, StefaniRender, SophieRender, AdamRender, DavidRender, MeganRender;

  private void Awake()
  {
    CharacterPosition = Remy.transform.position;
    Offscreen = Megan.transform.position;
    RemyRender = Remy.GetComponent<SpriteRenderer>();
    StefaniRender = Remy.GetComponent<SpriteRenderer>();
    SophieRender = Remy.GetComponent<SpriteRenderer>();
    AdamRender = Remy.GetComponent<SpriteRenderer>();
    DavidRender = Remy.GetComponent<SpriteRenderer>();
    MeganRender = Remy.GetComponent<SpriteRenderer>();
  }

  public void NextCharacter()
  {
    switch(CharacterInt)
    {
      case 1:
      RemyRender.enabled = false;
      Remy.transform.position = Offscreen;
      Stefani.transform.position = CharacterPosition;
      StefaniRender.enabled = true;
      CharacterInt++;
      break;
      case 2:
      StefaniRender.enabled = false;
      Stefani.transform.position = Offscreen;
      Sophie.transform.position = CharacterPosition;
      SophieRender.enabled = true;
      CharacterInt++;
        break;
      case 3:
      SophieRender.enabled = false;
      Sophie.transform.position = Offscreen;
      Adam.transform.position = CharacterPosition;
      AdamRender.enabled = true;
      CharacterInt++;
        break;
      case 4:
      AdamRender.enabled = false;
      Adam.transform.position = Offscreen;
      David.transform.position = CharacterPosition;
      DavidRender.enabled = true;
      CharacterInt++;
        break;
      case 5:
      DavidRender.enabled = false;
      David.transform.position = Offscreen;
      Megan.transform.position = CharacterPosition;
      MeganRender.enabled = true;
      CharacterInt++;
        break;
      case 6:
      MeganRender.enabled = false;
      Megan.transform.position = Offscreen;
      Remy.transform.position = CharacterPosition;
      RemyRender.enabled = true;
      CharacterInt++;
      ResetInt();
        break;
        default:
        ResetInt();
          break;
    }

  }

  public void PreviousCharacter()
  {
    switch(CharacterInt)
    {
      case 1:
      RemyRender.enabled = false;
      Remy.transform.position = Offscreen;
      Megan.transform.position = CharacterPosition;
      MeganRender.enabled = true;
      CharacterInt--;
        ResetInt();
        break;
      case 2:
      StefaniRender.enabled = false;
      Stefani.transform.position = Offscreen;
      Remy.transform.position = CharacterPosition;
      RemyRender.enabled = true;
      CharacterInt--;
        break;
      case 3:
      SophieRender.enabled = false;
      Sophie.transform.position = Offscreen;
      Stefani.transform.position = CharacterPosition;
      StefaniRender.enabled = true;
      CharacterInt--;
        break;
      case 4:
      AdamRender.enabled = false;
      Adam.transform.position = Offscreen;
      Sophie.transform.position = CharacterPosition;
      SophieRender.enabled = true;
      CharacterInt--;
        break;
      case 5:
      DavidRender.enabled = false;
      David.transform.position = Offscreen;
      Adam.transform.position = CharacterPosition;
      AdamRender.enabled = true;
      CharacterInt--;
        break;
      case 6:
      MeganRender.enabled = false;
      Megan.transform.position = Offscreen;
      David.transform.position = CharacterPosition;
      DavidRender.enabled = true;
      CharacterInt--;
        break;
        default:
          ResetInt();
          break;
    }
  }

  private void ResetInt()
  {
    if (CharacterInt >=6)
    {
      CharacterInt = 1;
    }

    else
    {
      CharacterInt = 6;
    }
  }
}
