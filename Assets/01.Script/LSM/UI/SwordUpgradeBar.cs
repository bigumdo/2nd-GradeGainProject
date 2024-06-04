using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordUpgradeBar : MonoBehaviour
{
    
    private Transform _point;
    private StarCatchBar _weaponProduction;
    private RectTransform outlineTrm;
    private float _barSize;
    private int _pointDirection=1;

    private void Awake()
    {
        _point = transform.Find("HitPointOutline/Point");
        outlineTrm = transform.Find("HitPointOutline").GetComponent<RectTransform>();
        _weaponProduction = GetComponentInChildren<StarCatchBar>();
        _barSize = outlineTrm.rect.width;


    }

    private void Update()
    {
        if(_point.transform.localPosition.x < -_barSize ||
            _point.transform.localPosition.x >= 0)
        {
            _pointDirection *= -1;
        }
        _point.transform.position += Vector3.right * _pointDirection;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _weaponProduction.Hitpoint(_point);
        }
    }

}
