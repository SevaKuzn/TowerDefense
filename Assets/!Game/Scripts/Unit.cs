using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] public int hp = 10;
    [SerializeField] public int damage = 2;
    [SerializeField] private GameObject unitPrefab;

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetDamage(int enemyDamage)
    {
        hp -= enemyDamage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GiveDamage(Enemy target)
    {
        if (anim != null)
        {
            anim.SetBool("isAttack", true);
        }
        if (target != null)
        {
            target.GetDamage(damage);
        }
        if (anim != null)
        {
            anim.SetBool("isAttack", true);
        }
    }
}