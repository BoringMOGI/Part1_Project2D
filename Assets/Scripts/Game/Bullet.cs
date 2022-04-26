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
        // ���� �ð��� ���ҽ�Ų �� 0.0f ���ϰ� �Ǹ� ���ӿ�����Ʈ�� �����϶�.
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0.0f)
            Destroy(gameObject);

        // �� ��ġ�� ����*�ӵ� ��ŭ �����̰ڴ�.
        transform.position += direction * speed * Time.deltaTime;
    }
}
