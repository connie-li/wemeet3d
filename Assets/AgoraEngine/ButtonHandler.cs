using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    TestHome app;
    // Start is called before the first frame update
    void Start()
    {

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
          //app.onJoinButtonClicked();
        }
        if(name.CompareTo("video-toggle-button") == 0)
        {
          app.onJoinButtonClicked();
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
