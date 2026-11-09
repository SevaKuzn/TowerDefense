using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] public int hp = 10;
    [SerializeField] public int damage = 1;
    [SerializeField] private GameObject enemyPrefab;

    private bool isDie = false;
    private bool s1;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        SetWalk(true);
        StartCoroutine(WalkDelay(5f));
    }

    void Update()
    {
        if (hp <= 0)
        {
            anim.SetBool("isDie", true);
        }
    }

    public void Init(RectTransform spawnArea)
    {
        GameObject instance = Instantiate(enemyPrefab, spawnArea);
        Enemy newEnemy = instance.GetComponent<Enemy>();
        if (newEnemy != null)
        {
            newEnemy.SetWalk(true);
        }
    }

    public void SetWalk(bool isActive)
    {
        if (anim != null)
        {
            anim.SetBool("isWalk", isActive);
        }     
    }

    public void SetDie(bool isActive)
    {
        if (anim != null)
        {
            anim.SetBool("isDie", isActive);
        }
    }

    public void SetAttack()
    {
        if (anim != null)
        {
            anim.SetTrigger("isAttack");
        }
    }

    public void GetDamage(int unitDamage)
    {
        hp -= unitDamage;
        if (hp <= 0)
        {
            StartCoroutine(DeathDelay(5f));
        }
    }

    IEnumerator WalkDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetWalk(false);
    }

    IEnumerator DeathDelay(float delay)
    {
        SetDie(true);
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}