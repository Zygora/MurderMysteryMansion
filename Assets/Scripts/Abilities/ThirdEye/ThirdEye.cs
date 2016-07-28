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

    void Start() {
        arrow = Resources.Load("ThirdEyeArrow", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            if (LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
            LeftRoomPos == Controls.currentPlayer4Pos)
            {
                if (leftArrow == 0)
                {
                    CreateArrowLeft();
                }
            }

            else {
                leftArrow = 0;
                Destroy(GameObject.Find("LeftArrow"));
            }

            if (RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("RightArrow"));
            }

            if (UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("UpArrow"));
            }

            if ( BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("DownArrow"));
            }
        }

        if (gameObject.tag == "Player2")
        {
            if (LeftRoomPos == Controls.currentPlayer1Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("LeftArrow"));
            }

            if (RightRoomPos == Controls.currentPlayer1Pos || RightRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("RightArrow"));
            }

            if (UpRoomPos == Controls.currentPlayer1Pos || UpRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("UpArrow"));
            }

            if (BottomRoomPos == Controls.currentPlayer1Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
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
                Destroy(GameObject.Find("DownArrow"));
            }
        }

        if (gameObject.tag == "Player3")
        {
            if (LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer1Pos ||
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
                Destroy(GameObject.Find("LeftArrow"));
            }

            if (RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer1Pos ||
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
                Destroy(GameObject.Find("RightArrow"));
            }

            if (UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer1Pos ||
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
                Destroy(GameObject.Find("UpArrow"));
            }

            if (BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer1Pos ||
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
                Destroy(GameObject.Find("DownArrow"));
            }
        }

        if (gameObject.tag == "Player4")
        {
            if (LeftRoomPos == Controls.currentPlayer2Pos || LeftRoomPos == Controls.currentPlayer3Pos ||
            LeftRoomPos == Controls.currentPlayer1Pos)
            {
                if (leftArrow == 0)
                {
                    CreateArrowLeft();
                }
            }

            else
            {
                leftArrow = 0;
                Destroy(GameObject.Find("LeftArrow"));
            }

            if (RightRoomPos == Controls.currentPlayer2Pos || RightRoomPos == Controls.currentPlayer3Pos ||
            RightRoomPos == Controls.currentPlayer1Pos)
            {
                if (rightArrow == 0)
                {
                    CreateArrowRight();
                }
            }

            else
            {
                rightArrow = 0;
                Destroy(GameObject.Find("RightArrow"));
            }

            if (UpRoomPos == Controls.currentPlayer2Pos || UpRoomPos == Controls.currentPlayer3Pos ||
            UpRoomPos == Controls.currentPlayer1Pos)
            {
                if (upArrow == 0)
                {
                    CreateArrowUp();
                }
            }

            else
            {
                upArrow = 0;
                Destroy(GameObject.Find("UpArrow"));
            }

            if (BottomRoomPos == Controls.currentPlayer2Pos || BottomRoomPos == Controls.currentPlayer3Pos ||
            BottomRoomPos == Controls.currentPlayer1Pos)
            {
                if (downArrow == 0)
                {
                    CreateArrowDown();
                }
            }

            else
            {
                downArrow = 0;
                Destroy(GameObject.Find("DownArrow"));
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
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x-20, this.transform.position.y + 60, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 0);
        arrow1.name = "LeftArrow";
        leftArrow += 1;
    }

    void CreateArrowRight()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x + 20, this.transform.position.y + 60, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 180);
        arrow1.name = "RightArrow";
        rightArrow += 1;
    }

    void CreateArrowDown()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 50, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 90);
        arrow1.name = "DownArrow";
        downArrow += 1;
    }

    void CreateArrowUp()
    {
        GameObject arrow1 = Instantiate(arrow);
        arrow1.transform.parent = this.transform;
        arrow1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 80, this.transform.position.z);
        arrow1.transform.eulerAngles = new Vector3(0, 0, 270);
        arrow1.name = "UpArrow";
        upArrow += 1;
    }
}
