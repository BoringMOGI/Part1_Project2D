using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;    

    Vector3 direction;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.layer == LayerMask.NameToLayer("Ground")
        Player player = collision.GetComponent<Player>();
        if (player != null)
            player.OnContactTrap(gameObject);

        Destroy(gameObject);
    }

    private void Update()
    {        
        // 생존 시간을 감소시킨 후 0.0f 이하가 되면 게임오브젝트를 삭제하라.
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
            Destroy(gameObject);

        // 내 위치를 방향*속도 만큼 움직이겠다.
        transform.position += direction * speed * Time.deltaTime;
    }
}
