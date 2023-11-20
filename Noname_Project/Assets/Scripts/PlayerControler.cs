using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("이동")]
    [SerializeField] public Vector2 inputVec;
    [SerializeField] public float Maxspeed;

    [Header("점프")]
    [SerializeField] public float jumpTime;
    [SerializeField] public int JumpPower;
    [SerializeField] public float fallMultiplier;
    [SerializeField] public float jumpMultiplier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;
    bool isJump;
    float jumpCounter;

    [Header("공격")]
    [SerializeField] public float coolTime = 0.5f;
    [SerializeField] public float curTime;
    [SerializeField] public int damage;
    public Transform pos;
    public Transform pos2;
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

        Jump();
        Attack();        
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
        Gizmos.DrawWireCube(pos2.position, boxSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }

    void Jump()
    {
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

            if (j_time > 0.5f)   // 점프 시간의 절반이 지나면 상승속도 줄어듬(유사관성)
            {
                currentJump = jumpMultiplier * (1 - j_time);
            }

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)  //제일 약한 점프
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (rb.velocity.y < 0)  // 떨어질때 속도 증가
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
            anim.SetBool("Jump", false);
        }
    }

    void Attack()
    {
        //Attack
        if (curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                gameObject.layer = 9;
                if (!spriter.flipX)
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                }

                else
                {
                    Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize, 0);
                    foreach (Collider2D collider in collider2Ds)
                    {
                        if (collider.tag == "Enemy")
                        {
                            collider.GetComponent<Enemy>().TakeDamage(damage);
                        }
                    }
                }

                Invoke("OffDamage", 1);
                anim.SetTrigger("Attack");
                curTime = coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 9;   //PlayerDamaged Layer

        spriter.color = new Color(1, 1, 1, 0.4f);    //반투명

        int knockback = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(knockback,1) * 5, ForceMode2D.Impulse);

        anim.SetTrigger("Hit");

        StartCoroutine("OffDamage");
    }

    IEnumerator OffDamage()
    {
        yield return new WaitForSeconds(2f);

        gameObject.layer = 8;   //Player Layer
        spriter.color = new Color(1, 1, 1, 1);    //원래 색으로
    }
}
