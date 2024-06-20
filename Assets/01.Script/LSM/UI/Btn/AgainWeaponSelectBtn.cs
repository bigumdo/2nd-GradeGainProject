using UnityEngine;
using DG.Tweening;

public class AgainWeaponSelectBtn : BaseBtn
{

    private Transform _parent;

    public override void Awake()
    {
        base.Awake();
        _parent = transform.root.Find("WeaponChoicePanel") as RectTransform;
    }

    protected override void PointClick()
    {
        base.PointClick();
        UIManager.Instance._produceResetPanel.SetActive(false);
        _parent.DOMoveX(0, 0.5f).SetEase(ease);
        UIManager.Instance.startCatchCanvasGroup.alpha = 0;
        UIManager.Instance.startCatchPanel.StartCatchBar.ResetSelectBar();
    }

    protected override void PointEnter()
    {
        base.PointEnter();
    }

    protected override void PointExit()
    {
        base.PointExit();
    }
}
