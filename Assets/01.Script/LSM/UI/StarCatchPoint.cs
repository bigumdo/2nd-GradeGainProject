using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCatchPoint : MonoBehaviour
{

    private RectTransform [] _points;
    public Vector2 starCatchSize;

    public void SetSize()
    {
        if(_points == null)
            _points = transform.GetComponentsInChildren<RectTransform>();

        for (int i = 1; i < _points.Length; ++i)
        {
            _points[i].sizeDelta = starCatchSize;
        }
    }

}
