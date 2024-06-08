using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class FallEffect : PoolableMono
{
    public void Disable()
    {
        StartCoroutine(DisableCoroutine());
    }

    private IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        PoolingManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        
    }
}
