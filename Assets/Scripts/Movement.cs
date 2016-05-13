using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public float speed;
	public Rigidbody2D rb;
	public float jumpForce;
	Vector3 move;
	bool onGround;
	public GameObject red;
	bool onLadder;
	public bool canGoUp;
	public bool canGoDown;
	public bool onNoJumpArea;
	public GameObject topPlatform;
	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}


	// Update is called once per frame
	void Update () 
	{
		if (rb.velocity.y > 0) 
		{
			onGround = false;
		}

		if (rb.velocity.y == 0) 
		{
			onGround = true;
		}
		if ((onLadder)&&(canGoUp)&&((Input.GetAxis("Vertical")>0))) {
			move = new Vector3 (0, Input.GetAxis ("Vertical"), 0);
			transform.position += move * speed * Time.deltaTime;
		}
		if ((onLadder)&&(canGoDown)&&((Input.GetAxis("Vertical")<0))) {
			move = new Vector3 (0, Input.GetAxis ("Vertical"), 0);
			transform.position += move * speed * Time.deltaTime;
		}
			move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			transform.position += move * speed * Time.deltaTime;
		
		// Jump if on ground
		if ((Input.GetKeyDown (KeyCode.Space)&&(onGround))&&(!onNoJumpArea))
		{
			rb.AddForce (Vector2.up*jumpForce,ForceMode2D.Impulse);
		}

		// Special Ability
		if (Input.GetKeyDown (KeyCode.E))
		{
			if (red.activeSelf) {
				red.SetActive (false);
				return;
			}
			if (!red.activeSelf) {
				red.SetActive (true);
				return;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "NoJumpArea") {
			onNoJumpArea = true;
		}

		if (other.tag == "Ladder") {
			
			onLadder = true;
			rb.gravityScale = 0;
			if (other.gameObject.transform.position.y < gameObject.transform.position.y)
				canGoUp = false;
		}
		if (other.gameObject.tag == "BottomLadder") {
			canGoDown = false;
			canGoUp = true;
		}
		if (other.gameObject.tag == "TopLadder") {
			canGoDown = true;
			Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D>(),
				topPlatform.GetComponent<Collider2D>(),true);
			
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "NoJumpArea") {
			onNoJumpArea = false;
		}

		if (other.tag == "Ladder") {
			onLadder = false;
			rb.gravityScale = 1;
			canGoUp = false;

		}
		if (other.gameObject.tag == "BottomLadder") {
			canGoDown = true;
		}
		if (other.gameObject.tag == "TopLadder") {
			Physics2D.IgnoreCollision (gameObject.GetComponent<Collider2D>(),
				topPlatform.GetComponent<Collider2D>(),false);
			canGoUp = true;
		}
	}		
}
