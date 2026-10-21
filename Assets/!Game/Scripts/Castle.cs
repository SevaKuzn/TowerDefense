using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Castle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Scrollbar hpBar;
    private int hpValue = 100;


    void Start()
    {
        hpBar.interactable = false;
        
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

