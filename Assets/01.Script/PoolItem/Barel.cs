using ObjectPooling;
using UnityEngine;

public class Barel : PoolableMono
{
    [SerializeField] private int hp;
    public bool Break()
    {
        hp -= 1;
        if (hp > 0) return false;
        gameObject.SetActive(false);
        PoolingManager.Instance.Push(this);
        return true;
    }
    
    public override void ResetItem()
    {
        hp = 10;
        transform.position = PoolingManager.Instance.transform.position;
        transform.localRotation = Quaternion.identity;
    }
}
