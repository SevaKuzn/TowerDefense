using UnityEngine;
using System.Collections;


public class Unit : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] public int hp = 10;
    [SerializeField] public int damage = 2;
    [SerializeField] private GameObject unitPrefab;

    void Update()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetAttack()
    {
        if (anim != null)
        {
            anim.SetTrigger("isAttack");
        }
    }

    public void GetDamage(int enemyDamage)
    {
        hp -= enemyDamage;
        if (hp <= 0)
        {
            StartCoroutine(DeathDelay(5f));
        }
    }

    public void GiveDamage(Enemy target)
    {
        SetAttack();
        if (target != null)
        {
            target.GetDamage(damage);
        }
    }

    IEnumerator DeathDelay(float delay)
    {
        SetAttack();
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}