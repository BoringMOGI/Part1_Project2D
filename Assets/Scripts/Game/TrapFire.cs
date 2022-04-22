using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] GameObject fireCollider;   // �� �浹 üũ ����.
    [SerializeField] float delayTime;           // ������ �ð�.
    [SerializeField] float continueTime;        // ���� �ð�.

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

        // continueTime �Ŀ� OnStopTrap �Լ��� ȣ���϶�.
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
