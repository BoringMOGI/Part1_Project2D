using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator anim;
    new Collider2D collider2D;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 나랑 부딪히면.
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.OnContactCoin(this);          // 플레이어에게 알려주기.
            collider2D.enabled = false;          // 충돌체 끄기.
            anim.SetTrigger("onEat");            // onEat트리거 누르기.
            AudioManager.Instance.PlaySE("eat"); // 먹는 효과음 재생.
        }
    }
    private void OnDestroyCoin()
    {
        Destroy(gameObject);        // Coin 스크립트가 존재하는 게임 오브젝트를 지워라.
    }
}
