using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float godModeTime;     // 무적 시간.
    [SerializeField] int maxHp;             // 플레이어의 최대 체력.

    [Header("Attack")]
    [SerializeField] Transform footPivot;   // 발 위치.
    [SerializeField] float attackRadius;    // 공격 범위.
    [SerializeField] LayerMask attackMask;  // 공격 마스크.

    // 참조형 변수들은 캐싱해서 쓰는 것이 좋다.
    // GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않다.
    // 따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용한다.
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;
    Rigidbody2D rigid;

    bool isGodMode;         // 무적모드인가?
    bool isFallDown;        // 플레이어가 아래로 떨어졌다.
    int hp;                 // 현재 체력.

    public int Hp => hp;        // int형 프로퍼티. Hp를 참조하면 hp값을 리턴하겠다.

    // 프로퍼티 : isDead는 참조만 가능하며 리턴 값은 아래 조건 연산자의 결과 값이다.
    public bool isDead => (hp <= 0) || isFallDown;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        hp = maxHp;

        StartPoint.Instance.SetStartPoint(transform);    
    }
    private void Update()
    {
        OnCheckAttack();
    }

    private void OnCheckAttack()
    {
        // 내가 하강 중이 아니라면 리턴.
        if (rigid.velocity.y >= 0f)
            return;

        Collider2D contact = Physics2D.OverlapCircle(footPivot.position, attackRadius, attackMask);
        if(contact != null)
        {
            EnemyTree enemy = contact.GetComponent<EnemyTree>();
            if(enemy != null)
            {
                enemy.OnDamaged();
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);     // y축의 속도만 0으로 변환.
                rigid.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);   // 위쪽으로 3의 힘만큼 날린다.
            }
        }
    }

    public void OnContactTrap(GameObject trap)
    {
        if (isGodMode)
            return;

        StartCoroutine(OnHit(trap.transform.position)); 
    }
    public void OnContactCoin(Coin target)
    {
        GameManager.Instance.AddEatCount(1); 
    }
    public void OnFallDown()
    {
        isFallDown = true;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        movement.OnSwitchLockControl(isLock);
    }

    // Hit애니메이션 클립 이벤트.
    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Vector3 hitPosition)
    {
        if ((hp -= 1) <= 0)     // 체력을 1 깍은 후 0이하라면.
        {
            OnDead();           // 죽는다.
            Collider2D collider = GetComponent<Collider2D>();       // 콜라이더 검색.
            collider.isTrigger = true;                              // 트리거로 변경한다.
        }

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);        // 반 투명 상태.
        anim.SetBool("isHit", true);                            // 피격 애니메이션 변수 변경.
        anim.SetTrigger("onHit");                               // onHit 트리거 당김.

        // nameof(Method) : 함수명을 string 문자열로 변환한다.
        // Invoke(string, int) : void
        // => 특정 함수를 n초 후에 호출하라.
        Invoke(nameof(ReleaseGodMode), godModeTime);            // n초 후에 무적 풀림.
        StartCoroutine(FlipPlayer());                           // 플레이어의 반짝임.

        // 내 오브젝트에서 Movement를 검색한다.
        // 이후 OnThrow함수를 trap의 transform으로 보내 호출한다.
        yield return null;

        movement.OnThrow(hitPosition);
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


    private void OnDrawGizmos()
    {
        if(footPivot != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(footPivot.position, attackRadius);
        }
    }
}
