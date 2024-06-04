using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCatchPoint : MonoBehaviour
{

    private RectTransform [] _points;
    public Vector2 starCatchSize;

    private void Awake()
    {

        _points = transform.GetComponentsInChildren<RectTransform>();

        for (int i = 1; i <= transform.childCount; ++i)
        {
            _points[i].sizeDelta = starCatchSize;
        }
    }

}
