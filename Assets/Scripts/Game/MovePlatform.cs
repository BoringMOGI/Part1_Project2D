using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] Vector2[] destinations;    // �̵��ؾ��� ��ġ �迭.
    [SerializeField] float moveSpeed;           // �����̴� �ӵ�.
    [SerializeField] bool isReverse;            // �ݴ�� �����̴°�?

    Vector3 originPos;                          // ���� ��ġ.
    Transform player;                           // �÷��̾�.
    int index;                                  // ������ index.

    private void Start()
    {
        originPos = transform.position;
        transform.position = GetDestination(index);
    }
    private void Update()
    {
        Vector3 destination = GetDestination(index);

        // MoveTowards : ���� ��ġ���� Ư�� ��ġ �������� �̵�����ŭ �������� ���� ������.
        Vector3 beforePos = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // �÷��̾ �� ���� �ִٸ�.
        if (player != null)
        {
            Vector3 movement = transform.position - beforePos;  // ���� ��ġ - ���� ��ġ : �̵���.
            player.position += movement;                        // �÷��̾��� ��ġ�� �̵�����ŭ �����δ�.
        }

        // Mathf.Abs(value) : ���� ���밪���� ����.
        // �������� ������ �ߴٸ�.
        if(transform.position == destination)
        {
            // ���������� ���� �־��µ� index�� ������ ��ġ���
            if(!isReverse && index == destinations.Length - 1)
            {
                isReverse = true;
            }
            else if(isReverse && index == 0)
            {
                isReverse = false;
            }

            index += isReverse ? -1 : 1;
        }
    }

    private Vector3 GetDestination(int index)
    {
        Vector3 position = destinations[index];
        Vector3 destination = originPos + position;
        return destination;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü�� player�� ���ٸ�.
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = Player.Instance.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹�� ��� ��ü�� player���.
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = null;
        }
    }

    // Gizmo �׸���.
    // ���� �̵��� ���� & ������ ��ġ�� �׸���.
    private void OnDrawGizmosSelected()
    {
        if (destinations == null)
            return;

        // ������ �������� �ƴ϶��
        if (!Application.isPlaying)
            originPos = transform.position;                

        Gizmos.color = Color.red;
        for (int i = 0; i < destinations.Length; i++)
        {
            Vector3 pos = destinations[i];
            Gizmos.DrawSphere(originPos + pos, 0.1f);
        }

    }
}
