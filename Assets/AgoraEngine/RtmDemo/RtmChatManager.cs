using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using agora_rtm;

namespace io.agora.rtm.demo
{
    public class RtmChatManager : MonoBehaviour
    {
#pragma warning disable 0649

        [Header("Agora Properties")]
        [SerializeField]
        private string appId = "";
        [SerializeField]
        private string token = "";

        [Header("Application Properties")]
        // put absolute path like /Users/yournameonmac/Downloads/mono-boad.jpg  in the Inspector

        [SerializeField] InputField userNameInput, channelNameInput;
        [SerializeField] InputField channelMsgInputBox;
        [SerializeField] MessageDisplay messageDisplay;
        [SerializeField] GameObject roomCode;

#pragma warning restore 0649

        private RtmClient rtmClient = null;
        private RtmChannel channel;
        private RtmCallManager callManager;

        private RtmClientEventHandler clientEventHandler;
        private RtmChannelEventHandler channelEventHandler;
        private RtmCallEventHandler callEventHandler;

        //channelNameInput.text = roomCode.text;
        string _userName = "";
        string UserName {
            get { return _userName; }
            set {
                _userName = value;
                PlayerPrefs.SetString("RTM_USER", _userName);
                PlayerPrefs.Save();
            }
        }

        string _channelName = "";
        string ChannelName
        {
            get { return _channelName; }
            set {
                _channelName = value;
                PlayerPrefs.SetString("RTM_CHANNEL", _channelName);
                PlayerPrefs.Save();
            }
        }

        agora_rtm.SendMessageOptions _MessageOptions = new agora_rtm.SendMessageOptions() {
                    enableOfflineMessaging = true,
                    enableHistoricalMessaging = true
	    };

        private void Awake()
        {
            userNameInput.text = PlayerPrefs.GetString("RTM_USER", "");
            channelNameInput.text = PlayerPrefs.GetString("RTM_CHANNEL", "");
        }

        // Start is called before the first frame update
        void Start()
        {
            clientEventHandler = new RtmClientEventHandler();
            channelEventHandler = new RtmChannelEventHandler();
            callEventHandler = new RtmCallEventHandler();

            rtmClient = new RtmClient(appId, clientEventHandler);

            clientEventHandler.OnLoginSuccess = OnClientLoginSuccessHandler;
            clientEventHandler.OnLoginFailure = OnClientLoginFailureHandler;

            channelEventHandler.OnJoinSuccess = OnJoinSuccessHandler;
            channelEventHandler.OnJoinFailure = OnJoinFailureHandler;
            channelEventHandler.OnLeave = OnLeaveHandler;
            channelEventHandler.OnMessageReceived = OnChannelMessageReceivedHandler;

            // Optional, tracking members
            channelEventHandler.OnMemberJoined = OnMemberJoinedHandler;
            channelEventHandler.OnMemberLeft = OnMemberLeftHandler;



            callManager = rtmClient.GetRtmCallManager(callEventHandler);
            // state
            clientEventHandler.OnConnectionStateChanged = OnConnectionStateChangedHandler;

        }

        void OnApplicationQuit()
        {
            if (channel != null)
            {
                channel.Dispose();
                channel = null;
            }
            if (rtmClient != null)
            {
                rtmClient.Dispose();
                rtmClient = null;
            }
        }

        #region Button Events
        public void Login()
        {
            UserName = userNameInput.text;

            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(appId))
            {
                Debug.LogError("We need a username and appId to login");
                return;
            }

            rtmClient.Login(token, UserName);
        }

        public void Logout()
        {
            //messageDisplay.AddTextToDisplay(UserName + " logged out of the rtm", Message.MessageType.Info);
            rtmClient.Logout();
        }


        public void JoinChannel()
        {
            ChannelName = roomCode.GetComponent<Text>().text;
            //channelNameInput.GetComponent<InputField>().text;
            //Debug.Log(ChannelName);
            channel = rtmClient.CreateChannel(ChannelName, channelEventHandler);
            ShowCurrentChannelName();
            UserName = userNameInput.GetComponent<InputField>().text;
            Debug.Log(UserName);
            channel.Join();
        }

        public void LeaveChannel()
        {
            messageDisplay.AddTextToDisplay(UserName + " left the chat", Message.MessageType.Info);
            channel.Leave();
        }

        public void SendMessageToChannel()
        {
            string msg = channelMsgInputBox.text;
            string peer = "[channel:" + ChannelName + "]";

            string displayMsg = string.Format("{0}->{1}: {2}", UserName, peer, msg);

            messageDisplay.AddTextToDisplay(displayMsg, Message.MessageType.PlayerMessage);
            channel.SendMessage(rtmClient.CreateMessage(msg));
        }

        #endregion


        void ShowCurrentChannelName()
        {
            ChannelName = roomCode.GetComponent<Text>().text;
            Debug.Log("Channel name is " + ChannelName);
        }

        #region EventHandlers


        void OnJoinSuccessHandler(int id)
        {
            string msg = "channel:" + ChannelName + " OnJoinSuccess id = " + id;
            Debug.Log(msg);
        }

        void OnJoinFailureHandler(int id, JOIN_CHANNEL_ERR errorCode)
        {
            string msg = "channel OnJoinFailure  id = " + id + " errorCode = " + errorCode;
            Debug.Log(msg);
        }

        void OnClientLoginSuccessHandler(int id)
        {
            string msg = "client login successful! id = " + id;
            Debug.Log(msg);
        }

        void OnClientLoginFailureHandler(int id, LOGIN_ERR_CODE errorCode)
        {
            string msg = "client login unsuccessful! id = " + id + " errorCode = " + errorCode;
            Debug.Log(msg);
        }

        void OnLeaveHandler(int id, LEAVE_CHANNEL_ERR errorCode)
        {
            string msg = "client onleave id = " + id + " errorCode = " + errorCode;
            Debug.Log(msg);
        }

        void OnChannelMessageReceivedHandler(int id, string userId, TextMessage message)
        {
            Debug.Log("client OnChannelMessageReceived id = " + id + ", from user:" + userId + " text:" + message.GetText());
              messageDisplay.AddTextToDisplay(userId + ": " + message.GetText(), Message.MessageType.ChannelMessage);
        }

        void OnMemberJoinedHandler(int id, RtmChannelMember member)
        {
            string msg = "channel OnMemberJoinedHandler member ID=" + member.GetUserId() + " channelId = " + member.GetChannelId();
            Debug.Log(msg);
            messageDisplay.AddTextToDisplay(msg, Message.MessageType.Info);
        }

        void OnMemberLeftHandler(int id, RtmChannelMember member)
        {
            string msg = "channel OnMemberLeftHandler member ID=" + member.GetUserId() + " channelId = " + member.GetChannelId();
            Debug.Log(msg);
            messageDisplay.AddTextToDisplay(msg, Message.MessageType.Info);
        }



        void OnConnectionStateChangedHandler(int id, CONNECTION_STATE state, CONNECTION_CHANGE_REASON reason)
        {
            string msg = string.Format("connection state changed id:{0} state:{1} reason:{2}", id, state, reason);
            Debug.Log(msg);
        }

        #endregion
    }

}
