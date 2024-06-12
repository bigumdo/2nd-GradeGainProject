using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public StarCatchPanel startCatchPanel;
    public CanvasGroup startCatchCanvasGroup;

    [SerializeField] private TextMeshProUGUI resourceText;
    [SerializeField] private TextMeshProUGUI timemerText;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button ProduceCloseBtn;



    private void Awake()
    {
        MenuBtn.onClick.AddListener(Debuging);
        startCatchCanvasGroup.alpha = 0;
    }

    private void Update()
    {
        resourceText.text = Inventory.Instance.gold.ToString();
    }

    private void MenuOpen()
    {
        menuPanel.gameObject.SetActive(true);

    }

    public void SelectWeapon(int time)
    {
        Sequence seq = DOTween.Sequence();
        int count= time;
        timemerText.text = count.ToString();
        for (int i = time;i>0;--i)
        {

            seq.Append(DOTween.To(() => timemerText.fontSize, x => timemerText.fontSize = x, 600, 0.5f))
                .Append(DOTween.To(() => timemerText.fontSize, x => timemerText.fontSize = x, 0, 0.5f))
                .AppendCallback(()=>count--)
                .AppendCallback(()=> timemerText.text = count.ToString())
                .AppendCallback(()=>
                {
                    if(count == 0)
                        GameManager.Instance.isSelectWeapon = true;
                });

        }
        
    }



    public void Debuging()
    {
        Debug.Log(1);
    }

}
