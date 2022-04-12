using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float godModeTime;     // 무적 시간.
    [SerializeField] int maxHp;             // 플레이어의 최대 체력.

    // 참조형 변수들은 캐싱해서 쓰는 것이 좋다.
    // GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않다.
    // 따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용한다.
    SpriteRenderer spriteRenderer;

    bool isGodMode;         // 무적모드인가?
    bool isFallDown;        // 플레이어가 아래로 떨어졌다.
    int hp;                 // 현재 체력.
    int coin;               // 보유 코인.

    public int Hp => hp;        // int형 프로퍼티. Hp를 참조하면 hp값을 리턴하겠다.
    public int Coin => coin;    // Coin을 참조하면 coin값을 리턴하겠다.

    // 프로퍼티 : isDead는 참조만 가능하며 리턴 값은 아래 조건 연산자의 결과 값이다.
    public bool isDead => (hp <= 0) || isFallDown;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = maxHp;
    }

    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
            return;

        // 내 오브젝트에서 Movement를 검색한다.
        // 이후 OnThrow함수를 trap의 transform으로 보내 호출한다.
        Movement movement = gameObject.GetComponent<Movement>();
        movement.OnThrow(trap.transform);

        OnHit();
    }
    public void OnContactCoin(Coin target)
    {
        coin += 1;
        Debug.Log($"코인 획득, 보유코인 : {coin}");
    }
    public void OnFallDown()
    {
        isFallDown = true;
    }

    private void OnHit()
    {
        if (isGodMode)
            return;
                
        if ((hp -= 1) <= 0)     // 체력을 1 깍은 후 0이하라면.
        {
            OnDead();           // 죽는다.
        }
        else
        {
            isGodMode = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);        // 반 투명 상태.

            // nameof(Method) : 함수명을 string 문자열로 변환한다.
            // Invoke(string, int) : void
            // => 특정 함수를 n초 후에 호출하라.
            Invoke(nameof(ReleaseGodMode), godModeTime);
            StartCoroutine(FlipPlayer());
        }
    }
    private void OnDead()
    {

    }
    private void ReleaseGodMode()
    {
        isGodMode = false;                      // 무적 해제.
        spriteRenderer.color = Color.white;     // 원 색으로 되돌림.
    }


    // 코루틴.
    private IEnumerator FlipPlayer()
    {
        Color red = new Color(1, 0, 0, 0.5f);
        Color white = new Color(1, 1, 1, 0.5f);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);      // 0.1초 대기.
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);      // 0.1초 대기.
        }
    }
}
