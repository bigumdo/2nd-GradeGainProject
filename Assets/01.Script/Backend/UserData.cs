using System.Collections.Generic;
using System.Text;

public class UserData 
{
    public string songName;
    public float accuracy;

    public override string ToString() 
    {
        StringBuilder result = new StringBuilder();
        
        result.Append($"정확도: {accuracy}");
        result.Append($"\n곡명: {songName}");
        return result.ToString();
    }
}