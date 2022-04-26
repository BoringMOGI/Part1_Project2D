using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float attackDelay;
    [SerializeField] int hp;
    
    Animator anim;
    float attackTime = 0.0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 시간이 다 되면 애니메이션을 재생한다.
        attackTime += Time.deltaTime;
        if (attackTime > attackDelay)
        {
            anim.SetTrigger("onAttack");
            attackTime = 0.0f;
        }
    }

    void Fire()
    {
        // 투사체 발사.
        Bullet bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        bullet.name = "Tree_Bullet";

        bullet.Shoot(Vector3.left);
    }

    public void OnDamaged()
    {
        hp -= 1;
        if (hp <= 0)
            Destroy(gameObject);
        else
            anim.SetTrigger("onDamaged");
        
    }

}
