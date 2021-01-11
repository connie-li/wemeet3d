using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using agora_gaming_rtc;

public class AgoraInterface : MonoBehaviour
{
    //private static string appId = "5b605d324a8d4fe082de1236e69872af";
    public IRtcEngine mRtcEngine;
    //private string PlaySceneName = "inMeetingScene";

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


    //enabling camera
    public void joinMeeting()
    {
      //enable video
      mRtcEngine.EnableVideo();
      //allow for camera output callback
      mRtcEngine.EnableVideoObserver();
      mRtcEngine.StartPreview();
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
      mRtcEngine.StopPreview();
      //remove the video observer
      mRtcEngine.DisableVideoObserver();
      unloadEngine();

    }

    public void unloadEngine()
    {
      Debug.Log("Unloading Agora Engine");
      if(mRtcEngine != null)
      {
        IRtcEngine.Destroy();
        mRtcEngine = null;
      }
    }


    public void addCamera()
    {
        // Attach the SDK Script VideoSurface for video rendering
        GameObject cube = GameObject.Find("Cube");
        if (ReferenceEquals(cube, null))
        {
            Debug.Log("BBBB: failed to find Cube");
            return;
        }
        else
        {
            cube.AddComponent<VideoSurface>();
        }
    }


}
