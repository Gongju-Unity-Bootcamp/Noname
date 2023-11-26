using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("블럭 이동설정")]
    [SerializeField] public float moveX;    // x축 이동
    [SerializeField] public float moveY;    // y축 이동
    [SerializeField] public float times;    // 이동 시간
    [SerializeField] public float wait;     // 대기 시간
    public bool isMoveWhenOn = false;
    
    public bool isCanMove = true;   //움직임 여부
    float perDx;    // 프레임당 이동 속도
    float perDy;    // 프레임당 이동 속도
    Vector3 defPos;         // 초기위치
    bool isReverse = false; // 반전여부

    void Start()
    {
        defPos = transform.position;
        float timestep = Time.fixedDeltaTime;
        perDx = moveX / (1.0f /  timestep * times);
        perDy = moveY / (1.0f /  timestep * times);

        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    void FixedUpdate()
    {
        if(isCanMove)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;
            if(isReverse)
            {
                if((perDx >= 0.0f && x <= defPos.x) || (perDx < 0.0f && x >= defPos.x))
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y <= defPos.y) || (perDy < 0.0f && y >= defPos.y))
                {
                    endY = true;
                }

                transform.Translate(new Vector3(-perDx, -perDy, defPos.z));
            }
            else
            {
                if ((perDx >= 0.0f && x >= defPos.x + moveX) || (perDx < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true;
                }
                if ((perDy >= 0.0f && y >= defPos.y + moveY) || (perDy < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true;
                }

                transform.Translate(new Vector3(perDx, perDy, defPos.z));
            }

            if (endX && endY) 
            {
                if (isReverse)
                {
                    transform.position = defPos;    //위치 어긋남 방지, 원위치
                }
                isReverse = !isReverse;
                isCanMove = false;
                if(isMoveWhenOn == false)
                {
                    Invoke("Move", wait);
                }

            }
        }
    }
    
    public void Move()
    {
        isCanMove = true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

}
