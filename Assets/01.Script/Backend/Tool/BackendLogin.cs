using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TMPro;
using UnityEngine;

public class BackendLogin
{
    #region Singleton
    private static BackendLogin _instance = null;
    public static BackendLogin Instance 
    {
        get 
        {
            if(_instance == null) 
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }
    #endregion
    
    /// <summary>
    /// 회원가입
    /// </summary>
    public void CustomSignUp() 
    {
        string id = UIManager.Instance.idInputFieldForSignUp.text;
        string pw = UIManager.Instance.pwInputFieldForSignUp.text;
        Debug.Log("회원가입 요청");
        var bro = Backend.BMember.CustomSignUp(id, pw);
        
        if (bro.IsSuccess())
        {
            Debug.Log($"회원가입 성공: {bro}");
        }
        else
        {
            Debug.Log($"회원가입 실패: {bro}");
        }
    }

    /// <summary>
    /// 로그인
    /// </summary>
    public void CustomLogin() 
    {
        Debug.Log("로그인 요청");
        string id = UIManager.Instance.idInputFieldForLogin.text;
        string pw = UIManager.Instance.pwInputFieldForLogin.text;
        var bro = Backend.BMember.CustomLogin(id, pw);
        
        if (bro.IsSuccess())
        {
            Debug.Log($"로그인 성공: {bro}");
        }
        else
        {
            Debug.Log($"로그인 실패: {bro}");
        }
    }

    /// <summary>
    /// 닉네임 변경
    /// </summary>
    public void UpdateNickname() 
    {
        Debug.Log("닉네임 변경 요청");
        string nickname = UIManager.Instance.nicknameInputField.text;
        var bro = Backend.BMember.UpdateNickname(nickname);
        
        if (bro.IsSuccess())
        {
            Debug.Log($"닉네임 변경 성공: {bro}");
        }
        else
        {
            Debug.Log($"닉네임 변경 실패: {bro}");
        }
    }
}
