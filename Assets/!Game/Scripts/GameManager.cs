using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] Wheat wheat;
    [SerializeField] Civilians civilians;
    private float wheatTime = 0f;


    void Start()
    {
        wheat.wheatCount = 0;
        wheat.wheatText.text = $"Пщеница: {wheat.wheatCount}";
    }

    void Update()
    {   
        if (wheatTime >= 3f) {
            wheat.wheatCount += wheat.wheatSalary * civilians.civilianCount;
            wheat.wheatText.text = $"Пщеница: {wheat.wheatCount}";
            wheatTime = 0f;
        }
        wheatTime += Time.deltaTime;
    }
}
