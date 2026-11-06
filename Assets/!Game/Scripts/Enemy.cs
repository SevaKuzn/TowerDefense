using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float hp;
    [SerializeField] public float damage;
    [SerializeField] public GameObject enemyPrefab;


    public void Init(RectTransform spawnArea)
    {
        Instantiate(enemyPrefab, spawnArea);
    }
}
