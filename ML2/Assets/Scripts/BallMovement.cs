using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = .25f;
    [SerializeField] private Text leftScore;
    [SerializeField] private Text rightScore;

    private int hitCounter;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("startBall", 2f);//starts the round
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter)) ;
    }

    private void startBall() //method to start the round
    {
        rb.velocity = new Vector2(-1,0) * (initialSpeed + speedIncrease * hitCounter) ;
    }

    private void resetBall() //method to reset after a round
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;

        Invoke("startBall", 2f);
    }

    public void PlayerBounce(Transform obj)//gives direction to ball on hit
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = obj.position;

        float xDir;
        float yDir;
        if(transform.position.x > 0) //left and right direction
        {
            xDir = -1;

        }
        else
        {
            xDir = 1;

        }

        yDir = (ballPos.y - playerPos.y) / obj.GetComponent<Collider2D>().bounds.size.y;//this sets the direction up and down

        if (yDir == 0) //if somehow y dir ends up as 0
        {
            yDir = .25f;
        }

        rb.velocity = new Vector2(xDir, yDir) * (initialSpeed + speedIncrease * hitCounter); //update ball rigidbody
    }

    private void OnCollisionEnter2D(Collision2D collision)//collision with paddle
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerBounce(collision.transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//collision with the goals set as triggers
    {
        if(transform.position.x > 0)
        {
            resetBall();
            leftScore.text = (int.Parse(leftScore.text)+1).ToString();
        }
        else if (transform.position.x < 0)
        {
            resetBall();
            rightScore.text = (int.Parse(rightScore.text) + 1).ToString();
        }
    }


}
