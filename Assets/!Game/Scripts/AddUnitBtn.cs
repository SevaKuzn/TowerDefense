using UnityEngine;
using UnityEngine.UI;


public class AddUnitBtn : MonoBehaviour
{
    private int maxUnitCount = 14;
    private int unitCount = 1;
    private int price;
    private int wCount;
    private float timer;
    private float unitCooldown;
    private Image icon;
    private Wheat wh;
    private Transform place;

    private bool isCooldown = false;
    private bool isTimer = false;

    #region "Get"
    public void GetPrice(Civilians unit)
    {
        price = unit.price;
    }
    public void GetWheatCount(Wheat wheat)
    {
        wCount = wheat.wheatCount;
        wh = wheat;
    }
    public void GetPosition(GameObject spawnPlace)
    {
        place = spawnPlace.transform;
    }

    public void GetIcon(Image unitIcon)
    {
        icon = unitIcon;
    }

    public void GetUnitCooldown(int cooldown)
    {
        unitCooldown = cooldown;
    }
    #endregion

    void Update()
    {
        if (isTimer)
        {
            if ((timer != unitCooldown) && (timer !> unitCooldown))
            {
                timer += Time.deltaTime;
                icon.fillAmount += 1f/unitCooldown * Time.deltaTime;
                isCooldown = true;
            }
        }
    }

    public void AddUnit(GameObject prefab)
    {
        Debug.Log($"price:{price}, wCount:{wCount}");
        if ((unitCount != maxUnitCount || unitCount! > maxUnitCount) && (price <= wCount))
        {
            isTimer = true;
            if (isCooldown)
            {
                wCount -= price;
                wh.wheatCount -= price;
                wh.wheatText.text = $"Пщеница: {wCount}";
                Debug.Log("sadsad");
                GameObject newUnit = Instantiate(prefab, place);
                unitCount++;
                isCooldown = false;
                isTimer = false;
            }
        }
    }
}
