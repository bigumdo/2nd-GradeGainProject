using System.Collections.Generic;

public class Inventory : MonoSingleton<Inventory>
{
    public List<WeaponSO> items = new();

    public void AddItem(WeaponSO item)
    {
        items.Add(item);
    }

    public void RemoveItem(WeaponSO item)
    {
        items.Remove(item);
    }
}
