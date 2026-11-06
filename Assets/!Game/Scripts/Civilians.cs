using UnityEngine;
using UnityEngine.UI;

public class Civilians : MonoBehaviour
{
    [SerializeField] int civilianSalary = 20;
    [SerializeField] public int price = 100;

    public int civilianCount;
    void Start()
    {
        
    }

    void Update()
    {
        civilianCount = transform.childCount;
    }
}
