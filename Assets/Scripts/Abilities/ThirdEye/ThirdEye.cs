using UnityEngine;
using System.Collections;

public class ThirdEye : MonoBehaviour
{
    public Vector2 currentRoomPos;
    public Vector2 LeftRoomPos;
    public Vector2 RightRoomPos;
    public Vector2 UpRoomPos;
    public Vector2 BottomRoomPos;
    public GameObject arrow;
    public float thirdEyeCooldown = 1;
    private float timepassed;
    public Vector3 arrowSpawn;
    private int leftArrow = 0;
    private int rightArrow = 0;
    private int upArrow = 0;
    private int downArrow = 0;
    private GameObject canvas;
    private GameObject[] leftArrows;
    private GameObject[] rightArrows;
    private GameObject[] upArrows;
    private GameObject[] downArrows;

    void Start() {
        arrow = Resources.Load("ThirdEyeArrow", typeof(GameObject)) as GameObject;
        if (gameObject.tag == "Player1") {
            canvas = GameObject.Find("Canvas P1");
        }
        if (gameObject.tag == "Player2")
        {
            canvas = GameObject.Find("Canvas P2");
        }
        if (gameObject.tag == "Player3")
        {
            canvas = GameObject.Find("Canvas P3");
        }
        if (gameObject.tag == "Player4")
        {
            canvas = GameObject.Find("Canvas P4");
        }
        leftArrows = GameObject.FindGameObjectsWithTag("LeftArrow");
        rightArrows = GameObject.FindGameObjectsWithTag("RightArrow");
        upArrows = GameObject.FindGameObjectsWithTag("UpArrow");
        downArrows = GameObject.FindGameObjectsWithTag("DownArrow");
    }

    void Update()
    {

        if (LeftRoomPos == Controls.currentPlayer1Pos || LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
        LeftRoomPos == Controls.currentPlayer4Pos)
        {
            if (leftArrow == 0)
            {
                CreateArrowLeft();
            }
        }

        else
        {
           leftArrow = 0;
            for(int x = 0; x < leftArrows.Length; x++)
            {
                leftArrows[x].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (RightRoomPos == Controls.currentPlayer1Pos || RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer3Pos ||
        RightRoomPos == Controls.currentPlayer4Pos)
        {
            if (rightArrow == 0)
            {
                CreateArrowRight();
            }
        }

        else
        {
            rightArrow = 0;
            for (int x = 0; x < rightArrows.Length; x++)
            {
                rightArrows[x].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (UpRoomPos == Controls.currentPlayer1Pos || UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer3Pos ||
        UpRoomPos == Controls.currentPlayer4Pos)
        {
            if (upArrow == 0)
            {
                CreateArrowUp();
            }
        }

        else
        {
            upArrow = 0;
            for (int x = 0; x < upArrows.Length; x++)
            {
                upArrows[x].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if (BottomRoomPos == Controls.currentPlayer1Pos || BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
        BottomRoomPos == Controls.currentPlayer4Pos)
        {
            if (downArrow == 0)
            {
                CreateArrowDown();
            }
        }

        else
        {
            downArrow = 0;
            for (int x = 0; x < downArrows.Length; x++)
            {
                downArrows[x].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Room" || other.gameObject.tag == "MurdererStart") {
            currentRoomPos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            LeftRoomPos = currentRoomPos - new Vector2(1, 0);
            RightRoomPos = currentRoomPos + new Vector2(1, 0);
            UpRoomPos = currentRoomPos + new Vector2(0, 1);
            BottomRoomPos = currentRoomPos - new Vector2(0, 1);
        }
    }

    void CreateArrowLeft() {
        canvas.gameObject.transform.FindChild("LeftArrow").GetComponent<SpriteRenderer>().enabled = true;
        leftArrow += 1;
    }

    void CreateArrowRight()
    {
        canvas.gameObject.transform.FindChild("RightArrow").GetComponent<SpriteRenderer>().enabled = true;
        rightArrow += 1;
    }

    void CreateArrowDown()
    {
        canvas.gameObject.transform.FindChild("DownArrow").GetComponent<SpriteRenderer>().enabled = true;
        downArrow += 1;
    }

    void CreateArrowUp()
    {
        canvas.gameObject.transform.FindChild("UpArrow").GetComponent<SpriteRenderer>().enabled = true;
        upArrow += 1;
    }
}
