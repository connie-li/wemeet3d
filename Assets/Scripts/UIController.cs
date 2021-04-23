using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public UnityEngine.UI.InputField pwInputJoin = null;
    public UnityEngine.UI.InputField pwInputCreate = null;

    //public UnityEngine.UI.InputField createMeeting;

    //public static string textval;
    //textval = createMeeting.text;

    public void ToggleInputTypeJoinPW()
    {
        if (this.pwInputJoin != null)
        {
            if (this.pwInputJoin.contentType == InputField.ContentType.Password)
            {
                this.pwInputJoin.contentType = InputField.ContentType.Standard;
            }
            else
            {
                this.pwInputJoin.contentType = InputField.ContentType.Password;
            }

            this.pwInputJoin.ForceLabelUpdate();
        }
    }

    public void ToggleInputTypeCreatePW()
    {
        if (this.pwInputCreate != null)
        {
            if (this.pwInputCreate.contentType == InputField.ContentType.Password)
            {
                this.pwInputCreate.contentType = InputField.ContentType.Standard;
            }
            else
            {
                this.pwInputCreate.contentType = InputField.ContentType.Password;
            }

            this.pwInputCreate.ForceLabelUpdate();
        }
    }

    public UnityEngine.UI.Text m_roomCode;
    public UnityEngine.UI.Text m_password;
    public void copyToClipboard()
    {
        string roomCode;
        string password;
        
        if (this.m_roomCode.text != null)
        {
            GUIUtility.systemCopyBuffer = this.m_roomCode.text;
            roomCode = GUIUtility.systemCopyBuffer;
        }
        else
        {
            roomCode = "ERROR, no room code generated.";
        }

        if (this.m_password.text != null)
        {
            GUIUtility.systemCopyBuffer = this.m_password.text;
            password = GUIUtility.systemCopyBuffer;
        }
        else
        {
            password = "ERROR, no password generated.";
        }

        string invitation = $"You are invited to a WeMeet3D meeting!  Use the following login details to join the meeting.\n\nRoom code: {roomCode}\nPassword: {password}";
        GUIUtility.systemCopyBuffer = invitation;
    }
}
