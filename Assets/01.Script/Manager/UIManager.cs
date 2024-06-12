using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public StarCatchPanel startCatchPanel;
    public CanvasGroup startCatchCanvasGroup;

    [SerializeField] private TextMeshProUGUI resourceText;

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

    
    public void Debuging()
    {
        Debug.Log(1);
    }

}
