using ObjectPooling;
using UnityEngine;

public class Barel : PoolableMono
{
    private int hp;
    public void Break()
    {
        gameObject.SetActive(false);
        PoolingManager.Instance.Push(this);
    }
    
    public override void ResetItem()
    {
        transform.position = PoolingManager.Instance.transform.position;
        transform.localRotation = Quaternion.identity;
    }
}
