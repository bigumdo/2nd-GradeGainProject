using System.Collections;
using ObjectPooling;
using UnityEngine;

public class Barrel : PoolableMono
{
    [SerializeField] private int hp;
    [SerializeField] private ParticleSystem breakParticle;
    public bool Break()
    {
        hp -= 1;
        if (hp > 0) return false;
        StartCoroutine(BreakCoroutine());
        return true;
    }
    
    private IEnumerator BreakCoroutine()
    {
        breakParticle.transform.SetParent(null);
        breakParticle.transform.localPosition = Vector3.zero;
        breakParticle.Play();
        yield return null;
        gameObject.SetActive(false);
        PoolingManager.Instance.Push(this);
    }
    
    public override void ResetItem()
    {
        breakParticle.transform.SetParent(transform);
        hp = 10;
        transform.position = PoolingManager.Instance.transform.position;
        transform.localRotation = Quaternion.identity;
    }
}
