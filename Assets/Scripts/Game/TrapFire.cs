using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : Trap
{
    [SerializeField] float delayTime;           // ������ �ð�.
    [SerializeField] float continueTime;        // ���� �ð�.

    Animator anim;      // �ִϸ�����.
    bool isOn;          // �۵����ΰ�?
    bool isFire;        // ���� ������ �ִ°�?

    private void Start()
    {
        anim = GetComponent<Animator>();    // ������Ʈ�� �پ��ִ� Animator�˻�.
        isOn = false;
        isFire = false;
    }

    // ������ ���ȴ�.
    public override void OnContactEnter(GameObject gameObject)
    {
        if (isOn)
            return;

        isOn = true;
        anim.SetTrigger("switch");

        // continueTime �Ŀ� OnStopTrap �Լ��� ȣ���϶�.
        Invoke(nameof(OnStartFire), delayTime);
    }

    // �Ұ� �浹�ߴ�.
    public override void OnContactStay(GameObject target)
    {
        if (!isFire)
            return;

        Player player = target.GetComponent<Player>();
        if (player != null)
            player.OnContactTrap(gameObject);
    }

    // Ʈ���� �۵� ���̴�.
    private void OnStartFire()
    {
        anim.SetTrigger("onFire");
        isFire = true;
        Invoke(nameof(OnStopFire), continueTime);
    }

    private void OnStopFire()
    {
        anim.SetTrigger("offFire");
        isFire = false;
        isOn = false;
    }


}
