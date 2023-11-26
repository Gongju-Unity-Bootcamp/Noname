using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("�� �̵�����")]
    [SerializeField] public float moveX;    // x�� �̵�
    [SerializeField] public float moveY;    // y�� �̵�
    [SerializeField] public float times;    // �̵� �ð�
    [SerializeField] public float wait;     // ��� �ð�
    public bool isMoveWhenOn = false;
    
    public bool isCanMove = true;   //������ ����
    float perDx;    // �����Ӵ� �̵� �ӵ�
    float perDy;    // �����Ӵ� �̵� �ӵ�
    Vector3 defPos;         // �ʱ���ġ
    bool isReverse = false; // ��������

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
                    transform.position = defPos;    //��ġ ��߳� ����, ����ġ
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
