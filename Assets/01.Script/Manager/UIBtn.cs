using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public enum btnEnum
{
    Close,
    Start,
    MiniGame,
    Explanation,
    ExplanationClose
}

public class UIBtn : BaseBtn
{
    public btnEnum currentbtn;
    public CanvasGroup _currentCanvas;
    public GameObject explanationPanel;
    private Button _closeBtn;
    public Button _explanationPanelBtn;

    protected override void PointClick()
    {
        base.PointClick();
        switch (currentbtn)
        {
            case btnEnum.Close:
                _closeBtn.onClick.AddListener(Close);
                UIManager.Instance.IsStartCatch = false;
                break;
            case btnEnum.Start:
                SceneManager.LoadScene(1);
                break;
            case btnEnum.MiniGame:
                SceneManager.LoadScene(2);
                break;
            case btnEnum.Explanation:
                explanationPanel.SetActive(true);
                break;
            case btnEnum.ExplanationClose:
                _explanationPanelBtn.onClick.AddListener(explantionClose);
                break;
        };
               
    }

    protected override void PointEnter()
    {
        base.PointEnter();
    }

    protected override void PointExit()
    {
        base.PointExit();
    }

    public override void Awake()
    {
        base.Awake();
        //_currentCanvas = transform.root.GetComponent<CanvasGroup>();
        _closeBtn = GetComponent<Button>();
        
    }

    private void explantionClose()
    {
        explanationPanel.SetActive(false);
    }

    private void Close()
    {
        _currentCanvas.alpha = 0;
    }

    private void Open()
    {
        _currentCanvas.alpha = 1;

    }

}
