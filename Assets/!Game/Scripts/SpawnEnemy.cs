using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public float spawnTimer;
    [SerializeField] private int enemyCount;


    [SerializeField] private Enemy _enemyGoblin = new Enemy();
    [SerializeField] private Enemy _enemySkeleton = new Enemy();

    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Transform EnemyArea;

    private bool isTimer = true;

    void Update()
    {
        if (isTimer)
        {
            if (spawnArea.transform.childCount >= enemyCount)
            {
                isTimer = false;
            }
            else
            {
                SpawnRandomEnemy();
            }
        }
    }

    void SpawnRandomEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            int randomEnemyNumber = Random.Range(0, 2);
            //Debug.Log(randomEnemyNumber);
            if (randomEnemyNumber == 0)
            {
                _enemyGoblin.Init(spawnArea);
            }
            if (randomEnemyNumber == 1)
            {
                _enemySkeleton.Init(spawnArea);
            }
        }
    }
}
