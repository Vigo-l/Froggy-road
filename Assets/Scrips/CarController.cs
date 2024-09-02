using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy the car if it goes off-screen
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
