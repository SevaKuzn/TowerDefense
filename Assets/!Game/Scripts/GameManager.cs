using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] Wheat wheat;
    [SerializeField] TextMeshProUGUI wheatText;
    [SerializeField] Civilians civilians;
    private float wheatTime = 0f;


    void Start()
    {
        wheat.wheatCount = 0;
        wheatText.text = $"Пщеница: {wheat.wheatCount}";
    }

    void Update()
    {   
        if (wheatTime >= 3f) {
            wheat.wheatCount += wheat.wheatSalary * civilians.civilianCount;
            wheatText.text = $"Пщеница: {wheat.wheatCount}";
            wheatTime = 0f;
        }
        wheatTime += Time.deltaTime;
    }
}
