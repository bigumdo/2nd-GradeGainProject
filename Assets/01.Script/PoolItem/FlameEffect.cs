using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class FlameEffect : PoolableMono
{
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        PoolingManager.Instance.Push(this);
    }

    public override void ResetItem()
    {
        
    }
}
