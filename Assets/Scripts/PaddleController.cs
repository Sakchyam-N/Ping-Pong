using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private bool isComp;
    [SerializeField] private GameObject ball;

    private Rigidbody2D rigidBody;
    private Vector2 moveInput;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isComp)
        {
            CompMove();
        }
        else
        {
            PlayerMove();
        }
    }

    private void PlayerMove()
    {
        moveInput = new Vector2(0, Input.GetAxisRaw("Vertical"));
    }

    private void CompMove()
    {
        if(ball.transform.position.y > transform.position.y+0.5f)
        {
            moveInput = new Vector2(0, 1);
        }
        else if(ball.transform.position.y < transform.position.y - 0.5f)
        {
            moveInput = new Vector2(0, -1);
        }
        else
        {
            moveInput = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = moveInput * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 collisionNormal = collision.contacts[0].normal;
            Vector2 reflectionDirection = Vector2.Reflect(collisionNormal, Vector2.right);
            reflectionDirection += collisionNormal * 0.3f;
            float bounceSpeed = 22f;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = reflectionDirection * bounceSpeed;
        }
    }
}
