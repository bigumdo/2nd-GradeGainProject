using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class BaseBtn : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    [SerializeField] protected Ease ease;

    protected RectTransform _rect;

    public virtual void Awake()
    {
        _rect = transform as RectTransform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PointClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointExit();
    }

    protected virtual void  PointEnter()
    {
        _rect.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f).SetEase(ease);
    }

    protected virtual void PointClick()
    {

    }

    protected virtual void PointExit()
    {
        _rect.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(ease);
    }

}
