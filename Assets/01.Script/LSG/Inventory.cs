using System.Collections;
using System.Collections.Generic;
using ButtonAttribute;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoSingleton<Inventory>
{
    public List<WeaponSO> items = new();
    public int gold = 0;

    [SerializeField] private Transform _itemParent;
    [SerializeField] private SelectWeaponItem _itemPrefab;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private float _moveX;
    [SerializeField] private float _defaultX;

    public void AddItem()
    {
        items.Add(WeaponUpgradeManager.Instance.NowWeapon);
        WeaponUpgradeManager.Instance.NowWeapon = WeaponUpgradeManager.Instance.WeaponTree.items[0];
        WeaponUpgradeManager.Instance.WeaponPower = 0;
    }
    
    public void AddCoin(int coin)
    {
        gold += coin;
        StartCoroutine(CoinAddEffect(coin));
    }

    private IEnumerator CoinAddEffect(int coin)
    {
        RectTransform _goldTextTransform = _goldText.GetComponent<RectTransform>();
        _goldTextTransform.DOAnchorPosX(_moveX, 0.5f).SetEase(Ease.InOutBack);
        int temp = gold - coin;
        while (temp < gold)
        {
            temp++;
            _goldText.text = $"Coin: {temp:n0}";
            yield return null;
        }
        _goldTextTransform.DOAnchorPosX(_defaultX, 0.5f).SetEase(Ease.InOutBack);
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
