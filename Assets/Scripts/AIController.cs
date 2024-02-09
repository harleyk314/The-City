using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIController : MonoBehaviour
{

	private float speed;
	private Text scoreText;
	private Text win;

	private int player_direction;
	private float turnSpeed;
	private float turnMultiplier;

	private Vector3 jump;
	private float jumpForce = 320;
	private bool isGrounded;
	private int speedMultiplier = 1;
	private Vector3 offsetY;

	private Rigidbody rb;
	private int score = 0;
	private string AIDirection = "null";
	private string turnDirection = "null";
	private bool isJump = false;

	private int targetX = 1100;
	private int targetZ = 1100;
	private int targetDirection = 0;
	private float moveVertical;

	void Start()
	{
		//The speed of movement
		speedMultiplier = 1;
		speed = 120;
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

		//Look elsewhere when we've reached the target destination
		if (Mathf.Abs(transform.position.x - targetX) < 200 && Mathf.Abs(transform.position.z - targetZ) < 200)
        {
			//AIDirection = "null";
			//isJump = false;

			targetX = Random.Range(0, 24) * 200 + 100;
			targetZ = Random.Range(0, 24) * 200 + 100;

			AIDirection = "forward";
		} else
        {
			AIDirection = "forward";


		}

		void OnCollisionEnter(Collision collision)
		{
			//Jump upon any collision
			if (collision.gameObject.name == "AIController")
			{
				isJump = true;
			}
			else
			{
				isJump = false;
			}
		}

		//This seems to control forward movement

		if (AIDirection.Equals("forward")){
			moveVertical = 1;
        } else
        {
			moveVertical = 0;
		}



		transform.position += new Vector3(Mathf.Sin(player_direction * Mathf.PI / 180) * moveVertical, 0.0f, Mathf.Cos(player_direction * Mathf.PI / 180) * moveVertical).normalized * speed * speedMultiplier * Time.deltaTime;

		//Let's figure out what direction we want to travel in
		if (transform.position.z - targetZ > 30)
        {
			//We should go south
			targetDirection = 180;
        } else if (transform.position.z - targetZ < -30)
        {
			//we should go north
			targetDirection = 0;
		} else if (transform.position.x - targetX > 30)
		{
			//We should go west
			targetDirection = -90;
		}
		else if (transform.position.x - targetX < -30)
		{
			//we should go east
			targetDirection = 90;
		}

		if ((player_direction % 360) > (targetDirection % 360) && (player_direction % 360) < ((targetDirection % 360) + 180))
		{
			turnDirection = "left";
		}
		else if ((player_direction % 360) < (targetDirection % 360) && (player_direction % 360) > ((targetDirection % 360) - 180))
		{
			turnDirection = "right";
		}
		else if ((player_direction % 360) == (targetDirection % 360))
		{
			turnDirection = "null";
		} 
		else
		{
			turnDirection = "right";
		}

		if (turnDirection.Equals("right") && isGrounded)
		{
			player_direction += (int) (turnSpeed * turnMultiplier * Time.deltaTime);
		}
		else if (turnDirection.Equals("left") && isGrounded)
		{
			player_direction -= (int) (turnSpeed * turnMultiplier * Time.deltaTime);
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


		if (isJump && isGrounded)
		{
			rb.velocity = new Vector3(0, 15, 0);
			isGrounded = false;
		}

		//If out of bounds, reset.
		if (transform.position.y < -20 || transform.position.x < -1000 || transform.position.x > 10000 || transform.position.z < -1000 || transform.position.y > 10000)
		{
			transform.position = new Vector3(0, 5, 0);
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			rb.velocity = new Vector3(0, 0, 0);
		}

		//rb.AddForce (movement * speed);
	}

	//Seems to be related to scoring, so ignore this for now.
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Diamond"))
		{
			this.gameObject.GetComponent<AudioSource>().Play();
			other.gameObject.SetActive(false);
			score += 1;
			if (score == 9)
			{
				scoreText.text = "Good job!";
				win.enabled = true;
			}
			else
			{
				scoreText.text = "Score: " + score.ToString();
			}

		}
	}



}
