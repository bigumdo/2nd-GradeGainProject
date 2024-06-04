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
    
    public void SellItem(WeaponSO item)
    {
        if (items.Contains(item) == false) return;
        if (items.Count == 0) return;
        items.Remove(item);
        gold += item.price;
    }

    public void Hammering(float clikMoney)
    {
        gold += (int)clikMoney;
    }

}
