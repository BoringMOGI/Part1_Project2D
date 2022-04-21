using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float leftDistance;        // 왼쪽으로 이동할 거리.
    [SerializeField] float rightDistance;       // 오른쪽으로 이동할 거리.

    [SerializeField] float moveSpeed;           // 움직이는 속도.
    [SerializeField] bool isRight;              // 오른족으로 이동하는지?

    Vector3 originPos;                          // 최초 위치.
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
        Vector3 destination = isRight ? rightPos : leftPos;                     // 목적지.
        float movement = moveSpeed * Time.deltaTime * (isRight ? 1f : -1f);     // 이동량.
        transform.position += new Vector3(movement, 0f, 0f);                    // 이동량 만큼 위치 조정.
            
        // 플레이어가 내 위에 있다면.
        if(player != null)
            player.position += new Vector3(movement, 0f, 0f);

        // Mathf.Abs(value) : 값을 절대값으로 변경.
        if(Vector3.Distance(transform.position, destination) < Mathf.Abs(movement))  // 목적지와의 거리가 짧을때.
            isRight = !isRight;                                                      // bool값 반전.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 물체가 player와 같다면.
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = Player.Instance.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 충돌을 벗어난 물체가 player라면.
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = null;
        }
    }

    // Gizmo 그리기.
    // 내가 이동할 왼쪽 & 오른쪽 위치를 그린다.
    private void OnDrawGizmosSelected()
    {
        // 게임이 실행중이 아니라면
        if (Application.isPlaying == false)
        {
            leftPos = transform.position - new Vector3(leftDistance, 0, 0);
            rightPos = transform.position + new Vector3(rightDistance, 0, 0);                        
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPos, 0.1f);     // 와이어형(선으로 이루어진) 원형을 그려라. (위치, 반지름)
        Gizmos.DrawWireSphere(rightPos, 0.1f);

    }
}
