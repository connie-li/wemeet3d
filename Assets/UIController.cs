using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public UnityEngine.UI.InputField pwInputJoin = null;
    public UnityEngine.UI.InputField pwInputCreate = null;

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
}