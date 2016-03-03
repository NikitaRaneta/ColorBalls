using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

    public GameObject BallPrefab;
    public float baseSpeed = 1;
    public float spawnTime = 5;
    float speedMultiplyer = 1;
    float timer;

    float increaseSpeedTime = 30f;
    float increaseSpeedTimer;
    public float speedIncreasedBy = 0.5f;

    Rect spawnRect;

    BallsPool pool;

    void Awake()
    {
        timer = spawnTime;
        increaseSpeedTimer = increaseSpeedTime;
        pool = new BallsPool(BallPrefab);
    }
	// Use this for initialization
	void Start () {
        spawnRect.min = transform.TransformPoint(((RectTransform)transform).rect.min);
        spawnRect.max = transform.TransformPoint(((RectTransform)transform).rect.max);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime * speedMultiplyer;
        increaseSpeedTimer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            SpawnBall();
            timer = spawnTime;
        }

        if (increaseSpeedTimer <= 0)
        {
            increaseSpeedTimer = increaseSpeedTime;
            speedMultiplyer += speedIncreasedBy;
        }

	}

    void SpawnBall()
    {
        Vector3 position;
        position.x = Random.Range(spawnRect.min.x, spawnRect.max.x);
        position.y = Random.Range(spawnRect.min.y, spawnRect.max.y);
        position.z = 0;
        Ball ball = pool.GetItem();
        ball.transform.position = position;
        float random = Random.Range(0f, 1f);
        if (random < 0.3f)
        {
            ball.InitStraight(pool, -baseSpeed * speedMultiplyer);
            print("straight");
        }
        else
        {
            float leftBorder = 0f, rightBorder = 0f;
            while (rightBorder - leftBorder < spawnRect.width * 0.2f)
            {
                leftBorder = Random.Range(spawnRect.min.x, position.x);
                rightBorder = Random.Range(position.x, spawnRect.max.x);
            }

            ball.InitSine(pool, -baseSpeed * speedMultiplyer, Random.Range(0.5f * baseSpeed * speedMultiplyer, 5f * baseSpeed * speedMultiplyer),
                leftBorder, rightBorder);
            print("sin");
        }
    }

    public class BallsPool
    {
        Stack<Ball> pool;
        GameObject prefab;

        public BallsPool(GameObject prefab)
        {
            pool = new Stack<Ball>();
            this.prefab = prefab;
        }

        void CreateNewBall()
        {
            GameObject go = Instantiate(prefab) as GameObject;
            pool.Push(go.GetComponent<Ball>());
        }

        public Ball GetItem()
        {
            if (pool.Count == 0)
            {
                CreateNewBall();
            }

            return pool.Pop();
        }

        public void ReturnItem(Ball ball)
        {
            pool.Push(ball);
        }
    }
}
