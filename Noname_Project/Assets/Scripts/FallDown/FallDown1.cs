using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown1 : MonoBehaviour
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
                collision.transform.position = new Vector3(94, -6, -1);
            }

            gameManager.HealthDown();
        }
    }
}
