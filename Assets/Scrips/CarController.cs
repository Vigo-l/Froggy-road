using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2f;
    private PlayerController _playerController;
    [SerializeField] private GameObject _playerObject;

    private void Start()
    {
        _playerController = _playerObject.GetComponent<PlayerController>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy the car if it goes off-screen
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.Die();
        }
    }
}
