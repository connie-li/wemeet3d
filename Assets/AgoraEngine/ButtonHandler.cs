using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    TestHome app;
    public bool val;
    public bool mute;
    public bool start;

    // Start is called before the first frame update
    void Start()
    {
      start = true;
      val =true;
      mute = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnButtonClick()
    {
      GameObject go = GameObject.Find("GameController");

      //app = new TestHome();
      if (go != null)
      {
        TestHome app = go.GetComponent<TestHome>();
        if (app == null)
        {
            Debug.LogError("Missing game controller...");
            return;
        }
        Debug.Log("Button Clicked: " + name);
        if (name.CompareTo("PauseMenuButton") == 0)
        {
          app.onJoinButtonClicked();
          app.turnOff(false);
          app.turnOffMic(false);
        }
        if (name.CompareTo("mic-toggle-button") == 0)
        {
          //app.onJoinButtonClicked();
          /*if(start)
          {
            app.onJoinButtonClicked();
            start = false;
            app.turnOff(false);
            Debug.Log("check 1");
            //return;
          }*/
          //if(mute & !start)
          if(mute)
          {
            app.turnOffMic(mute);
            mute = false;
            //app.onJoinButtonClicked();
            //Debug.Log("turned on");
            Debug.Log("check 2");
            return;
          }
          else
          {
            app.turnOffMic(mute);
            mute = true;
            //Debug.Log("turned off");
            Debug.Log("check 3");
            return;
          }
        }
        if(name.CompareTo("video-toggle-button") == 0)
        {
          /*if(start)
          {
            app.onJoinButtonClicked();
            start = false;
            app.turnOffMic(false);
            Debug.Log("check 4");
            //return;
          }*/
          //if(val & !start)
          if(val)
          {
            app.turnOff(val);
            val = false;
            Debug.Log("check 5");
            //app.onJoinButtonClicked();
            //Debug.Log("turned on");
            return;
          }
          else
          {
            app.turnOff(val);
            val = true;
            Debug.Log("check 6");
            //Debug.Log("turned off");
            return;
          }
        }
        if (name.CompareTo("chat-toggle-button") == 0)
        {
          //app.onJoinButtonClicked();
        }
        if(name.CompareTo("leave-meeting-button") == 0)
        {
          app.onLeaveButtonClicked();
        }
      }

    }
}
