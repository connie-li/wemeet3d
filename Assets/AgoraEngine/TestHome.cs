using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
using UnityEngine.Android;
#endif

/// <summary>
///    TestHome serves a game controller object for this application.
/// </summary>
public class TestHome : MonoBehaviour
{

	// Use this for initialization
//	#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
  //  private ArrayList permissionList = new ArrayList();
	//#endif
	static AgoraInterface app = null;
	[SerializeField] InputField username;
	[SerializeField] InputField channelName;
	public GameObject Panel;
	//private string PlaySceneName = "conference-room-new";

	// PLEASE KEEP THIS App ID IN SAFE PLACE
	// Get your own App ID at https://dashboard.agora.io/
	[SerializeField]
	private string AppID = "your_appid";

	void Awake ()
	{
		#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
		permissionList.Add(Permission.Microphone);
		permissionList.Add(Permission.Camera);
		#endif
		AudioListener.volume = 0;

		// keep this alive across scenes
		DontDestroyOnLoad(this.gameObject);
	}

	void Start ()
	{
		 AudioListener.volume = 0;
		 //start engine and turn vid and mic off
		 onJoinButtonClicked();
		 turnOffOnVid(false);
		 turnOffOnMic(false);
		 //go = GameObject.Find("text-chat-panel");
		 Panel.SetActive(false);

	}

    void Update()
    {
			CheckPermissions();
    }

		private void CheckPermissions()
		{
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
				foreach(string permission in permissionList)
				{
						if (!Permission.HasUserAuthorizedPermission(permission))
						{
				Permission.RequestUserPermission(permission);
			}
				}
#endif
		}

	public void onJoinButtonClicked()
	{

		// create app if nonexistent
		if (ReferenceEquals (app, null))
		{

			app = new AgoraInterface(); // create app
			Debug.Log(AppID);
			app.loadEngine(AppID); // load engine
		}

		// join channel
		string channelname = channelName.text;
		app.join(channelname);
	}
	public void turnOffOnVid(bool OnOff)
	{
		app.turnCamera(OnOff);
	}

	public void turnOffOnChat(bool OnOff)
	{
		if (OnOff == true)
		{
			Panel.SetActive(true);
			Debug.Log("Chat on");
		}
		else
		{
			Panel.SetActive(false);
			Debug.Log("Chat off");
		}
	}

	public void turnOffOnMic(bool OnOff)
	{
		app.turnMic(OnOff);
	}

	public void onLeaveButtonClicked()
	{

		if (!ReferenceEquals (app, null))
		{

			app.leaveMeeting(); // leave channel
			app.unloadEngine (); // delete engine
			app = null; // delete app
		}
	}

	public void OnApplicationPause(bool paused)
	{
		if (!ReferenceEquals(app, null))
		{
			app.EnableVideo(paused);
		}
	}

	void OnApplicationQuit()
	{
		if (!ReferenceEquals(app, null))
		{
			Debug.Log("turning off");
			onLeaveButtonClicked();
		}

	}
}
