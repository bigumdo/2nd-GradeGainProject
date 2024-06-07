using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] hitTrm;
    private RectTransform selectHitTrm;
    private int _trueCatchPointCnt;

    private void OnEnable()
    {
        StarCatchBarChange();
    }

    public void StarCatchBarChange()
    {
        int rand = Random.Range(0, hitTrm.Length);
        for (int i = 0; i < hitTrm.Length; ++i)
        {
            hitTrm[i].gameObject.SetActive(false);
        }
        hitTrm[rand].gameObject.SetActive(true);
        selectHitTrm = hitTrm[rand];
    }

    public SuccessEnum Hitpoint(Transform point)
    {
        GameManager.Instance.player.GetComponent<Hammer>().HammerStarCatch();//돈과 애니메이션
        for (int i = 0; i < selectHitTrm.childCount; i++)
        {
            if(selectHitTrm.gameObject.activeSelf
                && selectHitTrm.GetChild(i).gameObject.activeSelf)
            {
                if (Mathf.Abs(selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 50)
                {
                    selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    StartCoroutine(SuccessStartcatch());
                    return SuccessEnum.GreatSuccess;
                }
                else if (Mathf.Abs(selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 75)
                {
                    selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    StartCoroutine(SuccessStartcatch());
                    return SuccessEnum.NormalSuccess;
                }
                else
                    return SuccessEnum.Fail;

            }
            //else
            //    return SuccessEnum.Fail;
        }
        return SuccessEnum.Fail;
    }

    private IEnumerator SuccessStartcatch()
    {
        _trueCatchPointCnt++;
        if (_trueCatchPointCnt == selectHitTrm.childCount)
        {
            for (int j = 0; j < selectHitTrm.childCount; ++j)
            {
                selectHitTrm.GetChild(j).gameObject.SetActive(true);
            }
            _trueCatchPointCnt = 0;
            selectHitTrm.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            StarCatchBarChange();
        }
    }

}


