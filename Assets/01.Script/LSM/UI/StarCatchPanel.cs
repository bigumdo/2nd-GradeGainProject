using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StarCatchPanel : MonoBehaviour
{
    public Transform Point { get; private set; }
    public Image _successGage;

    [SerializeField] private float _pointSpeed;
    [SerializeField] private int _hammerHitcnt;
    [SerializeField] private TextMeshProUGUI _hannerCountText;
    [SerializeField] private TextMeshProUGUI _hannerResultText;
    [SerializeField] private TextMeshProUGUI _WeaponLevelText;
    
    private StarCatchBar _starCatchBar;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection = 1;
    private int _successCnt;
    
    private void Awake()
    {
        Point = transform.Find("StarCatchBar/HitPointOutline/Point");
        outlineTrm = transform.Find("StarCatchBar/HitPointOutline").GetComponent<RectTransform>();
        _starCatchBar = GetComponentInChildren<StarCatchBar>();
        _barSize = outlineTrm.rect.width;
        _successGage.fillAmount = 0;
    }

    private void Start()
    {
        _hannerCountText.text = _hammerHitcnt.ToString();
    }

    private void Update()
    {
        if(_hammerHitcnt != 0)
        {
            if (Mathf.Abs(Point.transform.localPosition.x) > _barSize * 0.5f)
            //|| Point.transform.localPosition.x >= 0)
            {
                _pointDirection *= -1;
                _hammerHitcnt = Mathf.Clamp(_hammerHitcnt -= 1, 0, 100);
                _hannerCountText.text = _hammerHitcnt.ToString();
                _pointSpeed += Random.Range(Random.Range(-0.2f, -0.1f), Random.Range(0.1f, 0.2f));
                _pointSpeed = Mathf.Clamp(_pointSpeed, 5, 15);
            }
            Point.transform.position += Vector3.right * _pointDirection
                * _pointSpeed;
            if (Input.GetKeyDown(KeyCode.Space) && _hammerHitcnt != 0)
            {
                _hammerHitcnt = Mathf.Clamp(_hammerHitcnt -= 1, 0, 100);
                _hannerCountText.text = _hammerHitcnt.ToString();
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

            }
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
