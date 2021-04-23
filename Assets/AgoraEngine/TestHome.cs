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
	//public InputField textName;

	// Use this for initialization
//	#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
  //  private ArrayList permissionList = new ArrayList();
	//#endif
	static AgoraInterface app = null;
	//public string stringTheName;
	//public GameObject TextArea1;
	//public TextMeshProUGUI username;
	[SerializeField] InputField username;
	[SerializeField] InputField channelName;
	private string PlaySceneName = "conference-room-new";

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
		//CheckAppId();
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

		// join channel and jump to next scene
		//string channelName = username.text;
		string channelname = channelName.text;
		//"testName";
		//Debug.Log(username.text);
		app.join(channelname);
		//app.addCamera();

		//SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
		//SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
	}
	public void turnOff(bool OnOff)
	{
		app.turnCamera(OnOff);
		//app.unloadEngine ();
		//Destroy(gameObject);
	}

	public void turnOffMic(bool OnOff)
	{
		app.turnMic(OnOff);
		//app.unloadEngine ();
		//Destroy(gameObject);
	}

	public void onLeaveButtonClicked()
	{

		if (!ReferenceEquals (app, null))
		{

			app.leaveMeeting(); // leave channel
			app.unloadEngine (); // delete engine
			app = null; // delete app
			//SceneManager.LoadScene (HomeSceneName, LoadSceneMode.Single);
		}
		GameObject go = GameObject.Find("RawImage");
		Destroy(go);
	}

	public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == PlaySceneName)
		{
			if (!ReferenceEquals (app, null))
			{
				app.addCamera(); // call this after scene is loaded
			}
			SceneManager.sceneLoaded -= OnLevelFinishedLoading;
		}
	}

	public void OnApplicationPause(bool paused)
	{
		if (!ReferenceEquals(app, null))
		{
			app.EnableVideo(paused);
		}
	}

	/*void OnApplicationQuit()
	{
		if (!ReferenceEquals(app, null))
		{
			app.unloadEngine();
		}
	}*/
}
