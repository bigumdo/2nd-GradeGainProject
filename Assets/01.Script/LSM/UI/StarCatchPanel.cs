using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StarCatchPanel : MonoBehaviour
{
    public Transform Point { get; private set; }
    public Image _successGage;


    public bool IsPointStop { get; set; }
    public Vector2 StartPoint { get; set; }
    public StarCatchBar StartCatchBar { get; set; }

    [SerializeField] private float _pointSpeed;
    [SerializeField] private int _hammerHitCnt;
    [SerializeField] private TextMeshProUGUI _hannerCountText;
    [SerializeField] private TextMeshProUGUI _hannerResultText;
    [SerializeField] private TextMeshProUGUI _WeaponLevelText;
    
    private StarCatchBar _starCatchBar;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection = 1;
    //private bool 

    private void Awake()
    {
        IsPointStop = false;
        StartCatchBar = transform.Find("StartCatchUI/StarCatchBar").GetComponent<StarCatchBar>();
        Point = transform.Find("Point");
        outlineTrm = transform.Find("StartCatchUI/StarCatchBar").GetComponent<RectTransform>();
        _starCatchBar = GetComponentInChildren<StarCatchBar>();
        _barSize = outlineTrm.rect.width;
        _successGage.fillAmount = 0;
        StartPoint = new Vector2(-_barSize * 0.4f, Point.localPosition.y);
    }


    private void Start()
    {
        _hannerCountText.text = _hammerHitCnt.ToString();
    }

    public void ResetCatchPanel()
    {
        Point.localPosition = StartPoint;
        _pointDirection = 1;
    }

    public void ResetPoint()
    {
        UIManager.Instance.SelectWeaponTimer();
        Point.localPosition = StartPoint;
        _pointDirection = 1;
        _successGage.fillAmount = 0;
    }

    public void ProductionSet(WeaponSO weaponSO)
    {
        _pointSpeed = weaponSO.starCatchSpeed;
        _hammerHitCnt = weaponSO.hammerHitCnt;
        _pointDirection = 1;
        _successGage.fillAmount = 0;
        _hannerCountText.text = _hammerHitCnt.ToString();
        for (int i =0;i< StartCatchBar._hitTrm.Length;++i)
        {
            StarCatchPoint []point;
            point = StartCatchBar._hitTrm[i].GetComponentsInChildren<StarCatchPoint>();
            for(int j =0;j<point.Length;++j)
            {
                point[j].starCatchSize = weaponSO.starCatchSize;
                point[j].SetSize();
            }
            
            //설명 택스와 아이콘도 바꿔야 한다.
        }
        StartCatchBar.StarCatchBarChange();
        ResetCatchPanel();
    }



    private void Update()
    {
        if(_hammerHitCnt != 0)
        {
            if (Mathf.Abs(Point.transform.localPosition.x) > _barSize * 0.5f)
            {
                float overRange = Mathf.Abs(Point.transform.localPosition.x) - _barSize * 0.5f;
                _pointDirection *= -1;
                Point.transform.position += Vector3.right * overRange * _pointDirection;
                _hammerHitCnt = Mathf.Clamp(_hammerHitCnt -= 1, 0, 100);
                _hannerCountText.text = _hammerHitCnt.ToString();
                _pointSpeed += Random.Range(Random.Range(-0.2f, -0.1f), Random.Range(0.1f, 0.2f));
                float sppedMathLimit = GameManager.Instance.nowWeapon.starCatchSpeed;
                _pointSpeed = Mathf.Clamp(_pointSpeed, sppedMathLimit *0.5f, sppedMathLimit * 1.5f);
            } // 방향 전환 및 강화 횟수 감소
            if(IsPointStop && GameManager.Instance.isSelectWeapon)
            {
                Point.transform.position += Vector3.right * _pointDirection
                * _pointSpeed * Time.deltaTime;
            }// 포인터 이동
            if (Input.GetKeyDown(KeyCode.Space) &&  IsPointStop)
            {
                _hammerHitCnt = Mathf.Clamp(_hammerHitCnt -= 1, 0, 100);
                _hannerCountText.text = _hammerHitCnt.ToString();
                SuccessEnum hitEnum = _starCatchBar.Hitpoint(Point);
                GameManager.Instance.currentSuccessEnum = hitEnum;
                switch (hitEnum)
                {
                    case SuccessEnum.GreatSuccess:
                        _successGage.fillAmount += 0.1f;
                        break;
                    case SuccessEnum.NormalSuccess:
                        _successGage.fillAmount += 0.05f;
                        break;
                    case SuccessEnum.Fail:
                        _successGage.fillAmount -= 0.1f;
                        break;
                }
                if (_successGage.fillAmount >= 1)
                {
                    GameManager.Instance.isSelectWeapon = false;
                    UIManager.Instance.produceResetPanel.SetActive(true);
                    Inventory.Instance.AddItem();
                    ResetCatchPanel();
                }

            }//망치 강화
        }
        else if(!UIManager.Instance.produceResetPanel.activeSelf) // 획수가 다 달면 UI  켜주기
        {
            UIManager.Instance.produceResetPanel.SetActive(true);
            GameManager.Instance.isSelectWeapon = false;
        }
    }

    public IEnumerator ResultText(Vector3 pointTrm, string resultText)
    {
        _hannerResultText.transform.position = pointTrm;
        _hannerResultText.enabled = true;
        _hannerResultText.text = resultText;
        yield return new WaitForSeconds(1f);
        _hannerResultText.enabled = false;
    }
}
