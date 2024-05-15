using System.Collections.Generic;
using System.Text;

/// <summary>
/// 랭킹 넣을 데이터
/// </summary>
public class UserData 
{
    public string songName;
    public float accuracy;

    /// <summary>
    /// 디버그용
    /// </summary>
    /// <returns></returns>
    public override string ToString() 
    {
        StringBuilder result = new StringBuilder();
        
        result.Append($"정확도: {accuracy}");
        result.Append($"\n곡명: {songName}");
        return result.ToString();
    }
}