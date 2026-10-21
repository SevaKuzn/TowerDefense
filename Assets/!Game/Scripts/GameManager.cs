using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] Wheat wheat;
    [SerializeField] TextMeshProUGUI wheatText;
    private float wheatTime = 0f;


    void Start()
    {
        wheatText.text = $"Пщеница: {wheat.wheatCount}";
    }

    // Update is called once per frame
    void Update()
    {
        if (wheatTime >= 3f) {
            wheat.wheatCount += wheat.wheatSalary;
            Debug.Log(wheat.wheatCount);
            wheatText.text = $"Пщеница: {wheat.wheatCount}";
            wheatTime = 0f;
        }
        wheatTime += Time.deltaTime;
    }
}
