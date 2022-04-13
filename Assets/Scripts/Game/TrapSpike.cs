using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] float delayUpTime;         // 위로 올라와있는 시간.
    [SerializeField] float delayDownTime;       // 아래로 내려가있는 시간.

    bool isShow;        // 트랩이 등장해있는가?
    float time;         // 시간. 

    private void Start()
    {
        isShow = true;
        time = 0.0f;
    }
    private void Update()
    {
        // Time.delatTime : 이전 프레임 부터 현재 프레임까지 걸린 시간.
        // time : 시간의 흐름을 나타내는 float형 변수.
        // anim.isPlaying <bool> : 현재 애니메이션을 재생하고 있는가?
        if (!anim.isPlaying)
            time += Time.deltaTime;

        // 함정이 등장하지 않고 time이 down타임보다 클 때.
        if(!isShow && time >= delayDownTime)
        {
            anim.Play("Trap_Show");             // Trap_Show라는 이름을 가진 애니메이션 clip을 재생하라.
            isShow = true;                      // isShow의 값을 true로 대입.
            time = 0.0f;                        // time에 0.0f 값 대입.
        }
        // 함정이 등장해있고 time이 up타임보다 클 때.
        else if(isShow && time >= delayUpTime)
        {
            anim.Play("Trap_Down");
            isShow = false;
            time = 0.0f;
        }
    }

    // 트리거와 충돌하는 그 순간 1회 호출.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if(player != null)
            player.OnContactTrap(this);
    }
    // 트리거와 충돌하는 동안 계속 호출.
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerStay:{collision.name}");
    }
    // 트리거와 충돌이 일어나지 않게 되는 순간 1회 호출.
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerExit:{collision.name}");
    }

    // (내가 콜라이더이고) 콜라이더와 충돌하는 그 순간 1회 호출.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log($"OnCollisonEnter:{collision.collider.name}");
    }


    /*
    // 해당 스크립트가 붙은 오브젝트가 활성화 될 때 최초에 1번. (Start보다 먼저)
    // 스크립트가 비 활성화되어 있어도 불림.
    private void Awake()
    {
        Debug.Log("Awake");
    }

    // 해당 스크립트가 붙은 오브젝트가 활성화 될 때 최초에 1번.
    void Start()
    {
        Debug.Log("Start");
    }

    // 오브젝트가 활성화 될 때마다 호출.
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    // 오브젝트가 비활성화 될 때마다 호출.
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    // 물리 연산에 관련된 처리를 할 때 사용하는 이벤트 함수.
    private void FixedUpdate()
    {
       // Debug.Log("FixedUpdate");
    }

    // 매 프레임마다 호출되는 이벤트 함수.
    // => 해당 스크립트가 활성화 되어 있어야 호출.
    void Update()
    {
       // Debug.Log("Update");
    }

    // Update 후에 불리는 Update 함수.
    // => 보통 Update루프 이후에 계산된 값으로 처리를 하고 싶을 때 사용.
    private void LateUpdate()
    {
       // Debug.Log("Late Update");
    }

    // 게임이 멈추거나 다시 실행했 때 호출되는 함수.
    private void OnApplicationPause(bool pause)
    {
        Debug.Log($"OnApplicationPause : {pause}");
    }
    // 프로그램이 포커싱 되거나 풀렸을 때 불리는 함수.
    private void OnApplicationFocus(bool focus)
    {
        Debug.Log($"OnApplicationFocus : {focus}");
    }
    // 게임이 종료되는 순간에 호출.
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
    }
     */

}
