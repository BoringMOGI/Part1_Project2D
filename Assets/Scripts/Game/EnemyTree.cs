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
        attackTime += Time.deltaTime;       // ���� �ð��� �ð� ������ ���ذ���.
        if(attackTime > attackDelay)        // ���� �ð��� ������ �ð��� �ѱ��.
        {
            // ����ü �߻�.
            GameObject bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
            bullet.name = "Tree_Bullet";
            attackTime = 0.0f;
        }
    }

}
