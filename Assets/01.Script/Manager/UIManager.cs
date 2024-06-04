using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI resourceText;

    private void Update()
    {
        resourceText.text = Inventory.Instance.gold.ToString();
    }

}
