using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverTextManager : MonoBehaviour {
    public GameObject gameOverTextP1;
    public GameObject gameOverTextP2;
    public GameObject gameOverTextP3;
    public GameObject gameOverTextP4;
    public float GameLength;
    public static bool gameOver = false;
    private bool wimpsWin = false;
    // Use this for initialization
    void Start () {
        GameLength = GameLength * 60;
	}
	
	// Update is called once per frame
	void Update () {
        GameLength -= Time.smoothDeltaTime;
        if (GameLength <= 0)
        {
            gameOver = true;
            wimpsWin = true;
        }
        if (OrbCount.wimpsExited == 3 || wimpsWin == true) {
            gameOverTextP1.GetComponent<Text>().enabled = true;
            gameOverTextP2.GetComponent<Text>().enabled = true;
            gameOverTextP3.GetComponent<Text>().enabled = true;
            gameOverTextP4.GetComponent<Text>().enabled = true;
            gameOver = true;
        }

        if (Controls.wimpsDowned == 3) {
            gameOverTextP1.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP2.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP3.GetComponent<Text>().text = "Murderer Wins";
            gameOverTextP4.GetComponent<Text>().text = "Murderer Wins";

            gameOverTextP1.GetComponent<Text>().enabled = true;
            gameOverTextP2.GetComponent<Text>().enabled = true;
            gameOverTextP3.GetComponent<Text>().enabled = true;
            gameOverTextP4.GetComponent<Text>().enabled = true;
            gameOver = true;
        }
	}
}
