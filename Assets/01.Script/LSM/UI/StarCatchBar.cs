using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] hitTrm;
    private RectTransform selectHitTrm;

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
        for (int j = 0; j < selectHitTrm.childCount; ++j)
        {
            if (Mathf.Abs(selectHitTrm.GetChild(j).transform.position.x -
                point.position.x) < 50)
            {
                //Debug.Log(selectHitTrm.GetComponentInChildren<RectTransform>[1]().name);
                Debug.Log(1);
            }
        }
    }

}


