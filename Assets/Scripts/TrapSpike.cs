using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] float delayUpTime;         // ���� �ö���ִ� �ð�.
    [SerializeField] float delayDownTime;       // �Ʒ��� �������ִ� �ð�.

    bool isShow;        // Ʈ���� �������ִ°�?
    float time;         // �ð�. 

    private void Start()
    {
        isShow = true;
        time = 0.0f;
    }
    private void Update()
    {
        // Time.delatTime : ���� ������ ���� ���� �����ӱ��� �ɸ� �ð�.
        // time : �ð��� �帧�� ��Ÿ���� float�� ����.
        // anim.isPlaying <bool> : ���� �ִϸ��̼��� ����ϰ� �ִ°�?
        if (!anim.isPlaying)
            time += Time.deltaTime;

        // ������ �������� �ʰ� time�� downŸ�Ӻ��� Ŭ ��.
        if(!isShow && time >= delayDownTime)
        {
            anim.Play("Trap_Show");             // Trap_Show��� �̸��� ���� �ִϸ��̼� clip�� ����϶�.
            isShow = true;                      // isShow�� ���� true�� ����.
            time = 0.0f;                        // time�� 0.0f �� ����.
        }
        // ������ �������ְ� time�� upŸ�Ӻ��� Ŭ ��.
        else if(isShow && time >= delayUpTime)
        {
            anim.Play("Trap_Down");
            isShow = false;
            time = 0.0f;
        }
    }

    // Ʈ���ſ� �浹�ϴ� �� ���� 1ȸ ȣ��.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null)
            player.OnContactTrap(this);
    }
    // Ʈ���ſ� �浹�ϴ� ���� ��� ȣ��.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerStay:{collision.name}");
    }
    // Ʈ���ſ� �浹�� �Ͼ�� �ʰ� �Ǵ� ���� 1ȸ ȣ��.
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerExit:{collision.name}");
    }

    // (���� �ݶ��̴��̰�) �ݶ��̴��� �浹�ϴ� �� ���� 1ȸ ȣ��.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"OnCollisonEnter:{collision.collider.name}");
    }


    /*
    // �ش� ��ũ��Ʈ�� ���� ������Ʈ�� Ȱ��ȭ �� �� ���ʿ� 1��. (Start���� ����)
    // ��ũ��Ʈ�� �� Ȱ��ȭ�Ǿ� �־ �Ҹ�.
    private void Awake()
    {
        Debug.Log("Awake");
    }

    // �ش� ��ũ��Ʈ�� ���� ������Ʈ�� Ȱ��ȭ �� �� ���ʿ� 1��.
    void Start()
    {
        Debug.Log("Start");
    }

    // ������Ʈ�� Ȱ��ȭ �� ������ ȣ��.
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    // ������Ʈ�� ��Ȱ��ȭ �� ������ ȣ��.
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    // ���� ���꿡 ���õ� ó���� �� �� ����ϴ� �̺�Ʈ �Լ�.
    private void FixedUpdate()
    {
       // Debug.Log("FixedUpdate");
    }

    // �� �����Ӹ��� ȣ��Ǵ� �̺�Ʈ �Լ�.
    // => �ش� ��ũ��Ʈ�� Ȱ��ȭ �Ǿ� �־�� ȣ��.
    void Update()
    {
       // Debug.Log("Update");
    }

    // Update �Ŀ� �Ҹ��� Update �Լ�.
    // => ���� Update���� ���Ŀ� ���� ������ ó���� �ϰ� ���� �� ���.
    private void LateUpdate()
    {
       // Debug.Log("Late Update");
    }

    // ������ ���߰ų� �ٽ� ������ �� ȣ��Ǵ� �Լ�.
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"OnApplicationPause : {pause}");
    }
    // ���α׷��� ��Ŀ�� �ǰų� Ǯ���� �� �Ҹ��� �Լ�.
    private void OnApplicationFocus(bool focus)
    {
        Debug.Log($"OnApplicationFocus : {focus}");
    }
    // ������ ����Ǵ� ������ ȣ��.
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }
     */

}
