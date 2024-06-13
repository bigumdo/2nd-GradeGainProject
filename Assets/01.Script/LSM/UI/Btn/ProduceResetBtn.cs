using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceResetBtn : BaseBtn
{
    protected override void PointClick()
    {
        base.PointClick();
        UIManager.Instance.startCatchPanel.ProductionSet(GameManager.Instance.nowWeapon);
        UIManager.Instance._produceResetPanel.SetActive(false);
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
