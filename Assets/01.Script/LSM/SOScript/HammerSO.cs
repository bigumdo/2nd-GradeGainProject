using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(menuName ="SO/Click")]
public class HammerSO : ScriptableObject
{

    [System.Serializable]
    public struct a
    {
        public string name;
        public int killCount;
    }
    Dictionary<string, int> aArr = new Dictionary<string, int>();
    public a[] arr;

    private void OnEnable()
    {
        if (aArr.Count == 0)
        {
            for(int i =0;i< arr.Length;++i)
            {
                aArr.Add(arr[i].name, arr[i].killCount);
            }
        }
            
    }

    //public void cout()
    //{
    //    Debug.Log(aArr["ÀÌ»ó±Ô"]);
    //}

    public float clickCoolTime;
    public int oneClickMoney;
    public float changeTime;
    public float animaSpeed;
    public Sprite hammer;

}

