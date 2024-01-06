using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 200;
    public float rotationSpeed = 3;
    public Rigidbody2D rb;
    bool isReady, isDead;

    void Start()
    {
        GameManager.OnGameStarted += OnGameStarted;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnDestroy()
    {
        GameManager.OnGameStarted -= OnGameStarted;
    }

    void OnGameStarted()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isReady = true;
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        if (isReady && !isDead)
        {
            float angle;
            float rotSpeed = rotationSpeed;

            if (rb.velocity.y < -2)
            {
                angle = Mathf.Lerp(-90, 90, rb.velocity.y);
            } else
            {
                angle = 20;
                rotSpeed *= 3;
            }

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }
}
