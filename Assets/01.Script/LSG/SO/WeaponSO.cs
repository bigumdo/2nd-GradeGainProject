using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    public int rank;
    public string weaponName;
    public string inventoryWeaponName;
    public int damage;
    public int price;
    public float nextUpgradePercent;
    public float breakPercent;
    public int starCatchSpeed;
    public Vector2 starCatchSize;
    public Sprite weaponSprite;
    public GameObject weaponPrefab;
}