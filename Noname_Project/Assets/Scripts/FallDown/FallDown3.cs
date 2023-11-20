using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown3 : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(gameManager.health > 1)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                collision.transform.position = new Vector3(231, -2, -1);
            }

            gameManager.HealthDown();
        }
    }
}
