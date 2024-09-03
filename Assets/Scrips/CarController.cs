using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2f;
    private PlayerController _playerController;
    [SerializeField] private GameObject _playerObject;

    private void Awake()
    {
        _playerObject = GameObject.Find("Frog");
        _playerController = _playerObject.GetComponent<PlayerController>();
        Debug.Log("car spawned" + _playerController);
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

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.Die();
        }
    }
}
