using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public int nextMove;    // 1:right -1:left

    public bool islive;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriter;
    CapsuleCollider2D collider;


    PlayerControler player;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        player = GetComponent<PlayerControler>();

        MoveRandom();
    }

    private void Update()
    {
        CheckHp(hp);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(nextMove, rb.velocity.y);

        Vector2 frontVec = new Vector2(rb.position.x + nextMove * 0.5f, rb.position.y);
        Debug.DrawLine(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
            Turn();         
    }

    void MoveRandom()
    {
        nextMove = Random.Range(-1, 2);

        //anim.SetInteger("WalkSpeed", nextMove);
        if(nextMove != 0)
        {
            spriter.flipX = nextMove < 0;
        }

        float nextTime = Random.Range(2f, 7f);
        Invoke("MoveRandom", nextTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriter.flipX = nextMove < 0;

        CancelInvoke();
        Invoke("MoveRandom", 3);
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
        EmOnDamaged();
    }

    void CheckHp(int heath)
    {
        if(heath == 0)
        {
            collider.enabled = false;
            spriter.color = new Color(1, 1, 1, 0.4f);
            StartCoroutine("Dead");
        }
    }

    void EmOnDamaged()
    {
        gameObject.layer = 10;   //EnemyDamaged Layer
        spriter.color = new Color(1, 1, 1, 0.4f);    //반투명
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        StartCoroutine(EmOffDamage());
    }
    IEnumerator EmOffDamage()
    {
        yield return new WaitForSeconds(2f);

        gameObject.layer = 7;   //Enemy Layer
        spriter.color = new Color(1, 1, 1, 1);    //원래 색으로
    }


    IEnumerator Dead()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
