using System.Collections;
using UnityEngine;

public class StarCatchBar : MonoBehaviour
{

    public RectTransform[] _hitTrm;

    private StarCatchPanel _startCatchPanel;
    private RectTransform _selectHitTrm;
    private int _trueCatchPointCnt;

    private SuccessEnum currentSuccess = SuccessEnum.Fail;
    private string currentSuccessResult = "";
    private Vector3 currentSuccessTrm = Vector3.zero;

    private void OnEnable()
    {
        StarCatchBarChange();
    }

    private void Awake()
    {
        _startCatchPanel = GetComponentInParent<StarCatchPanel>();
    }

    public void StarCatchBarChange()
    {
        int rand = Random.Range(0, _hitTrm.Length);
        for (int i = 0; i < _hitTrm.Length; ++i)
        {
            _hitTrm[i].gameObject.SetActive(false);
        }
        _startCatchPanel.IsPointStop = true;
        _hitTrm[rand].gameObject.SetActive(true);
        _selectHitTrm = _hitTrm[rand];
    }

    public SuccessEnum Hitpoint(Transform point)
    {
        GameManager.Instance.player.GetComponent<Hammer>().HammerStarCatch();//돈과 애니메이션
        float range = GameManager.Instance.nowWeapon.starCatchSize.x;
        for (int i = 0; i < _selectHitTrm.childCount; i++)
        {
            if(_selectHitTrm.gameObject.activeSelf
                && _selectHitTrm.GetChild(i).gameObject.activeSelf)
            {
                // 강화 성공 범위 체크
                if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < range * 0.5f)
                {
                    SetSuccessEnum(i, _selectHitTrm.GetChild(i).position, "Success", SuccessEnum.GreatSuccess);
                    break;
                }
                else if (Mathf.Abs(_selectHitTrm.GetChild(i).transform.position.x -
                point.position.x) < range)
                {
                    SetSuccessEnum(i, _selectHitTrm.GetChild(i).position, "Normal", SuccessEnum.NormalSuccess);
                    break;
                }
                else
                {
                    Vector3 vec = _startCatchPanel.Point.transform.position;
                    currentSuccessTrm = new Vector3(vec.x,_selectHitTrm.GetChild(i).transform.position.y,vec.z);
                    currentSuccessResult = "Fail";
                    currentSuccess = SuccessEnum.Fail;
                }

            }
        }
        StartCoroutine(_startCatchPanel.ResultText(currentSuccessTrm, currentSuccessResult));
        return currentSuccess;
    }

    public void SetSuccessEnum(int child, Vector3 successTrm,string ResultStr, SuccessEnum success)
    {
        _selectHitTrm.GetChild(child).gameObject.SetActive(false);
        currentSuccessTrm = successTrm;
        currentSuccessResult = ResultStr;
        StartCoroutine(SuccessStartcatch());
        currentSuccess = success;
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
            _startCatchPanel.IsPointStop = false;
            _startCatchPanel.ResetCatchPanel();

            yield return new WaitForSeconds(1);
            StarCatchBarChange();
        }
    }

    public void ResetSelectBar()
    {
        for (int j = 0; j < _selectHitTrm.childCount; ++j)
        {
            _selectHitTrm.GetChild(j).gameObject.SetActive(true);
        }
        _trueCatchPointCnt = 0;
    }

}


