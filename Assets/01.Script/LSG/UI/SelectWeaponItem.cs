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
    
    private RectTransform _rect;

    public WeaponSO WeaponSo
    {
        get => weaponSO;
        set => weaponSO = value;
    }

    private void Awake()
    {
        _rect = transform as RectTransform;
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
        GameManager.Instance.nowWeapon = weaponSO;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rect.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(ease);
    }
}
