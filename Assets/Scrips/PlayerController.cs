using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1f;
    public LayerMask obstacleLayer;
    private bool isOnLog = false;
    private GameObject currentLog;

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
            move = Vector3.up * moveDistance;
        else if (Input.GetKeyDown(KeyCode.S))
            move = Vector3.down * moveDistance;
        else if (Input.GetKeyDown(KeyCode.A))
            move = Vector3.left * moveDistance;
        else if (Input.GetKeyDown(KeyCode.D))
            move = Vector3.right * moveDistance;

        if (move != Vector3.zero)
            AttemptMove(move);
    }

    void AttemptMove(Vector3 move)
    {
        Vector3 targetPos = transform.position + move;

        if (!Physics2D.OverlapCircle(targetPos, 0.1f, obstacleLayer))
        {
            transform.position = targetPos;
            if (!isOnLog)
                CheckWater();
            else
                CheckOffScreen();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Die();
        }
        else if (collision.gameObject.CompareTag("Log"))
        {
            isOnLog = true;
            currentLog = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Log"))
        {
            isOnLog = false;
            currentLog = null;
        }
    }

    void CheckWater()
    {
        // Check if the player is on water without a log
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Water")))
        {
            Die();
        }
    }

    void CheckOffScreen()
    {
        // Check if the player is off-screen
        if (transform.position.x < -5 || transform.position.x > 5)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death (restart level, etc.)
        Debug.Log("Player Died");
        // For example, reset the level
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
