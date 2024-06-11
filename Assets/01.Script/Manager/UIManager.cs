using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI resourceText;

    [SerializeField] private GameObject menuPanel;

    [SerializeField] private Button MenuBtn;
    [SerializeField] private Button ProduceCloseBtn;



    private void Awake()
    {
        MenuBtn.onClick.AddListener(Debuging);
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
