using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("�̵�")]
    [SerializeField] public Vector2 inputVec;
    [SerializeField] public float Maxspeed;

    [Header("����")]
    [SerializeField] public float jumpTime;
    [SerializeField] public int JumpPower;
    [SerializeField] public float fallMultiplier;
    [SerializeField] public float jumpMultiplier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;
    bool isJump;
    float jumpCounter;

    [Header("����")]
    [SerializeField] public float coolTime = 0.5f;
    [SerializeField] public float curTime;
    public Transform pos;
    public Vector2 boxSize;

    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.5f, rb.velocity.y);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && isGround())
        {
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            isJump = true;
            jumpCounter = 0;
            anim.SetBool("Jump", true);
        }

        if (rb.velocity.y > 0 && isJump)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJump = false;

            float j_time = jumpCounter / jumpTime;
            float currentJump = jumpMultiplier;

            if(j_time > 0.5f)   // ���� �ð��� ������ ������ ��¼ӵ� �پ��(�������)
            {
                currentJump = jumpMultiplier * (1 - j_time);
            }

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
            jumpCounter = 0; 

            if (rb.velocity.y > 0)  //���� ���� ����
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (rb.velocity.y < 0)  // �������� �ӵ� ����
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
            anim.SetBool("Jump", false);
        }

        //Attack
        if(curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if(CompareTag("Enemy"))
                    {
                        //collider.GetComponent<Enemy>().TakeDamage('������');
                    }
                }

                anim.SetTrigger("Attack");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * inputVec.x, ForceMode2D.Impulse);

        if (rb.velocity.x > Maxspeed)
            rb.velocity = new Vector2(Maxspeed, rb.velocity.y);
        else if (rb.velocity.x < -Maxspeed)
            rb.velocity = new Vector2(-Maxspeed, rb.velocity.y);
    }

    void LateUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) <= 0.5)
            anim.SetBool("Walk", false);
        else
            anim.SetBool("Walk", true);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }    
    }

    bool isGround()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.4f, 0.15f),CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}