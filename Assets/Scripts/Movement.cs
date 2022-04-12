using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundRadius;
    [SerializeField] float throwPower;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    bool isGrounded
    {
        get
        {
            return anim.GetBool("isGrounded");
        }
        set
        {
            // value : isGrounded에 대입한 값 그 자체다.
            anim.SetBool("isGrounded", value);
        }
    }
    bool isJump;                // 내가 점프를 했는가?
    bool isLockControl;         // 컨트롤(제어)가 막혔는가?

    // start함수는 이벤트 함수로 게임 시작 시 최초 1회 실행된다.
    void Start()
    {
        //Vector3 position = transform.position;
        //transform.position = new Vector3(1.0f, 1.0f, 0.0f);

        // Translate는 현 위치에서 Vector3만큼 이동해라.
        //transform.Translate(new Vector3(10.0f, 0.0f, 0.0f));
    }

    // update함수는 이벤트 함수로 매 프레임마다 1회 호출된다.
    void Update()
    {
        CheckGround();

        // 플레이어가 죽지 않았고 컨트롤이 막히지 않았을 경우.
        if (!player.isDead && !isLockControl)
        {
            Move();
            Jump();
        }

        // Rigidbody2D에 현재 오브젝트의 속련에 관한 변수가 있다.
        // Vector2 rigid.velocity;
        anim.SetFloat("velocityY", rigid.velocity.y);
    }

    void CheckGround()
    {
        // 내가 상승중일때는 땅에 있지 않다.
        if (rigid.velocity.y > 0.0f)
        {
            isGrounded = false;
            return;
        }

        // 광선을 쏜다.
        // pivot(원점)은 내 위치이고 방향은 아래쪽, 거리는 0.3
        // groundMask에 해당하는 레이어인 충돌체를 감지하겠다.
        // hit는 내가 충돌한 '충돌체'의 정보를 들고있다.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRadius, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            isJump = false;
            isLockControl = false;
        }
    }
    void Move()
    {
        // GetAsisRaw는 -1,0,1의 값을 리턴한다.
        // Horizontal기준으로 왼쪽은 -1, 오른쪽은 1, 비입력은 0.
        // Vertical기준으로 위쪽은 1, 아래쪽은 -1, 비입력은 0.
        int x = (int)Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector3.right * x * speed * Time.deltaTime);
        anim.SetInteger("horizontal", x);
        if (x != 0)
        {
            spriteRenderer.flipX = (x == -1);
        }
    }
    void Jump()
    {
        // KeyDown : 키를 입력하는 순간 1번.
        // KeyUp   : 키를 때는 순간 1번
        // Key     : 누르고 있는 동안 계속.
        if (Input.GetKeyDown(KeyCode.Space) && !isJump && isGrounded)
        {
            // Rigidbody2D.AddForce(Vector3, ForceMode2D) : void
            //  => Vector3방향 + 힘으로 힘을 가한다.
            //  => ForceMode2D.Force   : 민다.
            //  => ForceMode2D.Impulse : 폭발적인 힘을 가한다.
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySE("jump");
        }
    }


    public void OnThrow(Transform targetPivot)
    {
        // 내 위치 - 상대방의 위치 = 상대방에서 내 위치로 보는 방향.
        Vector3 direction =  transform.position - targetPivot.position;
        direction.Normalize();          // 벡터 값의 정규화.
        direction.y = 1;                // y축 벡터 제거.

        // direction 방향으로 throwPower만큼 (한번에)힘을 가하라.
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse);
        isLockControl = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
  
}
