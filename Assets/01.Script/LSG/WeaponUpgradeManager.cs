using UnityEngine;

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private WeaponSO _nowWeapon;
    [SerializeField] private WeaponTreeSO _weaponTree;
    private int _weaponPower = 0;
    
    private void Start()
    {
        _nowWeapon = _weaponTree.items[0];
    }

    public void UpgradeWeapon()
    {
        int random = Random.Range(0, 100);

        // 이제 확률에따라 업드레이드 또는 터지는 코드 적으면 됨 끼얏호우
    }
}
