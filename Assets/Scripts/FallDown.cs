using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();   // 충돌한 충돌체에게서 Player 검색.
        if (player != null)                                 // 플레이어를 검색했다면.
            player.OnFallDown();                            // 떨어졌다고 전달.
    }
}
