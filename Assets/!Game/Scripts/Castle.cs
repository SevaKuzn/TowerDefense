using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Castle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Scrollbar hpBar;
    [SerializeField] public GameObject losePanel;
    public int hpValue = 100;
    public bool isDamage;

    void Start()
    {
        ColorBlock colBlock = hpBar.colors;
        hpBar.interactable = false;
        colBlock.colorMultiplier = 5f;
    }

    void Update()
    {
        if (isDamage)
        {
            hpBar.size = hpValue / 100f;
            hpText.text = hpValue.ToString();
        }

        if (hpValue <= 0)
        {
            losePanel.SetActive(true);
        }
    }
}

