using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Wheat wheat;
    [SerializeField] private Civilians civiliansWork;
    [SerializeField] private Castle castle;

    [SerializeField] private RectTransform enemySpawnArea;
    [SerializeField] private RectTransform civilianSpawnArea;
    [SerializeField] private RectTransform warriorSpawnArea;
    [SerializeField] private RectTransform defArea;

    [SerializeField] private int warriorRequest = 1;
    [SerializeField] private int civilianRequest = 3;
    [SerializeField] private float battleInterval = 1f;

    
    [SerializeField] private float wheatInterval = 3f;
    [SerializeField] private float waveDelay = 3f;

    public Unit[] civilians;
    public Unit[] warriors;
    public Enemy[] enemies;

    private float wheatTime = 0f;
    private float battleTimer = 0f;
    private bool isEnemyWave = false;
    private bool waveTimerRunning = false;
    private bool unitsReturned = false;
    private bool battleStarted = false;

    void Start()
    {
        wheat.wheatCount = 1000;
        wheat.wheatText.text = $"Пщеница: {wheat.wheatCount}";
        StartWaveTimer();
    }

    void Update()
    {
        UpdateWheat();
        CheckEnemyWave();
        UpdateBattle();
    }

    private void UpdateWheat()
    {
        wheatTime += Time.deltaTime;
        if (wheatTime >= wheatInterval)
        {
            wheat.wheatCount += wheat.wheatSalary * civiliansWork.civilianCount;
            wheat.wheatText.text = $"Пщеница: {wheat.wheatCount}";
            wheatTime = 0f;
        }
    }

    private void CheckEnemyWave()
    {
        if (!isEnemyWave)
        {
            return;
        }

        CollectUnits();

        if (enemies.Length == 0)
        {
            EndWave();
            return;
        }

        if (!unitsReturned && enemySpawnArea.transform.childCount > 0)
        {
            ReturnUnitsToBases();
            unitsReturned = true;
            battleStarted = false;
        }

        if (!battleStarted)
        {
            StartBattle();
            battleStarted = true;
        }
    }

    private void UpdateBattle()
    {
        if (!battleStarted)
        { 
            return; 
        }

        battleTimer += Time.deltaTime;
        if (battleTimer < battleInterval)
        {
            return;
        }

        battleTimer = 0f;
        CollectUnits();

        if (enemies.Length == 0)
        {
            EndWave();
            return;
        }

        bool hasWarriors = warriors.Length >= warriorRequest;
        bool hasCivilians = civilians.Length >= civilianRequest;

        if (hasWarriors || hasCivilians)
        {
            if (hasWarriors)
            {
                BattleWarriorsVsEnemies();
            }
                
            else if (hasCivilians)
            {
                BattleCiviliansVsEnemies();
            }
        }
        else
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.GiveDamage();
                    GetCastleDamage(enemy.damage);
                } 
            }
            EndWave();
        }
    }

    private void BattleWarriorsVsEnemies()
    {
        int fightsCount = Mathf.Min(warriors.Length, enemies.Length);

        for (int i = 0; i < fightsCount; i++)
        {
            if (warriors[i] != null && enemies[i] != null)
            {
                enemies[i].GetDamage(enemies[i].hp);
                warriors[i].GetDamage(warriors[i].hp);
            }
        }
    }

    private void BattleCiviliansVsEnemies()
    {
        int enemiesToKill = civilians.Length / civilianRequest;
        int fightsCount = Mathf.Min(enemiesToKill, enemies.Length);

        for (int i = 0; i < fightsCount; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetDamage(enemies[i].hp);
            }

            for (int j = 0; j < civilianRequest; j++)
            {
                int civilianIndex = i * civilianRequest + j;
                if (civilianIndex < civilians.Length && civilians[civilianIndex] != null)
                {
                    civilians[civilianIndex].GetDamage(civilians[civilianIndex].hp);
                }
            }
        }
    }

    private void StartBattle()
    {
        if (warriors.Length >= warriorRequest)
        {
            foreach (Unit warrior in warriors)
            {
                if (warrior != null)
                    warrior.transform.SetParent(defArea);
            }
        }
        else if (civilians.Length >= civilianRequest)
        {
            foreach (Unit civilian in civilians)
            {
                if (civilian != null)
                    civilian.transform.SetParent(defArea);
            }
        }

        battleTimer = 0f;
    }

    private void EndWave()
    {
        isEnemyWave = false;
        battleStarted = false;
        unitsReturned = false;
        battleTimer = 0f;

        StartWaveTimer();
    }
    private void CollectUnits()
    {
        List<Unit> civilianList = new List<Unit>();
        foreach (Transform child in civilianSpawnArea)
        {
            if (child.CompareTag("Civilian"))
            {
                Unit unit = child.GetComponent<Unit>();
                if (unit != null) civilianList.Add(unit);
            }
        }

        foreach (Transform child in defArea)
        {
            if (child.CompareTag("Civilian"))
            {
                Unit unit = child.GetComponent<Unit>();
                if (unit != null) civilianList.Add(unit);
            }
        }
        civilians = civilianList.ToArray();


        List<Unit> warriorList = new List<Unit>();
        foreach (Transform child in warriorSpawnArea)
        {
            if (child.CompareTag("Warrior"))
            {
                Unit unit = child.GetComponent<Unit>();
                if (unit != null) warriorList.Add(unit);
            }
        }

        foreach (Transform child in defArea)
        {
            if (child.CompareTag("Warrior"))
            {
                Unit unit = child.GetComponent<Unit>();
                if (unit != null) warriorList.Add(unit);
            }
        }
        warriors = warriorList.ToArray();


        List<Enemy> enemyList = new List<Enemy>();
        foreach (Transform child in enemySpawnArea)
        {
            if (child.CompareTag("Enemy"))
            {
                Enemy enemy = child.GetComponent<Enemy>();
                if (enemy != null) enemyList.Add(enemy);
            }
        }
        enemies = enemyList.ToArray();
    }


    private void ReturnUnitsToBases()
    {
        foreach (Unit warrior in warriors)
        {
            if (warrior != null)
                warrior.transform.SetParent(warriorSpawnArea);
        }
        foreach (Unit civilian in civilians)
        {
            if (civilian != null)
                civilian.transform.SetParent(civilianSpawnArea);
        }
    }


    private void StartWaveTimer()
    {
        if (waveTimerRunning) return;
        waveTimerRunning = true;
        StartCoroutine(WaveDelayCoroutine());
    }

    public void GetCastleDamage(int damage)
    {
        castle.hpValue -= damage;
        castle.isDamage = true;

        if (castle.hpValue <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private IEnumerator WaveDelayCoroutine()
    {
        yield return new WaitForSeconds(waveDelay);
        waveTimerRunning = false;
        isEnemyWave = true;
    }
}