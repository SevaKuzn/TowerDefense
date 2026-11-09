using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 5f;
    [SerializeField] private int startEnemyCount = 3;
    private int waveCount = 1;

    [SerializeField] private Enemy _enemyGoblin;
    [SerializeField] private Enemy _enemySkeleton;

    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Vector2 enemyArea;

    [SerializeField] private TextMeshProUGUI waveTimerText;

    private float timer;
    private bool isTimerRunning = false;
    private bool isSpawning = false;
    private float walkDuration = 5f;

    void Start()
    {
        timer = spawnTimer;

        
    }

    void Update()
    {
        if (isSpawning || spawnArea.transform.childCount > 0)
        {
            return;
        }
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            timer = spawnTimer;
            StartCoroutine(Timer());
        }
        waveTimerText.text = $"{waveCount} волна через: {timer:F1}";
    }

    void StartWave()
    {
        isSpawning = true;
        SpawnRandomEnemy();
        StartCoroutine(MoveRoutine(enemyArea, walkDuration));
        isSpawning = false;
    }

    void SpawnRandomEnemy()
    {
        for (int i = 0; i < startEnemyCount; i++)
        {
            int randomEnemyNumber = Random.Range(0, 2);

            if (randomEnemyNumber == 0)
            {
                _enemyGoblin.Init(spawnArea);
            }
            else
            {
                _enemySkeleton.Init(spawnArea);
            }
        }
        startEnemyCount++;
    }

    IEnumerator Timer()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        timer = 0;
        isTimerRunning = false;
        waveCount++;
        StartWave();
    }

    private IEnumerator MoveRoutine(Vector2 endPos, float time)
    {
        Vector2 startPos = spawnArea.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / time);
            spawnArea.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        spawnArea.anchoredPosition = endPos;
    }

    private IEnumerator Walk(float walkDuration)
    {
        _enemyGoblin.SetWalk(true);
        _enemySkeleton.SetWalk(true);
        yield return new WaitForSeconds(walkDuration);
        _enemyGoblin.SetWalk(false);
        _enemySkeleton.SetWalk(false);
    }
}