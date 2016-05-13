using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	public float speed;
	public bool onLadder;
	public Rigidbody2D rb;
	public float jumpForce;
	Vector3 move;
	bool onGround;
	public GameObject red;

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

<<<<<<< Updated upstream
	// Update is called once per frame
	void Update () 
	{
		
		if (rb.velocity.y > 0) 
		{
=======
	void FixedUpdate()
	{ 
		
			
	}

	// Update is called once per frame
	void Update () 
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, 0.57f);
		Debug.DrawRay (transform.position, Vector2.down * 0.57f);
		if (hit.collider!=null) {
			print ("on ground");
			onGround = true;
		} else {
>>>>>>> Stashed changes
			onGround = false;
			print ("not on ground");
		}

<<<<<<< Updated upstream
		if (rb.velocity.y == 0) 
		{
			onGround = true;
		}

		move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

		transform.position += move * speed * Time.deltaTime;

=======
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
		
>>>>>>> Stashed changes
		// Jump if on ground
		if (Input.GetKeyDown (KeyCode.Space)&&(onGround))
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
<<<<<<< Updated upstream

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		
	}
=======
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "NoJumpArea") {
			onNoJumpArea = true;
			canGoUp = true;
			canGoDown = true;
			rb.velocity = new Vector3 (0, 0, 0);
			rb.gravityScale = 0;
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

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "NoJumpArea") {
			onNoJumpArea = false;
			rb.gravityScale = 1;
			canGoUp = false;
			canGoDown = false;
		}
>>>>>>> Stashed changes


		
}
