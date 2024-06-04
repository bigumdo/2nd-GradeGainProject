using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using BackEnd;
using backend;

public class BackendManager : MonoBehaviour
{
    /// <summary>
    /// 뒤끝 초기화
    /// </summary>
    private void Init()
    {
        var bro = Backend.Initialize(true);
        
        if (bro.IsSuccess())
        {
            Debug.Log($"Success: {bro}");
        }
        else
        {
            Debug.Log($"Failed: {bro}");
        }
    }

    /// <summary>
    /// 회원 가입
    /// </summary>
    public async void SignUp()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomSignUp();
            Debug.Log("SignUp");
        });
    }
    
    /// <summary>
    /// 로그인
    /// </summary>
    public async void Login()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomLogin();
            Debug.Log("Login");
        });
    }
    
    /// <summary>
    /// 닉네임 설정
    /// </summary>
    public async void UpdateNickname()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.UpdateNickname();
            Debug.Log("UpdateNickname");
        });
    }
    
    /// <summary>
    /// DB에 데이터 추가
    /// </summary>
    public async void GameDataInsert()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataInsert();
            Debug.Log("GameDataInsert");
        });
    }
    
    /// <summary>
    /// DB에서 데이터 가져오기
    /// </summary>
    public async void GameDataGet()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataGet();
            Debug.Log("GameDataGet");
        });
    }
    
    /// <summary>
    /// DB 데이터 업데이트
    /// </summary>
    public async void GameDataUpdate()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataUpdate();
            Debug.Log("GameDataUpdate");
        });
    }
    
    /// <summary>
    /// 랭킹 데이터 추가
    /// </summary>
    public async void RankInsert()
    {
        await Task.Run(() =>
        {
            BackendRank.Instance.RankInsert(int.Parse(backend.UIManager.Instance.scoreInputField.text));
            Debug.Log("RankInsert");
        });
    }
    
    /// <summary>
    /// 랭킹 데이터 전부 가져오기
    /// </summary>
    public async void RankGet()
    {
        await Task.Run(() =>
        {
            BackendRank.Instance.RankGet();
            Debug.Log("RankGet");
        });
    }
}
