using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset;         // Offset position relative to the player

    void Start()
    {
        // If the offset isn't set, calculate the initial offset based on current positions
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        void LateUpdate()
        {
            // Smoothly interpolate between the camera's current position and the target position
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }
    }
}
