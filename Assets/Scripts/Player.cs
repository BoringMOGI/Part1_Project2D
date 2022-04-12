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
        hp = maxHp;
    }

    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
            return;

        // �� ������Ʈ���� Movement�� �˻��Ѵ�.
        // ���� OnThrow�Լ��� trap�� transform���� ���� ȣ���Ѵ�.
        Movement movement = gameObject.GetComponent<Movement>();
        movement.OnThrow(trap.transform);

        OnHit();
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

    private void OnHit()
    {
        if (isGodMode)
            return;
                
        if ((hp -= 1) <= 0)     // ü���� 1 ���� �� 0���϶��.
        {
            OnDead();           // �״´�.
        }
        else
        {
            isGodMode = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);        // �� ���� ����.

            // nameof(Method) : �Լ����� string ���ڿ��� ��ȯ�Ѵ�.
            // Invoke(string, int) : void
            // => Ư�� �Լ��� n�� �Ŀ� ȣ���϶�.
            Invoke(nameof(ReleaseGodMode), godModeTime);
            StartCoroutine(FlipPlayer());
        }
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
