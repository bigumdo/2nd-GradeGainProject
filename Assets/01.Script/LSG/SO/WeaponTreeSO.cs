using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTree", menuName = "SO/WeaponTree")]
public class WeaponTreeSO : ScriptableObject
{
    public List<WeaponSO> items;
}