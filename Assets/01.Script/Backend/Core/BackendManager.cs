using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    private void Start()
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

    public async void SignUp()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomSignUp();
            Debug.Log("SignUp");
        });
    }
    
    public async void Login()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.CustomLogin();
            Debug.Log("Login");
        });
    }
    
    public async void UpdateNickname()
    {
        await Task.Run(() =>
        {
            BackendLogin.Instance.UpdateNickname();
            Debug.Log("UpdateNickname");
        });
    }
    
    
    public async void GameDataInsert()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataInsert();
            Debug.Log("GameDataInsert");
        });
    }
    
    public async void GameDataGet()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataGet();
            Debug.Log("GameDataGet");
        });
    }
    
    public async void LevelUp()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.LevelUp();
            Debug.Log("LevelUp");
        });
    }
    
    public async void GameDataUpdate()
    {
        await Task.Run(() =>
        {
            BackendGameData.Instance.GameDataUpdate();
            Debug.Log("GameDataUpdate");
        });
    }
    
    public async void RankInsert()
    {
        await Task.Run(() =>
        {
            BackendRank.Instance.RankInsert(int.Parse(UIManager.Instance.scoreInputField.text));
            Debug.Log("RankInsert");
        });
    }
    
    public async void RankGet()
    {
        await Task.Run(() =>
        {
            BackendRank.Instance.RankGet();
            Debug.Log("RankGet");
        });
    }
}
