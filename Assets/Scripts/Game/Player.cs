using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float godModeTime;     // ���� �ð�.
    [SerializeField] int maxHp;             // �÷��̾��� �ִ� ü��.

    // ������ �������� ĳ���ؼ� ���� ���� ����.
    // GetComponent�� ������Ʈ�� �˻��ϱ� ������ �޸� ȿ���� ���� �ʴ�.
    // ���� �ʱ�ȭ ������ �̸� �˻��ϰ� ���Ŀ��� ������ ����Ѵ�.
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;

    bool isGodMode;         // ��������ΰ�?
    bool isFallDown;        // �÷��̾ �Ʒ��� ��������.
    int hp;                 // ���� ü��.
    int coin;               // ���� ����.

    public int Hp => hp;        // int�� ������Ƽ. Hp�� �����ϸ� hp���� �����ϰڴ�.
    public int Coin => coin;    // Coin�� �����ϸ� coin���� �����ϰڴ�.

    // ������Ƽ : isDead�� ������ �����ϸ� ���� ���� �Ʒ� ���� �������� ��� ���̴�.
    public bool isDead => (hp <= 0) || isFallDown;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
        anim = GetComponent<Animator>();

        hp = maxHp;
    }

    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
            return;

        StartCoroutine(OnHit(trap.transform)); 
    }
    public void OnContactCoin(Coin target)
    {
        coin += 1;
        Debug.Log($"���� ȹ��, �������� : {coin}");
    }
    public void OnFallDown()
    {
        isFallDown = true;
    }

    // Hit�ִϸ��̼� Ŭ�� �̺�Ʈ.
    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Transform target)
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

        movement.OnThrow(target);
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
}
