using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpForce = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("We hit " + collision.gameObject.name);
    }
}
