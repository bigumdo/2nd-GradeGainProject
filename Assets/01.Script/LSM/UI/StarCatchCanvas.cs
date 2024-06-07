using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCatchCanvas : MonoBehaviour
{
    
    private Transform _point;
    private StarCatchBar _starCatchBar;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection=1;
    [SerializeField]private int _pointSpeed;
    public Image _successGage;

    private void Awake()
    {
        _point = transform.Find("SwordUpgrades/HitPointOutline/Point");
        outlineTrm = transform.Find("SwordUpgrades/HitPointOutline").GetComponent<RectTransform>();
        _starCatchBar = GetComponentInChildren<StarCatchBar>();
        _barSize = outlineTrm.rect.width;
        _successGage.fillAmount = 0;

    }

    private void Update()
    {
        if(_point.transform.localPosition.x < -_barSize ||
            _point.transform.localPosition.x >= 0)
        {
            _pointDirection *= -1;
        }
        _point.transform.position += Vector3.right * _pointDirection
            * _pointSpeed;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (_starCatchBar.Hitpoint(_point))
            {
                case SuccessEnum.GreatSuccess:
                    _successGage.fillAmount += 0.3f;
                    break;
                case SuccessEnum.NormalSuccess:
                    _successGage.fillAmount += 0.1f;
                    break;
                case SuccessEnum.Fail:
                    _successGage.fillAmount -= 0.1f;
                    Debug.Log(10);
                    break;
            }

        }
    }

}
