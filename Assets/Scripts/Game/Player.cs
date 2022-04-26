using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float godModeTime;     // ���� �ð�.
    [SerializeField] int maxHp;             // �÷��̾��� �ִ� ü��.

    [Header("Attack")]
    [SerializeField] Transform footPivot;   // �� ��ġ.
    [SerializeField] float attackRadius;    // ���� ����.
    [SerializeField] LayerMask attackMask;  // ���� ����ũ.

    // ������ �������� ĳ���ؼ� ���� ���� ����.
    // GetComponent�� ������Ʈ�� �˻��ϱ� ������ �޸� ȿ���� ���� �ʴ�.
    // ���� �ʱ�ȭ ������ �̸� �˻��ϰ� ���Ŀ��� ������ ����Ѵ�.
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;
    Rigidbody2D rigid;

    bool isGodMode;         // ��������ΰ�?
    bool isFallDown;        // �÷��̾ �Ʒ��� ��������.
    int hp;                 // ���� ü��.

    public int Hp => hp;        // int�� ������Ƽ. Hp�� �����ϸ� hp���� �����ϰڴ�.

    // ������Ƽ : isDead�� ������ �����ϸ� ���� ���� �Ʒ� ���� �������� ��� ���̴�.
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
        // ���� �ϰ� ���� �ƴ϶�� ����.
        if (rigid.velocity.y >= 0f)
            return;

        Collider2D contact = Physics2D.OverlapCircle(footPivot.position, attackRadius, attackMask);
        if(contact != null)
        {
            EnemyTree enemy = contact.GetComponent<EnemyTree>();
            if(enemy != null)
            {
                enemy.OnDamaged();
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);     // y���� �ӵ��� 0���� ��ȯ.
                rigid.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);   // �������� 3�� ����ŭ ������.
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

    // Hit�ִϸ��̼� Ŭ�� �̺�Ʈ.
    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Vector3 hitPosition)
    {
        if ((hp -= 1) <= 0)     // ü���� 1 ���� �� 0���϶��.
        {
            OnDead();           // �״´�.
            Collider2D collider = GetComponent<Collider2D>();       // �ݶ��̴� �˻�.
            collider.isTrigger = true;                              // Ʈ���ŷ� �����Ѵ�.
        }

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);        // �� ���� ����.
        anim.SetBool("isHit", true);                            // �ǰ� �ִϸ��̼� ���� ����.
        anim.SetTrigger("onHit");                               // onHit Ʈ���� ���.

        // nameof(Method) : �Լ����� string ���ڿ��� ��ȯ�Ѵ�.
        // Invoke(string, int) : void
        // => Ư�� �Լ��� n�� �Ŀ� ȣ���϶�.
        Invoke(nameof(ReleaseGodMode), godModeTime);            // n�� �Ŀ� ���� Ǯ��.
        StartCoroutine(FlipPlayer());                           // �÷��̾��� ��¦��.

        // �� ������Ʈ���� Movement�� �˻��Ѵ�.
        // ���� OnThrow�Լ��� trap�� transform���� ���� ȣ���Ѵ�.
        yield return null;

        movement.OnThrow(hitPosition);
    }
    private void OnDead()
    {

    }
    private void ReleaseGodMode()
    {
        isGodMode = false;                      // ���� ����.
        spriteRenderer.color = Color.white;     // �� ������ �ǵ���.
    }

    // �ڷ�ƾ.
    private IEnumerator FlipPlayer()
    {
        Color red = new Color(1, 0, 0, 0.5f);
        Color white = new Color(1, 1, 1, 0.5f);

        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);      // 0.1�� ���.
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);      // 0.1�� ���.
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
