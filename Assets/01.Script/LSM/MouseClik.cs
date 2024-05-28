using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClik : MonoBehaviour
{

    [SerializeField] private ClickSO _normalClickSO;
    [SerializeField] private ClickSO _fevertimeClickSO;

    private float _time;
    private float _clickMoney;
    private float _clickCoolTime;

    private void Start()
    {
        _time = _normalClickSO.clickCoolTime;
        _clickMoney = _normalClickSO.oneClickMoney;
        _clickCoolTime = _normalClickSO.clickCoolTime;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _time <= 0)
        {
            Debug.Log(_clickMoney);
            _time = _clickCoolTime;
        }
        else if (_time >= 0)
        {
            _time -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangeClick(_fevertimeClickSO));
        }
    }

    private IEnumerator ChangeClick(ClickSO clickSO)
    {


        _clickCoolTime = clickSO.clickCoolTime;
        _clickMoney = clickSO.oneClickMoney;
        yield return new WaitForSeconds(clickSO.ChangeTime);
        _clickCoolTime = _normalClickSO.clickCoolTime;
        _clickMoney = _normalClickSO.oneClickMoney;

    }

}
