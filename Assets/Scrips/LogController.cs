using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy the log if it goes off-screen
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
