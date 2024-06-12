using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public Camera mainCam;
    public Player player;
    public WeaponSO nowWeapon;

    [HideInInspector] public SuccessEnum currentSuccessEnum;
    [HideInInspector] public bool isSelectWeapon;

    public IEnumerator SelectWeapon()
    {
        yield return new WaitForSeconds(3f);
        isSelectWeapon = true;
    }

}
