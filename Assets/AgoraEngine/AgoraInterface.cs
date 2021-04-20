using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using agora_gaming_rtc;
using agora_utilities;
using UnityEngine.SceneManagement;


public class AgoraInterface
{

    public IRtcEngine mRtcEngine;
    private Text MessageText;
    public string test;

    //initializating agora RTC engine
    public void loadEngine(string appId)
    {
      //starting SDK
      Debug.Log("initializating engine");

      if(mRtcEngine != null)
      {
        Debug.Log("Engine already exists");
        return;
      }
      //initializating RTC engine with appID
      mRtcEngine = IRtcEngine.getEngine(appId);
      // enable log
      mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);

    }

    public void join(string channel)
    {
      //set callbacks
      Debug.Log("calling join (channel = " + channel + ")");
      if (mRtcEngine == null)
          return;

      mRtcEngine.OnJoinChannelSuccess = onJoinChannelSuccess;
      mRtcEngine.OnUserJoined = onUserJoined;
      mRtcEngine.OnUserOffline = onUserOffline;

      mRtcEngine.OnWarning = (int warn, string msg) =>
      {
          Debug.LogWarningFormat("Warning code:{0} msg:{1}", warn, IRtcEngine.GetErrorDescription(warn));
      };
      mRtcEngine.OnError = HandleError;
      //enable video

      //recently commented
      mRtcEngine.EnableVideo();

      //allow for camera output callback

      //recently commented
      mRtcEngine.EnableVideoObserver();

      // join channel
      //mRtcEngine.StartPreview();
      mRtcEngine.JoinChannel(channel, null, 0);
      //mRtcEngine.StartPreview();
      Debug.Log ("initializeEngine done");
    }


    //enabling camera
    /*public void joinMeeting()
    {
      //enable video
      mRtcEngine.EnableVideo();
      //allow for camera output callback
      mRtcEngine.EnableVideoObserver();
      mRtcEngine.StartPreview();
    }*/

    public void turnCamera(bool OnOffButton)
    {
      //mRtcEngine.DisableVideoObserver();
      if (OnOffButton == true)
      {
        GameObject go = new GameObject("RawImage");
        //GameObject RawImage = GameObject.Find("RawImage");
        //RawImage.active = true;

        mRtcEngine.EnableLocalVideo(true);
        //mRtcEngine.EnableVideo();

        RawImage img = go.AddComponent<RawImage>();
        img.rectTransform.position = new Vector3(900,900,0);
        img.rectTransform.sizeDelta = new Vector2(250,250);
        img.rectTransform.Rotate(new Vector3(0,0,-180));
        //make the object draggable
        go.AddComponent<UIElementDragger>();
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            go.transform.parent = canvas.transform;
        }
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        //this was added
        go.AddComponent<UIElementDragger>();
        Debug.Log("vid on");
      }
      else
      {
        mRtcEngine.EnableLocalVideo(false);
        //mRtcEngine.DisableVideo();
        //GameObject go = GameObject.Find("RawImage");
        GameObject go = GameObject.Find("RawImage");
        Debug.Log(test);
        //GameObject go1 = GameObject.Find(test);
        //RawImage.active = false;
        //Object.Destroy(go1);
        Object.Destroy(go);
        Debug.Log("vid off");
      }
    }

    public void turnMic(bool OnOffButton)
    {
      //mRtcEngine.DisableVideoObserver();
      if (OnOffButton == true)
      {
        mRtcEngine.EnableLocalAudio(true);
        Debug.Log("audio on");
      }
      else
      {
        mRtcEngine.EnableLocalAudio(false);
        Debug.Log("audio off");
      }
    }
    public void leaveMeeting()
    {
      Debug.Log("Leaving meeting");

      if(mRtcEngine == null)
      {
        //Debug.Log("Engine needes to be initializated before leaving a channel");
        return;
      }
      //leaving meeting
      mRtcEngine.LeaveChannel();
      //remove the video observer
      mRtcEngine.DisableVideoObserver();
      //Debug.Log("check 1");
      //unloadEngine();

    }

    public void unloadEngine()
    {
      Debug.Log("Unloading Agora Engine");

      //delete
      if(mRtcEngine != null)
      {
        IRtcEngine.Destroy();
        mRtcEngine = null;
      }
      //Debug.Log("check 2");
    }

    public void addCamera()
    {
        // Attach the SDK Script VideoSurface for video rendering
        //GameObject go = GameObject.Find("GameController");
        GameObject video = GameObject.Find("RawImage");

        if (ReferenceEquals(video, null))
        {
            Debug.Log("BBBB: failed to find video");
            return;
        }
        else
        {
            video.AddComponent<VideoSurface>();
        }

    }

    public void EnableVideo(bool pauseVideo)
    {
      if (mRtcEngine != null)
      {
          if (!pauseVideo)
          {
              mRtcEngine.EnableVideo();
          }
          else
          {
              mRtcEngine.DisableVideo();
          }
      }
   }

    // implement engine callbacks
    private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);

    }

    // When a remote user joined, this delegate will be called. Typically
    // create a GameObject to render video on it
    private void onUserJoined(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);
        // this is called in main thread

        // find a game object to render video stream from 'uid'
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            return; // reuse
        }
        test = uid.ToString();
        // create a GameObject and assign to this new user
        VideoSurface videoSurface = makeImageSurface(uid.ToString());
        if (!ReferenceEquals(videoSurface, null))
        {
            // configure videoSurface
            videoSurface.SetForUser(uid);
            videoSurface.SetEnable(true);
            videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            videoSurface.SetGameFps(30);
        }

    }

    private const float Offset = 100;
    public VideoSurface makeImageSurface(string goName)
    {
        GameObject go = new GameObject();

        if (go == null)
        {
            return null;
        }

        go.name = goName;

        // to be renderered onto
        RawImage img = go.AddComponent<RawImage>();
        img.rectTransform.position = new Vector3(900,900,0);
        img.rectTransform.sizeDelta = new Vector2(250,250);
        img.rectTransform.Rotate(new Vector3(0,0,-180));
        //Color color;
        //color = new Color(0,0,0,255);
        //img.color = new Color(0.0f ,0.0f ,0.0f ,0.0f);
        //go.active = false;

        // make the object draggable
        go.AddComponent<UIElementDragger>();
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            go.transform.parent = canvas.transform;
        }

        // configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        return videoSurface;
    }

    // When remote user is offline, this delegate will be called. Typically
    // delete the GameObject for this user
    private void onUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        // remove video stream
        Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
        // this is called in main thread
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            Object.Destroy(go);
        }
    }


    #region Error Handling
    private int LastError { get; set; }
    private void HandleError(int error, string msg)
    {
        if (error == LastError)
        {
            return;
        }

        msg = string.Format("Error code:{0} msg:{1}", error, IRtcEngine.GetErrorDescription(error));

        switch (error)
        {
            case 101:
                msg += "\nPlease make sure your AppId is valid and it does not require a certificate for this demo.";
                break;
        }

        Debug.LogError(msg);
        if (MessageText != null)
        {
            if (MessageText.text.Length > 0)
            {
                msg = "\n" + msg;
            }
            MessageText.text += msg;
        }

        LastError = error;
    }

    #endregion


}
