using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickBarrelGameManager : MonoSingleton<ClickBarrelGameManager>
{
    [SerializeField] private Image _endGamePanel;
    [SerializeField] private TextMeshProUGUI _endGameTimeText;
    [SerializeField] private Button _endGameButton;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private List<int> _barrelCountList = new List<int>();
    
    private float _min;
    private float _sec;
    private float _msec;
    private bool _isEndHit;
    private bool _isEndGame;
    public int Count { get; set;}
    
    private void Update()
    {
        if (_isEndGame) return;
        if (Count >= 100)
        {
            _endGamePanel.gameObject.SetActive(true);
            EndGame();
            _isEndGame = true;
        }
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
    
    public void EndGame()
    {
        _endGameTimeText.text = $"Time: {_min:00}:{_sec:00}:{_msec:00}";
        _timeText.text = "00:00:00";
        _countText.text = "0";
        _endGameButton.onClick.AddListener(() =>
        {
            _endGamePanel.gameObject.SetActive(false);
            _isEndGame = false;
            _isEndHit = false;
            _min = 0;
            _sec = 0;
            _msec = 0;
            Count = 0;
            SceneManager.LoadScene("LSM");
        });
    }
}