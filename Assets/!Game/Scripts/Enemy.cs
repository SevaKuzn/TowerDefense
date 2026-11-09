using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] public int hp = 10;
    [SerializeField] public int damage = 1;
    [SerializeField] private GameObject enemyPrefab;

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
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

    public void GetDamage(int unitDamage)
    {
        hp -= unitDamage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            if (anim != null)
            {
                anim.SetBool("isDie", true);
            }
        }
    }

    public void GiveDamage()
    {
        if (anim != null)
        {
            anim.SetBool("isAttack", true);
        }

        if (anim != null)
        {
            anim.SetBool("isAttack", false);
        }
    }
}