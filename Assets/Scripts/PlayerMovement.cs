using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/* by Aubrey Isaacman and Trever Berryman
 *
 * Trever's code:
 * players can move to the seats directly next to them
 * players wobble when they move to mimic moving seats
 *
 * Aubrey's code:
 * when players are sitting next to each other, they prep to kiss
 * if players are both pressing their kiss buttons, they'll kiss
*/

public class PlayerMovement : MonoBehaviour {

	[Header("Rewired")]
    public Player rewiredPlayer; // Player object for rewired
    public int playerID; // Player ID in Rewired Settings

	[Header("Movement")]
	private float moveH; // Horizontal Movement from Rewired
	public float amountToMoveModifier; // Distance to move
	Vector3 amountToMove; // Goes with amountToMoveModifier to do movement calculation
	
	[Header("Movement")]
	public bool collided; // Shows if the players are colliding or not

	[Header("Rotation")]
	public float speed = 2f;
	public float duration;
	public float time = 3;
	public float maxRotation = 45f;

	void Start() {
		// Debug position information
		Debug.Log("Player " + playerID + " Position: " + transform.position.x);
		print("Start: " + gameObject.name);
	}

	// Sets up player ID in inspector to assign controls to the rewired Player object
	private void InitializeControls() {
        rewiredPlayer = ReInput.players.GetPlayer(playerID);
    }

	private void Awake() {
        //Initializes controls
        InitializeControls();
    }

	void GetMovementInput() {
		// assigns moveH value with the movement info from the Player object
        moveH = rewiredPlayer.GetAxisRaw("Horizontal");
    }

	void Update () {
		Move ();
	}

	// Sets collided to true if either player's box collider collides with each other
	void OnTriggerEnter2D(Collider2D other) {
		if (gameObject.name == "P1" || gameObject.name == "P2") {
			Debug.Log("Touching: " + gameObject.name); // Debug alerts for when they touch
			collided = true;	
        }
	}

	// Sets collided to false if either player's box collider exits the other player's box collider
	void OnTriggerExit2D(Collider2D other) {
		if (gameObject.name == "P1" || gameObject.name == "P2") {
			Debug.Log("Not Touching: " + gameObject.name); // Debug alerts for when they stop touching
			collided = false;	
        }
	}

	// Make the players wiggle when they move from seat to seat
	private IEnumerator Wiggle() {
		duration = 3f;
		float normalizedTime = 0;
		while(normalizedTime <= 1f) {
			transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * time));
			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
	}

	void Move()
	{
		// Assigns the amountToMoveModifier to the x (horizontal) variable
		amountToMove = new Vector3(amountToMoveModifier,0,0);
		
		// Logic for when the players have collided	
		if(collided == true) {
				// Kiss logic:
				// change player sprites to look at each other
						// worry about that later
				// then, if players both hit their kiss buttons, spawn cool shit
				if (/*p1 button*/Input.GetKey("e") && Input.GetKey("u")/*p2 button*/)
				{
					Kiss();
				}

				// Move logic:
				// Player 2 can only move left when collided is true
				if(gameObject.name == "P1" && rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
					if(transform.position.x > -8.8) {
						StartCoroutine(Wiggle()); //Start wiggle corouitine
						transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove * -1.0f, 1); // Move left
						
					}
				}
				// Player 2 can only move right when collided is true
				else if(gameObject.name == "P2" && rewiredPlayer.GetButtonDown("Horizontal")) {
					if(transform.position.x < 8.8) {
						StartCoroutine(Wiggle()); //Start wiggle corouitine
						transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove, 1); // Move right
						
					}
				}

		}
		
		// Movement logic for when players are separated
		if(collided != true) {
			// Player is at left barrier, don't move left
			if(transform.position.x < -8.8 && rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
				//Don't move
			}
			// Player is at right barrier, don't move right
			else if(transform.position.x > 8.8 && rewiredPlayer.GetButtonDown("Horizontal")) {
				//Don't move
			}
			
			// If Player's are within the barriers, move normally

			else if(transform.position.x >= -8.8 || transform.position.x <= 8.8) {
				if(rewiredPlayer.GetButtonDown("Horizontal")) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove, 1); // Move right
				}
				else if(rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove * -1.0f, 1); // Move left
				}
			}
			else if(transform.position.x >= -8.8 || transform.position.x <= 8.8){
				if(rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove * -1.0f, 1); // Move left
				}
			}
			else if(transform.position.x >= -8.8 || transform.position.x <= 8.8){
				if(rewiredPlayer.GetButtonDown("Horizontal")) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove, 1); // Move right
				}
			}
		}	
	}

	void Kiss()
	{
		Debug.Log("kissing!");
	}
}