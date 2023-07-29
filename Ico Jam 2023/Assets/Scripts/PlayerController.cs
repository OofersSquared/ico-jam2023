using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(1, 5)] public float speed = 5f;
    [Range(1, 10)] public float jumpForce = 10f;
    [Range(6, 10)] public float sprintSpeed = 10f;

    [SerializeField][Range(8, 15)] private float gravity = 9.81f;

    private bool _isJumping = false;
    private bool _gravityFlipped = false;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Is he sprinting or not?
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        if (Input.GetKeyDown(KeyCode.S) && _isJumping) 
        {
            _rb.velocity.Set(0, 0);
            _rb.AddForce(new Vector2(0f, -jumpForce *2), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.velocity = new Vector2(-currentSpeed, _rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = new Vector2(currentSpeed, _rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.W) && !_isJumping)
        {
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _isJumping = true;
        }

        // E key for gravity flip
        if (Input.GetKeyDown(KeyCode.E))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        if (_gravityFlipped)
        {
            // Normal Gravity
            Physics2D.gravity = new Vector2(0, -gravity);
            _gravityFlipped = false;
        }
        else
        {
            //Upside Down Gravity
            Physics2D.gravity = new Vector2(0, gravity);
            _gravityFlipped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform")) {
            _isJumping = false;
        }
    }
}