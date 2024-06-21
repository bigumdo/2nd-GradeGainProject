using System;
using ButtonAttribute;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _hideBtn;

    private WeaponSO weapon;
    private bool _isActiveShap;

    private void Start()
    {
        _sellButton.onClick.AddListener(Sell);
        _hideBtn.onClick.AddListener(HideShop);
    }
    
    public void Activate(bool value)
    {
        if (value)
        {
            transform.DOMoveX(1930f, 0.5f).SetEase(Ease.InOutBack);
            _isActiveShap = true;
            Inventory.Instance.ShowInventory();
        }
        else
        {
            transform.DOMoveX(2780f, 0.5f).SetEase(Ease.InOutBack);
            _isActiveShap = false;
            Inventory.Instance.HideInventory();
        }
    }
    
    [InspectorButton("ShowShop", 10)]
    public void ShowShop()
    {
        Activate(true);
    }
    
    [InspectorButton("HideShop", 10)]
    public void HideShop()
    {
        Activate(false);
    }
    
    private void Update()
    {
        if (_parent.childCount > 0)
        {
            SetText();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!_isActiveShap)
            {
                Activate(true);
            }
            else
            {
                Activate(false);
            }
        }
    }

    private void SetText()
    {
         weapon = _parent.GetChild(0).GetComponent<SelectWeaponItem>()?.WeaponSo;
         if (weapon == null) return;
        _nameText.text = weapon.weaponName;
        _priceText.text = weapon.GetPrice();
    }
    
    public void Sell()
    {
        if (weapon == null) return;
        if (_parent.childCount == 0) return;
        Inventory.Instance.AddCoin(weapon.price);
        Destroy(_parent.GetChild(0).gameObject);
        weapon = null;
    }
}
