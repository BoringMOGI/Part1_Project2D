using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Player player;

    private void LateUpdate()
    {
        // 플레이어가 죽지 않았다면 위치를 갱신.
        if(!player.isDead)
            transform.position = player.transform.position + offset;
    }
}
