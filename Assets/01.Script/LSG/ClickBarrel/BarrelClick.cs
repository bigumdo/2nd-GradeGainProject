using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class BarrelClick : MonoBehaviour
{
    [SerializeField] private List<Barrel> barrel;
    private Vector3 spawnPos;

    private void Awake()
    {
        spawnPos = transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Barrel newBarrel = PoolingManager.Instance.Pop(PoolingType.Barrel) as Barrel;
            barrel.Add(newBarrel);
            newBarrel.transform.position = spawnPos;
            spawnPos.y += 3.2f;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BreakBarrel();
        }
    }

    private void BreakBarrel()
    {
        if (barrel.Count > 0)
        {
            
            // 리스트에서 맨 밑에 있는 배럴 가져오기
            Barrel bottomBarrel = barrel[0];
                
            // 맨 밑에 있는 배럴의 Break 메서드 호출
            if (bottomBarrel.Break())
            {
                ClickBarrelGameManager.Instance.Count++;
                // 리스트에서 맨 밑에 있는 배럴 제거
                barrel.RemoveAt(0);

                // 새로운 배럴을 리스트의 맨 위에 추가
                Barrel newTopBarrel = PoolingManager.Instance.Pop(PoolingType.Barrel) as Barrel;
                Vector3 newSpawnPos = transform.position + new Vector3(0, 3.2f * barrel.Count, 0);
                newTopBarrel.transform.position = newSpawnPos;
                barrel.Add(newTopBarrel);
            }
        }
    }
}