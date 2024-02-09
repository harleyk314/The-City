using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	private float speed;
	private Text scoreText;
	private Text win;

	private float player_direction;
	private float turnSpeed;
	private float turnMultiplier;

	private Vector3 jump;
	private float jumpForce = 320;
	private bool isGrounded;
	private int speedMultiplier = 1;
	private Vector3 offsetY;

	private Rigidbody rb;
	private int score = 0;
	private float moveVertical;

	void Start()
	{
		//The speed of movement
		speedMultiplier = 1;
		speed = 30;
		player_direction = 90;
		turnSpeed = 100.0f;
		turnMultiplier = 1;
		
		offsetY = new Vector3(0,25,0);

		rb = GetComponent<Rigidbody>();

		//The jumping vector
		jump = new Vector3(0.0f, 80.00f, 0.0f);

		scoreText.text = "Score: " + score.ToString();
		win.enabled = false;
	}

	void OnCollisionStay()
	{
		isGrounded = true;
	}

	//Detect collisions between the GameObjects with Colliders attached
	void OnCollisionEnter(Collision collision)
	{
		//Check for a match with the specified name on any GameObject that collides with your GameObject
		if (collision.gameObject.name == "BoostJump")
		{
			//Give the player a boost-jump
			rb.velocity = new Vector3(30, 30, 0);
			isGrounded = false;

		}

	}


	void FixedUpdate()
	{

		//Going up or down stories
		if (Input.GetKeyDown(KeyCode.O))
		{
			transform.position = transform.position + (2 * offsetY);
		}


		moveVertical = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(KeyCode.Q))
        {
			if (speedMultiplier == 1)
			{
				speedMultiplier = 4;
            } else {
				speedMultiplier = 1;
			}
        }

		transform.position += new Vector3(Mathf.Sin(player_direction * Mathf.PI / 180) * moveVertical, 0.0f, Mathf.Cos(player_direction * Mathf.PI / 180) * moveVertical).normalized * speed * speedMultiplier * Time.deltaTime;

		if (Input.GetKey(KeyCode.RightArrow) && isGrounded)
		{
			player_direction += turnSpeed * turnMultiplier * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && isGrounded)
		{
			player_direction -= turnSpeed * turnMultiplier * Time.deltaTime;
		}

		if (player_direction > 180)
		{
			player_direction -= 360;
		}
		else if (player_direction < -180)
		{
			player_direction += 360;
		}

		// Set the visual rotation of the object to the variable "direction"
		transform.rotation = Quaternion.Euler(0, player_direction, 0);



		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			rb.velocity = new Vector3(0, 15, 0);
			isGrounded = false;
		}

		//If out of bounds, reset.
		if (transform.position.y < -20)
		{
			transform.position = new Vector3(0, 5, 0);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			rb.velocity = new Vector3(0, 0, 0);
		}

		//rb.AddForce (movement * speed);
	}


}
