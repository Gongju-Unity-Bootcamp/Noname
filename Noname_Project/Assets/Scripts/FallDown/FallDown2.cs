using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown2 : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerControler player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameManager.health > 1)
            {
                player.VelocityZero();   
                collision.transform.position = new Vector3(148, -7, -1);
            }

            gameManager.HealthDown();  
        }
    }
}
