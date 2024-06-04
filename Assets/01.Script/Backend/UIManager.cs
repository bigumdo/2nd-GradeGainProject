using System;
using TMPro;
using UnityEngine;

namespace backend
{
    public class UIManager : MonoSingleton<UIManager>
    {

        public TMP_InputField idInputFieldForSignUp;
        public TMP_InputField pwInputFieldForSignUp;
        public TMP_InputField idInputFieldForLogin;
        public TMP_InputField pwInputFieldForLogin;
        public TMP_InputField nicknameInputField;
        public TMP_InputField scoreInputField;
    }
}


