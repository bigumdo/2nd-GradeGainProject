using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private WeaponSO _nowWeapon;
    [SerializeField] private WeaponTreeSO _weaponTree;
    private int _weaponPower = 0;
    
    private void Start()
    {
        _nowWeapon = _weaponTree.items[0];
    }

    [ContextMenu("Upgrade Weapon")]
    public void UpgradeWeapon()
    {
        if (_weaponPower == _weaponTree.items.Count - 1)
        {
            Debug.Log("Max level reached!");
            return;
        }
        float random = Random.Range(0, 100);
        float breakRandom = Random.Range(0, 100);
        
        if (random < _nowWeapon.nextUpgradePercent)
        {
            _weaponPower++;
            _nowWeapon = _weaponTree.items[_weaponPower];
            Debug.Log("<color=green>Weapon upgraded!</color>");
            Debug.Log($"<color=green>Now weapon is {_nowWeapon.weaponName}!</color>");
        }
        else
        {
            if (breakRandom < _nowWeapon.breakPercent)
            {
                _weaponPower = 0;
                _nowWeapon = _weaponTree.items[_weaponPower];
                Debug.Log("<color=red>Weapon broken!</color>");
            }
            else
            {
                Debug.Log("<color=blue>Weapon upgrade failed!</color>");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeWeapon();
        }
    }
}
