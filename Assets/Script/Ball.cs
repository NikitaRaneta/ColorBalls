using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public enum MovingType
    {
        Straight,
        Sine
    }

    MovingType moveType;
    Rigidbody2D thisrigidbody;

    float verticalSpeed;
    float horizontalSpeed;
    float leftSlowDown;
    float rightSlowDown;
    float leftBorder;
    float rightBorder;
    float acceleration;
    bool moveLeft;

    Vector2 currentSpeed;

    SpawnController.BallsPool pool;

    void Awake()
    {
        thisrigidbody = GetComponent<Rigidbody2D>();
    }

    public void InitStraight(SpawnController.BallsPool pool, float verticalSpeed)
    {
        this.pool = pool;
        gameObject.SetActive(true);
        this.verticalSpeed = verticalSpeed;
        thisrigidbody.velocity = new Vector2(0, verticalSpeed);
        moveType = MovingType.Straight;
    }

    public void InitSine(SpawnController.BallsPool pool, float verticalSpeed, float horizontalSpeed, float leftBorder, float rightBorder)
    {
        this.pool = pool;
        gameObject.SetActive(true);
        this.verticalSpeed = verticalSpeed;
        this.horizontalSpeed = horizontalSpeed;
        this.leftBorder = leftBorder;
        this.rightBorder = rightBorder;
        if (leftBorder > rightBorder)
        {
            float t = leftBorder;
            leftBorder = rightBorder;
            rightBorder = t;
        }

        float width = rightBorder - leftBorder;
        leftSlowDown = leftBorder + width * 0.2f;
        rightSlowDown = rightBorder - width * 0.2f;

        acceleration = horizontalSpeed * horizontalSpeed / (2 * (leftSlowDown - leftBorder));
        moveLeft = transform.position.x > ((leftBorder + rightBorder) * 0.5f);
        currentSpeed = new Vector2(0, verticalSpeed);
        thisrigidbody.velocity = currentSpeed;
        moveType = MovingType.Sine;
    }

    public void DestroyBall()
    {
        pool.ReturnItem(this);
        gameObject.SetActive(false);
    }


	
	// Update is called once per frame
	void FixedUpdate () {
        if (moveType == MovingType.Sine)
        {
            print(leftBorder + " " + rightBorder + " " + transform.position.x + " " + " " + horizontalSpeed + " "+ moveLeft);
            if (moveLeft)
            {
                if (transform.position.x < leftSlowDown)
                {
                    currentSpeed.x += acceleration * Time.fixedDeltaTime;
                    if (currentSpeed.x >= 0)
                    {
                        moveLeft = false;
                    }
                }
                else if (currentSpeed.x > -horizontalSpeed)
                {
                    currentSpeed.x -= acceleration * Time.fixedDeltaTime;
                }
                else
                {
                    currentSpeed.x = -horizontalSpeed;
                }
            }
            else
            {
                if (transform.position.x > rightSlowDown)
                {
                    currentSpeed.x -= acceleration * Time.fixedDeltaTime;
                    if (currentSpeed.x <= 0)
                    {
                        moveLeft = true;
                    }
                }
                else if (currentSpeed.x < horizontalSpeed)
                {
                    currentSpeed.x += acceleration * Time.fixedDeltaTime;
                }
                else
                {
                    currentSpeed.x = horizontalSpeed;
                }
            }
            thisrigidbody.velocity = currentSpeed;
            print(currentSpeed);
        }
    }
}
