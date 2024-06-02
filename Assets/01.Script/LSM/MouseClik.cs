using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClik : MonoBehaviour
{

    [SerializeField] private ClickSO _normalClickSO;
    [SerializeField] private ClickSO _fevertimeClickSO;

    public Player _player;

    private float _time;
    private float _clickMoney;
    private float _clickCoolTime;

    private AnimationClip _animaClip;

    private void Awake()
    {
    }
    private void Start()
    {
        _time = _normalClickSO.clickCoolTime;
        _clickMoney = _normalClickSO.oneClickMoney;
        _clickCoolTime = _normalClickSO.clickCoolTime;
        _animaClip = _normalClickSO.clip;
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _time <= 0)
        {
            Debug.Log(_clickMoney);
            _player.audioSource.Play();
            _player.animator.SetTrigger("Click");
            _time = _clickCoolTime;
            //_normalClickSO.cout();
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

        _player.animator.speed = _fevertimeClickSO.animaSpeed;
        _clickCoolTime = clickSO.clickCoolTime;
        _clickMoney = clickSO.oneClickMoney;
        yield return new WaitForSeconds(clickSO.changeTime);
        _player.animator.speed = _normalClickSO.animaSpeed;
        _clickCoolTime = _normalClickSO.clickCoolTime;
        _clickMoney = _normalClickSO.oneClickMoney;

    }

}
