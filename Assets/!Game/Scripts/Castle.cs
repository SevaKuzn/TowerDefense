using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Castle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Scrollbar hpBar;
    private int hpValue = 100;


    void Start()
    {
        ColorBlock colBlock = hpBar.colors;
        hpBar.interactable = false;
        colBlock.colorMultiplier = 5f;
    }

 
    // Update is called once per frame
    void Update()
    {
        if ((hpValue != 0 || hpValue !< 0) && (hpBar.size != 0 || hpBar.size !< 0))
        {
            hpValue -= 1;
            hpBar.size = hpValue / 100f;
            hpText.text = hpValue.ToString();
        }
        hpBar.value = 0;
    }
}

