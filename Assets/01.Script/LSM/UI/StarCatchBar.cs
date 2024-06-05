using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] hitTrm;
    private RectTransform selectHitTrm;
    private bool _isCatch;
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

    public void Hitpoint(Transform point)
    {
        for (int i = 0; i < selectHitTrm.childCount; ++i)
        {
            if (Mathf.Abs(selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 50&& 
                selectHitTrm.gameObject.activeSelf
                &&selectHitTrm.GetChild(i).gameObject.activeSelf)
            {
                selectHitTrm.GetChild(i).gameObject.SetActive(false);
                
                StartCoroutine(SuccessStartcatch());
                //SuccessStartcatch();
            }
        }
    }

    private IEnumerator SuccessStartcatch()
    {
        _trueCatchPointCnt++;
        GameManager.Instance.player.GetComponent<Hammer>().HammerStarCatch();
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


