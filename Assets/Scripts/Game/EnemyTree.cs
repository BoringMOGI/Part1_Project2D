using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float attackDelay;
    [SerializeField] Animator anim;

    float attackTime = 0.0f;

    private void Update()
    {
        Attack();
        
    }

    void Attack()
    {
        attackTime += Time.deltaTime;       // 공격 시간을 시간 값으로 더해간다.
        if(attackTime > attackDelay)        // 공격 시간이 딜레이 시간을 넘기면.
        {
            // 투사체 발사.
            GameObject bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
            bullet.name = "Tree_Bullet";
            attackTime = 0.0f;
        }
    }

}
