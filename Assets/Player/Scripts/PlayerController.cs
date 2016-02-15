using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	private Rigidbody rb;
	public bool jump = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Called before every physics calculation
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float moveUp = Input.GetAxis ("Jump");
		float sprint = Input.GetAxis ("Sprint")+1;
		if (!jump && moveUp > 0) {
			Vector3 jumpForce = new Vector3 (0.0f, moveUp, 0.0f);
			rb.AddForce (jumpForce*jumpSpeed);
			jump = true;
		}

		if (Mathf.Abs (rb.velocity.y) < 0.05f) {
			jump = false;
		}
			

		Vector3 force = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.transform.Translate (force * (speed*sprint));
	}
}
