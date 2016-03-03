using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

    public GameController controller;
	void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Time.timeScale = 0;
            controller.GameOver();
        }
    }

}
