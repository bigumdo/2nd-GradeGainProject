using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] _hitTrm;
    private StarCatchCanvas _startCatchCanvas;
    private RectTransform _selectHitTrm;
    private int _trueCatchPointCnt;

    private void OnEnable()
    {
        StarCatchBarChange();
    }

    private void Awake()
    {
        _startCatchCanvas = GetComponentInParent<StarCatchCanvas>();
    }

    public void StarCatchBarChange()
    {
        int rand = Random.Range(0, _hitTrm.Length);
        for (int i = 0; i < _hitTrm.Length; ++i)
        {
            _hitTrm[i].gameObject.SetActive(false);
        }
        _hitTrm[rand].gameObject.SetActive(true);
        _selectHitTrm = _hitTrm[rand];
    }

    public SuccessEnum Hitpoint(Transform point)
    {
        GameManager.Instance.player.GetComponent<Hammer>().HammerStarCatch();//돈과 애니메이션
        for (int i = 0; i < _selectHitTrm.childCount; i++)
        {
            if(_selectHitTrm.gameObject.activeSelf
                && _selectHitTrm.GetChild(i).gameObject.activeSelf)
            {
                if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 50)
                {
                    _selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    StartCoroutine(SuccessStartcatch());
                    StartCoroutine(_startCatchCanvas.ResultText(_selectHitTrm.GetChild(i).transform, "Success"));
                    return SuccessEnum.GreatSuccess;
                }
                else if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 75)
                {
                    _selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    StartCoroutine(SuccessStartcatch());
                    StartCoroutine(_startCatchCanvas.ResultText(_selectHitTrm.GetChild(i).transform, "Normal"));
                    return SuccessEnum.NormalSuccess;
                }
                else
                {
                    StartCoroutine(_startCatchCanvas.ResultText(new Vector3(_startCatchCanvas.Point.transform.position.x,
                        _selectHitTrm.GetChild(i).transform.position.y,
                        _startCatchCanvas.Point.transform.position.z), "Fall"));
                    return SuccessEnum.Fail;
                }

            }
            //else
            //    return SuccessEnum.Fail;
        }
        return SuccessEnum.Fail;
    }

    private IEnumerator SuccessStartcatch()
    {
        _trueCatchPointCnt++;
        if (_trueCatchPointCnt == _selectHitTrm.childCount)
        {
            for (int j = 0; j < _selectHitTrm.childCount; ++j)
            {
                _selectHitTrm.GetChild(j).gameObject.SetActive(true);
            }
            _trueCatchPointCnt = 0;
            _selectHitTrm.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            StarCatchBarChange();
        }
    }

}


