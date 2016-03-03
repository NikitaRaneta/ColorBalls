using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

    public float speed = 8;
    public GameController controller;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().MovePosition(new Vector2(0, -Camera.main.orthographicSize + GetComponent<Renderer>().bounds.extents.y));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            ++controller.score;
            coll.gameObject.GetComponent<Ball>().DestroyBall();
        }
    }
}
