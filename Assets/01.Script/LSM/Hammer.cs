using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuccessEnum
{
    GreatSuccess,
    NormalSuccess,
    Fail
}

public class Hammer : MonoBehaviour
{

    [SerializeField] private HammerSO _normalClickSO;
    [SerializeField] private HammerSO _fevertimeClickSO;

    public Player _player;


    private void Start()
    {
        _player.audioSource.clip = SoundManager.Instance.getAudio["HammerClik"];
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && GameManager.Instance.isSelectWeapon)
        {
            StartCoroutine(ChangeClick(_fevertimeClickSO));
        }
    }

    public void HammerStarCatch()
    {
        _player.animator.SetTrigger("Reset");
        _player.animator.SetTrigger("Click");
    }

    private IEnumerator ChangeClick(HammerSO clickSO)
    {

        _player.animator.speed = _fevertimeClickSO.animaSpeed;
        //_clickCoolTime = clickSO.clickCoolTime;
        //_clickMoney = clickSO.oneClickMoney;
        yield return new WaitForSeconds(clickSO.changeTime);
        _player.animator.speed = _normalClickSO.animaSpeed;
        //_clickCoolTime = _normalClickSO.clickCoolTime;
        //_clickMoney = _normalClickSO.oneClickMoney;

    }

}
