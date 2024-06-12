using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickBarrelGameManager : MonoSingleton<ClickBarrelGameManager>
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private List<int> _barrelCountList = new List<int>();
    
    private float _min;
    private float _sec;
    private float _msec;
    private bool _isEndHit;

    public int Count { get; set;}
    private void Update()
    {
        TimeRender();
        CountRender();
    }

    private void TimeRender()
    {
        if (_isEndHit) return;
        _min = Mathf.FloorToInt(Time.time / 60);
        _sec = Mathf.FloorToInt(Time.time % 60);
        _msec = Mathf.FloorToInt((Time.time * 100) % 100);
        _timeText.text = $"{_min:00}:{_sec:00}:{_msec:00}";
    }
    
    private void CountRender()
    {
        _countText.text = $"{Count}";
    }
}