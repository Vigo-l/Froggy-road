using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1f;
    public LayerMask obstacleLayer;
    public LayerMask coinLayer;
    public int score = 0;
    private bool isOnLog = false;
    private GameObject currentLog;

    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
            move = Vector3.up * moveDistance;
        //else if (Input.GetKeyDown(KeyCode.S))
        // move = Vector3.down * moveDistance;
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
            score += 1;
            Debug.Log("Score: " + score);
            if (!isOnLog)
                CheckWater();
            else
                CheckOffScreen();

        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("Collided with Car");
            Die();
        }
        else if (collision.gameObject.CompareTag("Log"))
        {
            Debug.Log("Collided with Log");
            isOnLog = true;
            currentLog = collision.gameObject;
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collided with Coin");
            score += 10;
            Debug.Log("Score: " + score);
            Destroy(collision.gameObject);
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
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Water")))
        {
            Die();
        }
    }

    void CheckOffScreen()
    {
        if (transform.position.x < -5 || transform.position.x > 5)
        {
            Die();
        }
    }


    public void Die()
    {
        Debug.Log("Player Died");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
