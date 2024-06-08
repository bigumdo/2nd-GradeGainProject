using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class BarelClick : MonoBehaviour
{
    [SerializeField] private List<Barel> barel;
    private Vector3 spawnPos;

    private void Awake()
    {
        spawnPos = transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Barel newBarel = PoolingManager.Instance.Pop(PoolingType.Barel) as Barel;
            barel.Add(newBarel);
            newBarel.transform.position = spawnPos;
            spawnPos.y += 3.2f;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (barel.Count > 0)
            {
                // 리스트에서 맨 밑에 있는 배럴 가져오기
                Barel bottomBarel = barel[0];
                
                // 맨 밑에 있는 배럴의 Break 메서드 호출
                if (bottomBarel.Break())
                {
                    // 리스트에서 맨 밑에 있는 배럴 제거
                    barel.RemoveAt(0);

                    // 새로운 배럴을 리스트의 맨 위에 추가
                    Barel newTopBarel = PoolingManager.Instance.Pop(PoolingType.Barel) as Barel;
                    Vector3 newSpawnPos = transform.position + new Vector3(0, 3.2f * barel.Count, 0);
                    newTopBarel.transform.position = newSpawnPos;
                    barel.Add(newTopBarel);
                }
            }
        }
    }
}