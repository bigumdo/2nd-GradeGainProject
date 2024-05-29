using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string weaponName;
    public int damage;
    public int price;
    public Sprite weaponSprite;
}