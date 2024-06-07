using System.Collections.Generic;   

public class Inventory : MonoSingleton<Inventory>
{
    public List<WeaponSO> items = new();
    public int gold = 0;

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


}
