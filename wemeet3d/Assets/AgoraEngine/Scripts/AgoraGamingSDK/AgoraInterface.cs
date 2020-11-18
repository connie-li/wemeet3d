using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using agora_gaming_rtc;

public class AgoraInterface : MonoBehaviour
{
    private static string appId = "5b605d324a8d4fe082de1236e69872af";
    public IRtcEngine mRtcEngine;

    //initializating agora RTC engine
    public void loadEngine()
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

    }
}
