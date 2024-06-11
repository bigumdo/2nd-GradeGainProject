using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] _hitTrm;

    private StarCatchPanel _startCatchCanvas;
    private RectTransform _selectHitTrm;
    private int _trueCatchPointCnt;

    private void OnEnable()
    {
        StarCatchBarChange();
    }

    private void Awake()
    {
        _startCatchCanvas = GetComponentInParent<StarCatchPanel>();
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
        SuccessEnum success = SuccessEnum.Fail;
        string successResult = "";
        Vector3 successTrm = Vector3.zero;
        for (int i = 0; i < _selectHitTrm.childCount; i++)
        {
            if(_selectHitTrm.gameObject.activeSelf
                && _selectHitTrm.GetChild(i).gameObject.activeSelf)
            {
                if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 50)
                {
                    _selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    successTrm = _selectHitTrm.GetChild(i).position;
                    successResult = "Success";
                    StartCoroutine(SuccessStartcatch());
                    success =  SuccessEnum.GreatSuccess;
                    break;
                }
                else if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < 100)
                {
                    _selectHitTrm.GetChild(i).gameObject.SetActive(false);
                    successTrm = _selectHitTrm.GetChild(i).position;
                    successResult = "Normal";
                    StartCoroutine(SuccessStartcatch());
                    success = SuccessEnum.NormalSuccess;
                    break;
                }
                else
                {
                    successTrm = new Vector3(_startCatchCanvas.Point.transform.position.x,
                        _selectHitTrm.GetChild(i).transform.position.y,
                        _startCatchCanvas.Point.transform.position.z);
                    successResult = "Fail";
                //    Debug.Log(Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                //point.position.x));
                    success = SuccessEnum.Fail;
                }

            }
        }
        StartCoroutine(_startCatchCanvas.ResultText(successTrm, successResult));
        return success;
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


