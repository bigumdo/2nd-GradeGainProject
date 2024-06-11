using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum btnEnum
{
    Close,
    Open,
    Menu
}

public class UIBtn : MonoBehaviour
{
    public btnEnum currentbtn;
    public CanvasGroup _currentCanvas;
    private Button _closeBtn;

    private void Awake()
    {
        //_currentCanvas = transform.root.GetComponent<CanvasGroup>();
        _closeBtn = GetComponent<Button>();
        switch (currentbtn)
        {
            case btnEnum.Close:
                _closeBtn.onClick.AddListener(Close);

                break;
            case btnEnum.Open:
                break;
            case btnEnum.Menu:
                break;
        };
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
