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
        //reset game values
        GameLength = GameLength * 60;
        Controls.wimpsDowned = 0;
        OrbCount.wimpsExited = 0;
	}
	
	// Update is called once per frame
	void Update () {
        GameLength -= Time.smoothDeltaTime;
        //game over if time is up
        if (GameLength <= 0)
        {
            gameOver = true;
            wimpsWin = true;
        }
        //game over if all wimps exited. displays text
        if (OrbCount.wimpsExited == 3 || wimpsWin == true) {
            EnableText("  Wimps Wins");
        }

        //game over if all wimps downed. displays text
        if (Controls.wimpsDowned == 3) {
            
            EnableText("Murderer Wins");
        }
	}

    void EnableText(string gameOverText)
    {
        gameOverTextP1.GetComponent<Text>().text = gameOverText;
        gameOverTextP2.GetComponent<Text>().text = gameOverText;
        gameOverTextP3.GetComponent<Text>().text = gameOverText;
        gameOverTextP4.GetComponent<Text>().text = gameOverText;

        gameOverTextP1.GetComponent<Text>().enabled = true;
        gameOverTextP2.GetComponent<Text>().enabled = true;
        gameOverTextP3.GetComponent<Text>().enabled = true;
        gameOverTextP4.GetComponent<Text>().enabled = true;

        gameOver = true;
    }
}
