using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectWeaponItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private Ease ease;
    [SerializeField] private bool isInventoryItem;
    
    private RectTransform _rect;
    private Transform _canvas;
    private RectTransform _parent;

    public WeaponSO WeaponSo
    {
        get => weaponSO;
        set => weaponSO = value;
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
        _canvas = transform.root;
        if (!isInventoryItem)
            _parent = _canvas.Find("WeaponChoicePanel") as RectTransform;
    }

    private void OnValidate()
    {
        SetWeaponItem();
    }

    public void SetWeaponItem()
    {
        if (weaponSO == null) return;
        weaponImage.sprite = weaponSO.weaponSprite;
        weaponNameText.text = weaponSO.weaponName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rect.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f).SetEase(ease);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInventoryItem)
        {
            GameManager.Instance.nowWeapon = weaponSO;
        }
        else
        {
        }
        
        //UIManager로 바꾸기
        _parent.DOMoveX(-860f, 0.5f).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rect.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(ease);
    }
}
