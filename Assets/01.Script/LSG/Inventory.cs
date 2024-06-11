using System.Collections.Generic;
using ButtonAttribute;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoSingleton<Inventory>
{
    public List<WeaponSO> items = new();
    public int gold = 0;

    [SerializeField] private Transform _itemParent;
    [SerializeField] private SelectWeaponItem _itemPrefab;

    public void AddItem()
    {
        items.Add(WeaponUpgradeManager.Instance.NowWeapon);
        WeaponUpgradeManager.Instance.NowWeapon = WeaponUpgradeManager.Instance.WeaponTree.items[0];
        WeaponUpgradeManager.Instance.WeaponPower = 0;
    }
    
    public void SellItem()
    {
        if (items.Count == 0) return;
        gold += items[^1].price;
        items.Remove(items[^1]);
    }
    
    [InspectorButton("ShowInventory", 10)]
    public void ShowInventory()
    {
        transform.DOMoveX(0f, 0.5f).SetEase(Ease.InOutBack);
        foreach (Transform child in _itemParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            SelectWeaponItem itemObject = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            itemObject.transform.SetParent(_itemParent);
            itemObject.WeaponSo = item;
            itemObject.SetWeaponItem();
        }
    }

}
