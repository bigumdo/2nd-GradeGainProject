using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendGameData
{
    #region Singleton
    private static BackendGameData _instance = null;

    public static BackendGameData Instance 
    {
        get 
        {
            if(_instance == null) 
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }
    #endregion

    public static UserData userData;
    
    private string gameDataRowInDate = string.Empty;
    
    public void GameDataInsert() 
    {
        if (userData == null) 
        {
            userData = new UserData();
        }

        Debug.Log("데이터 초기화");
        userData.songName = "Test";
        userData.accuracy = 100f;
        
        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
        Param param = new Param();
        param.Add("songName", userData.songName);
        param.Add("accuracy", userData.accuracy);
        
        Debug.Log("뒤끝 업데이트 요청");
        BackendReturnObject bro = Backend.GameData.Insert("PLAY_DATA", param);
        
        if (bro.IsSuccess()) 
        {
            Debug.Log($"뒤끝 업데이트 성공 | {bro}");
            gameDataRowInDate = bro.GetInDate();
        }
        else 
        {
            Debug.Log($"뒤끝 업데이트 실패 | {bro}");
        }
    }
    
    public void GameDataGet() 
    {
        Debug.Log("뒤끝 데이터 조회 요청");
        BackendReturnObject bro = Backend.GameData.GetMyData("PLAY_DATA", new Where());
        
        if(bro.IsSuccess()) {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if(gameDataJson.Count <= 0) {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            } else {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임 정보의 고유값입니다.  

                userData = new UserData();
                
                userData.songName = gameDataJson[0]["songName"].ToString();
                userData.accuracy = float.Parse(gameDataJson[0]["accuracy"].ToString());

                Debug.Log(userData.ToString());
            }
        } else {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }
    
    public void LevelUp()
    {
        userData.songName = "WASANS";
        userData.accuracy += 10f;
    }
    
    public void GameDataUpdate() 
    {
        if (userData == null) 
        {
            Debug.LogError("데이터가 없습니다.");
            return;
        }
        
        Param param = new Param();
        param.Add("songName", userData.songName);
        param.Add("accuracy", userData.accuracy);

        BackendReturnObject bro = null;
        
        if (string.IsNullOrEmpty(gameDataRowInDate)) 
        {
            Debug.Log("최신 게임 정보 데이터 수정 요청");
            bro = Backend.GameData.Update("PLAY_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정 요청");
            bro = Backend.GameData.UpdateV2("PLAY_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }
        
        if (bro.IsSuccess()) 
        {
            Debug.Log($"뒤끝 업데이트 성공 | {bro}");
        }
        else 
        {
            Debug.Log($"뒤끝 업데이트 실패 | {bro}");
        }
    }
}
