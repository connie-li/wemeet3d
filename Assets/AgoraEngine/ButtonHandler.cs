using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    TestHome app;
    public bool vid;
    public bool mute;
    public bool chat;

    // Start is called before the first frame update
    void Start()
    {
      vid = true;
      mute = true;
      chat = true;
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
        if (name.CompareTo("mic-toggle-button") == 0)
        {
          if(mute)
          {
            app.turnOffOnMic(mute);
            mute = false;
            return;
          }
          else
          {
            app.turnOffOnMic(mute);
            mute = true;
            return;
          }
        }
        if(name.CompareTo("video-toggle-button") == 0)
        {
          if(vid)
          {
            app.turnOffOnVid(vid);
            vid = false;
            return;
          }
          else
          {
            app.turnOffOnVid(vid);
            vid = true;
            return;
          }
        }
        if (name.CompareTo("chat-toggle-button") == 0)
        {
          if(chat)
          {
            app.turnOffOnChat(chat);
            chat = false;
            return;
          }
          else
          {
            app.turnOffOnChat(chat);
            chat = true;
            return;
          }

          //app.onJoinButtonClicked();
        }
        if(name.CompareTo("leave-meeting-button") == 0)
        {
          app.onLeaveButtonClicked();
        }
      }

    }
}
