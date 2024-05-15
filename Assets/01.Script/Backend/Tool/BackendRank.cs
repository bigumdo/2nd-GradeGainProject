using System.Collections;
using System.Collections.Generic;
using System.Text;
using BackEnd;
using UnityEngine;

public class BackendRank
{
    #region Singleton
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
    #endregion
    
    /// <summary>
    /// 랭킹 삽입
    /// </summary>
    /// <param name="score">넣을 점수</param>
    public void RankInsert(int score)
    {
        string rankUUID = "<랭킹을 만들고 거기서 UUID를 적어야함>";

        string tableName = "<랭킹 테이블 이름>";
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

    /// <summary>
    /// 랭킹 전부 조회
    /// </summary>
    public void RankGet() 
    {
        string rankUUID = "<랭킹을 만들고 거기서 UUID를 적어야함>";
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
