using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    Vector3 move;

    public float speed = 1.0f;
    public SpriteRenderer GateHighlight;
    public Camera Camera;
    public GameObject OptionsRoom;
    public GameObject PlayerCustomizationRoom;
    Animator Animator;
    private bool moveCameraLeft;
    private bool moveCameraRight;


    void Start()
    {
        Animator = GetComponent<Animator>();
    }
    void Update()
    {

        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        transform.position += move * speed * Time.deltaTime;

        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
        {
            Animator.SetBool("Running", false);
            Animator.SetBool("Idle", true);
        }

        if (Input.GetAxis("Horizontal") < - 0.1f)
        {
            Animator.SetBool("Running", true);
            Animator.SetBool("Idle", false);
            this.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            Animator.SetBool("Running", true);
            Animator.SetBool("Idle", false);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Animator.SetBool("Dead", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("Dead", false);
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
        }

        if (moveCameraLeft == true) {
            Camera.transform.position = Vector2.MoveTowards(Camera.transform.position, OptionsRoom.transform.position, 1 * Time.deltaTime);
        }

        if (moveCameraRight == true)
        {
            Camera.transform.position = Vector2.MoveTowards(Camera.transform.position, OptionsRoom.transform.position, 1 * Time.deltaTime);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Gate")
        {
            GateHighlight.enabled = true;
            if (Input.GetKey(KeyCode.Space)) {
                SceneManager.LoadScene(2);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "OptionsRoom") {
            moveCameraLeft = true;
        }

        if (other.gameObject.tag == "OptionsRoom")
        {
            moveCameraRight = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        GateHighlight.enabled = false;
        }
    
}
