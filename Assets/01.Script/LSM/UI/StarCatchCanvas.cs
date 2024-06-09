using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StarCatchCanvas : MonoBehaviour
{
    public Image _successGage;

    [SerializeField] private int _pointSpeed;
    [SerializeField] private int _hammerHitcnt;
    [SerializeField] TextMeshProUGUI _hannerCountText;
    [SerializeField] TextMeshProUGUI _hannerResultText;
    
    public Transform Point { get; private set; }
    private StarCatchBar _starCatchBar;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection=1;


    private void Awake()
    {
        Point = transform.Find("SwordUpgrades/HitPointOutline/Point");
        outlineTrm = transform.Find("SwordUpgrades/HitPointOutline").GetComponent<RectTransform>();
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
        if(Point.transform.localPosition.x < -_barSize ||
            Point.transform.localPosition.x >= 0)
        {
            _pointDirection *= -1;
        }
        Point.transform.position += Vector3.right * _pointDirection
            * _pointSpeed;
        if(Input.GetKeyDown(KeyCode.Space) && _hammerHitcnt != 0)
        {
            _hammerHitcnt = Mathf.Clamp(_hammerHitcnt -= 1,0,100);
            _hannerCountText.text = _hammerHitcnt.ToString();
            switch (_starCatchBar.Hitpoint(Point))
            {
                case SuccessEnum.GreatSuccess:
                    _successGage.fillAmount += 0.2f;
                    break;
                case SuccessEnum.NormalSuccess:
                    _successGage.fillAmount += 0.1f;
                    break;
                case SuccessEnum.Fail:
                    _successGage.fillAmount -= 0.1f;
                    break;
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
