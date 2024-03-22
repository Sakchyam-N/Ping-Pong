using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 7f;
    [SerializeField] private float increaseSpeed = 0.25f;

    private Rigidbody2D rigidBody;
    private int hitCount;

    [SerializeField] private ScoreController scoreController;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, initialSpeed + (increaseSpeed * hitCount));
    }

    void StartBall()
    {
        rigidBody.velocity = Vector2.left * (initialSpeed + increaseSpeed *hitCount);
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        hitCount = 0;
        Invoke("StartBall",2f);
    }

    private void BallBounce(Transform myObject)
    {
        hitCount++;
        Vector2 ballPosition = transform.position;
        Vector2 paddlePosition = myObject.position;

        float xDirection, yDirection;

        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        yDirection = (ballPosition.y - paddlePosition.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rigidBody.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (increaseSpeed * hitCount));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            BallBounce(collision.transform);
        }
        else if (collision.gameObject.CompareTag("WallAi"))
        {
            scoreController.IncrementScore();
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            scoreController.IncrementAiScore();
            ResetBall();
        }
    }


}

