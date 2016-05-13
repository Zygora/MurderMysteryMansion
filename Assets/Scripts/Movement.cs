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

		move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

		transform.position += move * speed * Time.deltaTime;

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

	void OnTriggerEnter2D(Collider2D other)
	{
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		
	}


		
}
