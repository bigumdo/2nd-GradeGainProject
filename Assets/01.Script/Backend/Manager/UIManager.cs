using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public TMP_InputField idInputFieldForSignUp;
    public TMP_InputField pwInputFieldForSignUp;
    public TMP_InputField idInputFieldForLogin;
    public TMP_InputField pwInputFieldForLogin;
    public TMP_InputField nicknameInputField;
    public TMP_InputField scoreInputField;
}
