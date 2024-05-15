using System.Collections;
using System.Collections.Generic;
using System.Text;
using BackEnd;
using UnityEngine;

public class BackendRank
{
    private static BackendRank _instance = null;

    public static BackendRank Instance 
    {
        get 
        {
            if(_instance == null) 
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }
    
    public void RankInsert(int score)
    {
        string rankUUID = "b2cc2210-11b5-11ef-9836-8723331871d7";

        string tableName = "PLAY_DATA";
        string rowInDate = string.Empty;

        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (!bro.IsSuccess())
        {
            Debug.Log("데이터 불러오기 실패");
            Debug.Log(bro);
            return;
        }
        
        Debug.Log("데이터 불러오기 성공");
        Debug.Log(bro);

        if(bro.FlattenRows().Count > 0) 
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        } 
        else 
        {
            Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            var bro2 = Backend.GameData.Insert(tableName);

            if(!bro2.IsSuccess()) 
            {
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }

            Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);

            rowInDate = bro2.GetInDate();
        }
        
        Debug.Log("내 게임 정보의 rowInDate : " + rowInDate); 

        Param param = new Param();
        param.Add("accuracy", score);
 
        Debug.Log("랭킹 삽입을 시도합니다.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if(rankBro.IsSuccess() == false) 
        {
            Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
            return;
        }

        Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
    }

    public void RankGet() 
    {
        string rankUUID = "b2cc2210-11b5-11ef-9836-8723331871d7";
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if(bro.IsSuccess() == false) {
            Debug.LogError("랭킹 조회중 오류가 발생했습니다. : " + bro);
            return;
        }
        Debug.Log("랭킹 조회에 성공했습니다. : " + bro);

        Debug.Log("총 랭킹 등록 유저 수 : " + bro.GetFlattenJSON()["totalCount"].ToString());

        foreach(LitJson.JsonData jsonData in bro.FlattenRows()) {
            StringBuilder info = new StringBuilder();

            info.AppendLine("순위 : " + jsonData["rank"].ToString());
            info.AppendLine("닉네임 : " + jsonData["nickname"].ToString());
            info.AppendLine("점수 : " + jsonData["score"].ToString());
            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("정렬번호 : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}
