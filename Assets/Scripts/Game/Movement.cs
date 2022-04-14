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
            // value : isGrounded�� ������ �� �� ��ü��.
            anim.SetBool("isGrounded", value);
        }
    }
    bool isLockControl;         // ��Ʈ��(����)�� �����°�?
    int jumpCount;              // ���� ī��Ʈ.

    readonly int MAX_JUMP_COUNT = 2;

    // update�Լ��� �̺�Ʈ �Լ��� �� �����Ӹ��� 1ȸ ȣ��ȴ�.
    void Update()
    {
        CheckGround();

        // �÷��̾ ���� �ʾҰ� ��Ʈ���� ������ �ʾ��� ���.
        if (!player.isDead && !isLockControl)
        {
            Move();
            Jump();
        }

        // Rigidbody2D�� ���� ������Ʈ�� �ӷÿ� ���� ������ �ִ�.
        // Vector2 rigid.velocity;
        anim.SetFloat("velocityY", rigid.velocity.y);
        anim.SetInteger("jumpCount", jumpCount);
    }

    void CheckGround()
    {
        // ���� ������϶��� ���� ���� �ʴ�.
        if (rigid.velocity.y > 0.0f)
        {
            isGrounded = false;
            return;
        }

        // ������ ���.
        // pivot(����)�� �� ��ġ�̰� ������ �Ʒ���, �Ÿ��� 0.3
        // groundMask�� �ش��ϴ� ���̾��� �浹ü�� �����ϰڴ�.
        // hit�� ���� �浹�� '�浹ü'�� ������ ����ִ�.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRadius, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;            
            isLockControl = false;
            jumpCount = MAX_JUMP_COUNT;     // ���� ���� �꿴�� �� ���� ī��Ʈ Ƚ���� �ִ�� ������.
        }
    }
    void Move()
    {
        // GetAsisRaw�� -1,0,1�� ���� �����Ѵ�.
        // Horizontal�������� ������ -1, �������� 1, ���Է��� 0.
        // Vertical�������� ������ 1, �Ʒ����� -1, ���Է��� 0.
        int x = (int)Input.GetAxisRaw("Horizontal");

        //transform.Translate(Vector3.right * x * speed * Time.deltaTime);
        // Translate�� �����̵��̱� ������ ���� ó������ �ڿ������� �ʴ�.
        // ���� Velocity(�ӷ�)�� �̿��� ĳ���͸� �̵���Ų��.
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);
        anim.SetInteger("horizontal", x);
        if (x != 0)
        {
            spriteRenderer.flipX = (x == -1);
        }
    }
    void Jump()
    {
        // KeyDown : Ű�� �Է��ϴ� ���� 1��.
        // KeyUp   : Ű�� ���� ���� 1��
        // Key     : ������ �ִ� ���� ���.

        // ����Ű�� ������ ���� Ƚ���� 0���� Ŭ ��.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            // Rigidbody2D.AddForce(Vector3, ForceMode2D) : void
            //  => Vector3���� + ������ ���� ���Ѵ�.
            //  => ForceMode2D.Force   : �δ�.
            //  => ForceMode2D.Impulse : �������� ���� ���Ѵ�.
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);             // ������Ʈ�� �ӵ��� x���� �״�� y���� 0���� ����.
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);    // �������� ���� ���Ѵ�.            
            anim.SetTrigger("onJump");
            jumpCount -= 1;
            AudioManager.Instance.PlaySE("jump");
        }
    }


    public void OnThrow(Transform targetPivot)
    {
        // �� ��ġ - ������ ��ġ = ���濡�� �� ��ġ�� ���� ����.
        Vector3 direction =  transform.position - targetPivot.position;
        direction.Normalize();          // ���� ���� ����ȭ.
        direction.y = 1;                // y�� ���� ����.

        
        rigid.velocity = Vector2.zero;                                      // ������ �ӵ��� 0���� �����.
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse);        // direction �������� throwPower��ŭ (�ѹ���)���� ���϶�.
        isLockControl = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
  
}
