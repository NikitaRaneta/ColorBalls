using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public BoxCollider2D leftBorder;
    public BoxCollider2D rightBorder;
    public BoxCollider2D bottom;
    public float colliderSize = 1f;

    private int _score = 0;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreLabel.text = _score.ToString();
        }

    }
    public Text scoreLabel;

    public GameOverPopup gameOver;

    void Start()
    {
        // Расстановка позиций коллайдеров
        Camera mainCamera = Camera.main;
        rightBorder.transform.position = new Vector3(mainCamera.orthographicSize * mainCamera.aspect + colliderSize * 0.5f, 0, 0);
        rightBorder.size = new Vector2(colliderSize, mainCamera.orthographicSize * 2);

        leftBorder.transform.position = new Vector3(-rightBorder.transform.position.x, 0, 0);
        leftBorder.size = rightBorder.size;

        bottom.transform.position = new Vector3(0, -colliderSize * 0.5f - mainCamera.orthographicSize, 0);
        bottom.size = new Vector2(mainCamera.orthographicSize * mainCamera.aspect * 2, colliderSize);

        score = 0;
    }

    public void GameOver()
    {
        gameOver.ShowPopup(score);
    }


}
