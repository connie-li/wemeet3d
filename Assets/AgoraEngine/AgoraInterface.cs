using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using agora_gaming_rtc;
using agora_utilities;
using UnityEngine.SceneManagement;
using MirrorBasics;
using Mirror;


public class AgoraInterface
{

    public IRtcEngine mRtcEngine;
    private Text MessageText;
    public string uidString;
    uint num;
    public string appID;


   void Start()
   {
     //GameObject go = new GameObject(uidString);
   }

    //initializating agora RTC engine

    public void loadEngine(string appId)
    {
      //starting SDK
      appID = appId;
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
      //mRtcEngine.OnUserJoined = onUserJoined;
      mRtcEngine.OnUserOffline = onUserOffline;

      //ignore warnings
      /*mRtcEngine.OnWarning = (int warn, string msg) =>
      {
          Debug.LogWarningFormat("Warning code:{0} msg:{1}", warn, IRtcEngine.GetErrorDescription(warn));
      };*/
      mRtcEngine.OnError = HandleError;
      //enable video
      mRtcEngine.EnableVideo();
      //allow for camera output callback
      mRtcEngine.EnableVideoObserver();
      // join channel
      mRtcEngine.JoinChannel(channel, null, 0);
      Debug.Log ("initializeEngine done");
    }
    public void turnCamera(bool OnOffButton)
    {

      if (OnOffButton == true)
      {
        //GameObject go = new GameObject(uidString);
        mRtcEngine.EnableLocalVideo(true);
        Player.localPlayer.AddGameObject(uidString);
        //mRtcEngine.EnableVideo();
        Debug.Log("Video on");
      }
      else
      {
        mRtcEngine.EnableLocalVideo(false);
        GameObject go = GameObject.Find(uidString);
        //delete game object
        Player.localPlayer.DeleteGameObject(go,uidString);
        Debug.Log("Video off");
      }
    }

    public void turnCameraOne(bool OnOffButton)
    {
      mRtcEngine.EnableLocalVideo(false);
    }

    public void addObject(string uidString)
    {
      mRtcEngine = IRtcEngine.getEngine(appID);
      //mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
      mRtcEngine.EnableLocalVideo(true);
      VideoSurface videoSurface = makeImageSurface(uidString);
      if (!ReferenceEquals(videoSurface, null))
      {
          // configure videoSurface
          videoSurface.SetForUser(num);
          videoSurface.SetEnable(true);
          videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
          videoSurface.SetGameFps(30);
      }

      //GameObject.Destroy(go);
    }

    public void deleteObject(GameObject go,string uidString)
    {
      go = GameObject.Find(uidString);
      mRtcEngine = IRtcEngine.getEngine(appID);
      //mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
      mRtcEngine.EnableLocalVideo(false);
      GameObject.Destroy(go);
    }

    public void turnMic(bool OnOffButton)
    {
      if (OnOffButton == true)
      {
        mRtcEngine.EnableLocalAudio(true);
        Debug.Log("Audio on");
      }
      else
      {
        mRtcEngine.EnableLocalAudio(false);
        Debug.Log("Audio off");
      }
    }
    public void leaveMeeting()
    {
      Debug.Log("Leaving meeting");

      if(mRtcEngine == null)
      {
        Debug.Log("Engine needes to be initializated before leaving a channel");
        return;
      }
      //leaving meeting
      mRtcEngine.LeaveChannel();
      //remove the video observer
      mRtcEngine.DisableVideoObserver();
      GameObject go = GameObject.Find(uidString);
      Object.Destroy(go);

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
    }

    public void addCamera()
    {
        // Attach the SDK Script VideoSurface for video rendering
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
        uidString = uid.ToString();
        num = uid;
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);

    }

    void OnRemoteVideoStateChanged(uint uid, REMOTE_VIDEO_STATE state, REMOTE_VIDEO_STATE_REASON reason, int elapsed)
    {
        Debug.Log("uid " + uid + " state = " + state + " reason = " + reason);
        GameObject go = GameObject.Find(uid.ToString());
        if (reason == REMOTE_VIDEO_STATE_REASON.REMOTE_VIDEO_STATE_REASON_REMOTE_MUTED) {
            //remoteView.SetEnable(false);
            go.GetComponent<VideoSurface>().SetEnable(false);
	    }
    }
    // When a remote user joined, this delegate will be called. Typically
    // create a GameObject to render video on it
    private void onUserJoined(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);
        //find a game object to render video stream from 'uid'
        /* GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            return; // reuse
        }*/
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
        // make the object draggable
        go.AddComponent<UIElementDragger>();

        if (go == null)
        {
            return null;
        }

        go.name = goName;

        // to be renderered onto
        RawImage img = go.AddComponent<RawImage>();
        NetworkIdentity iden = go.AddComponent<NetworkIdentity>();
        img.rectTransform.position = new Vector3(600,600,0);
        img.rectTransform.sizeDelta = new Vector2(200,200);
        img.rectTransform.Rotate(new Vector3(0,0,-180));
        //img.color = new Color(0 ,0 ,0 ,255);
        GameObject canvas = GameObject.Find("VideoObject");
        if (canvas != null)
        {
            go.transform.SetParent(canvas.transform);
            //go.transform.parent = canvas.transform;
        }
        //go.SetActive(false);
        //go.SetActive(false);

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
