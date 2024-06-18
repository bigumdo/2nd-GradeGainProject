using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StarCatchPanel : MonoBehaviour
{
    public Transform Point { get; private set; }
    public Image _successGage;


    [HideInInspector]public bool _isPointStop;
    [HideInInspector] public Vector2 _startPoint;

    [SerializeField] private float _pointSpeed;
    [SerializeField] private int _hammerHitCnt;
    [SerializeField] private TextMeshProUGUI _hannerCountText;
    [SerializeField] private TextMeshProUGUI _hannerResultText;
    [SerializeField] private TextMeshProUGUI _WeaponLevelText;
    [SerializeField] private StarCatchBar _startCatchBar;
    
    private StarCatchBar _starCatchBar;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection = 1;

    private void Awake()
    {
        Point = transform.Find("Point");
        outlineTrm = transform.Find("StartCatchUI/StarCatchBar").GetComponent<RectTransform>();
        _starCatchBar = GetComponentInChildren<StarCatchBar>();
        _barSize = outlineTrm.rect.width;
        _successGage.fillAmount = 0;
        _startPoint = new Vector2(-_barSize * 0.4f, Point.localPosition.y);

        GameManager.Instance.ResetProductEvent += ResetCatchPanel;

    }

    private void OnDestroy()
    {
        GameManager.Instance.ResetProductEvent -= ResetCatchPanel;
    }

    private void Start()
    {
        _hannerCountText.text = _hammerHitCnt.ToString();
    }

    public void ResetCatchPanel()
    {
        Point.localPosition = _startPoint;
        _pointDirection = 1;
        _successGage.fillAmount = 0;
    }

    public void ProductionSet(WeaponSO weaponSO)
    {
        _pointSpeed = weaponSO.starCatchSpeed;
        _hammerHitCnt = weaponSO.hammerHitCnt;
        _pointDirection = 1;
        _hannerCountText.text = _hammerHitCnt.ToString();
        for (int i =0;i< _startCatchBar._hitTrm.Length;++i)
        {
            StarCatchPoint []point;
            point = _startCatchBar._hitTrm[i].GetComponentsInChildren<StarCatchPoint>();
            for(int j =0;j<point.Length;++j)
            {
                point[j].starCatchSize = weaponSO.starCatchSize;
                point[j].SetSize();
            }
            
            //설명 택스와 아이콘도 바꿔야 한다.
        }
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
            }
            if(!_isPointStop && GameManager.Instance.isSelectWeapon)
            {
                Point.transform.position += Vector3.right * _pointDirection
                * _pointSpeed * Time.deltaTime;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && _hammerHitCnt != 0)
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
                    UIManager.Instance._produceResetPanel.SetActive(true);
                    ResetCatchPanel();
                }

            }
        }
        else if(!UIManager.Instance._produceResetPanel.activeSelf)
        {
            UIManager.Instance._produceResetPanel.SetActive(true);
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
