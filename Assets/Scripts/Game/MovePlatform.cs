using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float leftDistance;        // �������� �̵��� �Ÿ�.
    [SerializeField] float rightDistance;       // ���������� �̵��� �Ÿ�.

    [SerializeField] float moveSpeed;           // �����̴� �ӵ�.
    [SerializeField] bool isRight;              // ���������� �̵��ϴ���?

    Vector3 originPos;                          // ���� ��ġ.
    Vector3 leftPos;
    Vector3 rightPos;

    Transform player;

    private void Start()
    {
        originPos = transform.position;
        leftPos = transform.position - new Vector3(leftDistance, 0, 0);
        rightPos = transform.position + new Vector3(rightDistance, 0, 0);


        GameObject parent = new GameObject("parent");
        GameObject child = new GameObject("child");

        child.transform.SetParent(parent.transform);
        child.transform.SetParent(null);
    }
    private void Update()
    {
        Vector3 destination = isRight ? rightPos : leftPos;                     // ������.
        float movement = moveSpeed * Time.deltaTime * (isRight ? 1f : -1f);     // �̵���.
        transform.position += new Vector3(movement, 0f, 0f);                    // �̵��� ��ŭ ��ġ ����.
            
        // �÷��̾ �� ���� �ִٸ�.
        if(player != null)
            player.position += new Vector3(movement, 0f, 0f);

        // Mathf.Abs(value) : ���� ���밪���� ����.
        if(Vector3.Distance(transform.position, destination) < Mathf.Abs(movement))  // ���������� �Ÿ��� ª����.
            isRight = !isRight;                                                      // bool�� ����.
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
        // ������ �������� �ƴ϶��
        if (Application.isPlaying == false)
        {
            leftPos = transform.position - new Vector3(leftDistance, 0, 0);
            rightPos = transform.position + new Vector3(rightDistance, 0, 0);                        
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPos, 0.1f);     // ���̾���(������ �̷����) ������ �׷���. (��ġ, ������)
        Gizmos.DrawWireSphere(rightPos, 0.1f);

    }
}
