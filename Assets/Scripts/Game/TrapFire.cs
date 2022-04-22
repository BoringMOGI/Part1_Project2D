using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] GameObject fireCollider;   // 불 충돌 체크 영역.
    [SerializeField] float delayTime;           // 딜레이 시간.
    [SerializeField] float continueTime;        // 지속 시간.

    Animator anim;
    bool isOn;

    private void Start()
    {
        anim = GetComponent<Animator>();
        fireCollider.SetActive(false);
        isOn = false;
    }

    public void OnSwitchTrap()
    {
        if (isOn)
            return;

        anim.SetTrigger("switch");
        isOn = true;

        // continueTime 후에 OnStopTrap 함수를 호출하라.
        Invoke(nameof(OnStartTrap), delayTime);
    }
    public void OnContactFire(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if (player != null)
            player.OnContactTrap(gameObject);
    }

    private void OnStartTrap()
    {
        fireCollider.SetActive(true);
        anim.SetTrigger("on");
        Invoke(nameof(OnStopTrap), continueTime);
    }

    private void OnStopTrap()
    {
        anim.SetTrigger("off");
        isOn = false;
    }


}
