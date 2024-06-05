using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    [SerializeField] private HammerSO _normalClickSO;
    [SerializeField] private HammerSO _fevertimeClickSO;

    public Player _player;


    private float _time;
    private int _clickMoney;
    private float _clickCoolTime;


    private void Start()
    {
        _player.audioSource.clip = SoundManager.Instance.getAudio["HammerClik"];
        _time = _normalClickSO.clickCoolTime;
        _clickMoney = _normalClickSO.oneClickMoney;
        _clickCoolTime = _normalClickSO.clickCoolTime;
        
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && _time <= 0)
        //{
            
        //    //_normalClickSO.cout();
        //}
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangeClick(_fevertimeClickSO));
        }
    }

    public void HammerStarCatch()
    {
        Inventory.Instance.Hammering(_clickMoney);

        _player.animator.SetTrigger("Click");
        _time = _clickCoolTime;
    }

    private IEnumerator ChangeClick(HammerSO clickSO)
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
