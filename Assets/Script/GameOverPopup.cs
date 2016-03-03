using UnityEngine;
using System.Collections;

public class GameOverPopup : MonoBehaviour {

    public UnityEngine.UI.Text scoreLabel;

    public void ShowPopup(int result)
    {
        scoreLabel.text = result.ToString();
        gameObject.SetActive(true);
    }
}
