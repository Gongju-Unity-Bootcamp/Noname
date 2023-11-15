using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 3;
    public int nextMove;    // 1:right -1:left

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();

        MoveRandom();
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
        HP = HP - damage; 
    }
}
