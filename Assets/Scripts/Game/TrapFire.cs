using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : Trap
{
    [SerializeField] float delayTime;           // 딜레이 시간.
    [SerializeField] float continueTime;        // 지속 시간.

    Animator anim;      // 애니메이터.
    bool isOn;          // 작동중인가?
    bool isFire;        // 불이 나오고 있는가?

    private void Start()
    {
        anim = GetComponent<Animator>();    // 오브젝트에 붙어있는 Animator검색.
        isOn = false;
        isFire = false;
    }

    // 함정이 눌렸다.
    public override void OnContactEnter(GameObject gameObject)
    {
        if (isOn)
            return;

        isOn = true;
        anim.SetTrigger("switch");

        // continueTime 후에 OnStopTrap 함수를 호출하라.
        Invoke(nameof(OnStartFire), delayTime);
    }

    // 불과 충돌했다.
    public override void OnContactStay(GameObject target)
    {
        if (!isFire)
            return;

        Player player = target.GetComponent<Player>();
        if (player != null)
            player.OnContactTrap(gameObject);
    }

    // 트랩이 작동 중이다.
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
