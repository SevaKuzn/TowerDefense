using UnityEngine;

public class AddUnitBtn : MonoBehaviour
{
    int maxUnitCount = 14;
    int unitCount = 1;
    int price;
    public Transform place;
    public void GetPrice(int unitPrice)
    {
        price = unitPrice;
    }
    public void GetPosition(GameObject spawnPlace)
    {
        place = spawnPlace.transform;
    }
    public void AddUnit(GameObject prefab)
    {
        if (unitCount != maxUnitCount || unitCount! > maxUnitCount)
        {
            GameObject newUnit = Instantiate(prefab, place);
            unitCount++;
        }
    }
}
