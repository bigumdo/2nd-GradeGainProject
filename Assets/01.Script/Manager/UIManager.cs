using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public StarCatchPanel startCatchPanel;
    public CanvasGroup startCatchCanvasGroup;
    public GameObject _produceResetPanel;

    public int timerCnt;

    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private TextMeshProUGUI timemerText;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button ProduceCloseBtn;



    private void Awake()
    {
        startCatchCanvasGroup.alpha = 0;
        _produceResetPanel.SetActive(false);
    }

    private void Update()
    {
        resourceText.text = Inventory.Instance.gold.ToString();
    }

    private void MenuOpen()
    {
        menuPanel.gameObject.SetActive(true);

    }

    public void SelectWeaponTimer()
    {
        UIManager.Instance.startCatchPanel._isPointStop = false;
        Sequence seq = DOTween.Sequence();
        int count= timerCnt;
        timemerText.text = count.ToString();
        for (int i = timerCnt; i>0;--i)
        {

            seq.Append(DOTween.To(() => timemerText.fontSize, x => timemerText.fontSize = x, 600, 0.5f))
                .Append(DOTween.To(() => timemerText.fontSize, x => timemerText.fontSize = x, 0, 0.5f))
                .AppendCallback(()=>count--)
                .AppendCallback(()=> timemerText.text = count.ToString())
                .AppendCallback(()=>
                {
                    if(count == 0)
                    {
                        GameManager.Instance.isSelectWeapon = true;
                        UIManager.Instance.startCatchPanel._isPointStop = true;

                    }
                });
        }   
    }
}
