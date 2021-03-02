using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Uduino;
using System;

/* by Aubrey Isaacman, Trever Berryman, and Lex Yu
 *
 * Trever's code:
 * players can move to the seats directly next to them
 * players wobble when they move to mimic moving seats
 *
 * Aubrey's code:
 * when players are sitting next to each other, they prep to kiss
 * if players are both pressing their kiss buttons, they'll kiss
 * Arduino input via Uduino ReadWrite
 ***** analog pins A0 and A3 are for flex sensor input
 ***** pins 3 and 6 are for analog LED output
 * 
*/
// Kissing particles: lsyu@usc.edu. particles should spawn when players hold down kiss buttons. also players should lean into each other.
public class StartPlayerMovement : MonoBehaviour {

	// Arduino input
	UduinoManager u;
	// player 1 stuff
		int p1ReadVal = 0;
		// left button
		int p1LeftPin = 2;
		int p1LeftVal = 0;
		public bool p1LeftPress = false;
		// right button
		int p1RightPin = 4;
		int p1RightVal = 0;
		public bool p1RightPress = false;
	// player 2 stuff
		int p2ReadVal = 0;
		// left button
		int p2LeftPin = 5;
		int p2LeftVal = 0;
		public bool p2LeftPress = false;
		// right button
		int p2RightPin = 7;
		int p2RightVal = 0;
		public bool p2RightPress = false;
	// for both players
		public int minKissPressure = 20;

	[Header("Rewired")]
    public Player rewiredPlayer; // Player object for rewired
	public Player otherPlayer;
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

	// So we can swap out the player sprite so they can look at each other when sitting next to each other
	private SpriteRenderer spriteRend;
	public Sprite spriteRest; // facing movie
	public Sprite spriteReady; // facing partner

	// So we can rotate the players towards each other for kissing
	// target marker
	public Transform target;
	// angular speed in radians per sec.
	public float rotSpeed = 1.0f;

	//are they kissing?
	public bool isKissing = false;
	private bool collidedWithPlayer = false;

	void Start() {

		u = UduinoManager.Instance;

		// player 1
			// pressure sensor
			u.pinMode(AnalogPin.A0, PinMode.Input);
			// LED
			u.pinMode(3, PinMode.Output);
			// push buttons
			// left
			u.pinMode(2, PinMode.Input_pullup);
			// right
			u.pinMode(4, PinMode.Input_pullup);

		// player 2
			// pressure sensor
			u.pinMode(AnalogPin.A3, PinMode.Input);
			// LED
			u.pinMode(6, PinMode.Output);
			// push buttons
			// left
			u.pinMode(5, PinMode.Input_pullup);
			// right
			u.pinMode(7, PinMode.Input_pullup);

		// the particle system is OFF
		//smoochParticle.GetComponent<ParticleSystem>().enableEmission = false;

		// sprite stuff
		spriteRend = GetComponent<SpriteRenderer>();
		// if the sprite is null, set it to resting sprite

	}

	// Sets up player ID in inspector to assign controls to the rewired Player object
	private void InitializeControls() {
        rewiredPlayer = ReInput.players.GetPlayer(playerID);
		int otherID = 1;
		if (playerID == 1){
			otherID = 0;
		}
		otherPlayer = ReInput.players.GetPlayer(otherID);
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
		// read the sensor value
		ReadValue();
		
		// Debug
		// player 1
		//Debug.Log("PLAYER 1 " + "left: " + p1LeftVal);
		//Debug.Log("PLAYER 1 " + "right: " + p1RightVal);
		// player 2
		//Debug.Log("PLAYER 2 " + "left: " + p2LeftVal);
		//Debug.Log("PLAYER 2 " + "right: " + p2RightVal);

		// move the players

		if (Input.anyKey)
        {
			Move ();
		}
		Debug.Log(gameObject.name + " " + transform.localPosition.x);
		if (transform.localPosition.x > 8.8f){
			transform.localPosition = new Vector3(8.8f, transform.localPosition.y, transform.localPosition.z);
		}
		else if (transform.localPosition.x < -8.8f){
			transform.localPosition = new Vector3(-8.8f, transform.localPosition.y, transform.localPosition.z);
		}
	}

	void ReadValue()
    {

		// player 1
			// when P1 pressure sensor is pushed,
			p1ReadVal = u.analogRead(AnalogPin.A0);
			// change their LED brightness to that relative value
			u.analogWrite(3, p1ReadVal / 6);
			
		// buttons
			p1LeftVal = u.digitalRead(p1LeftPin);
			p1RightVal = u.digitalRead(p1RightPin);

		// player 2
			// when P2 pressure sensor is pushed,
			p2ReadVal = u.analogRead(AnalogPin.A3);
			// change their LED brightness to that relative value
			u.analogWrite(6, p2ReadVal / 6);
			
			//butons
			p2LeftVal = u.digitalRead(p2LeftPin);
			p2RightVal = u.digitalRead(p2RightPin);
    }

	// Sets collided to true if either player's box collider collides with each other
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "P1" || other.gameObject.name == "P2" || other.gameObject.name == "RandomAudience") {
			collided = true;
		}
	}

	// Sets collided to false if either player's box collider exits the other player's box collider
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.name == "P1" || other.gameObject.name == "P2" || other.gameObject.name == "RandomAudience") {
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
		if(collided == true /*|| isKissing == true*/) {

			if (transform.localPosition.x < -8.8f) {
				//Don't move
				transform.localPosition = new Vector3(-8.8f, transform.localPosition.y, transform.localPosition.z);
			}
			// Player is at right barrier, don't move right
			else if(transform.localPosition.x > 8.8f) {
				//Don't move
				transform.localPosition = new Vector3(8.8f, transform.localPosition.y, transform.localPosition.z);
			}
			
			

			// MOVE LOGIC:
			// Player 1 can only move left when collided is true
			if (gameObject.name == "P1" && (rewiredPlayer.GetNegativeButtonDown("Horizontal") || p1LeftVal == 1)) {
				if(transform.localPosition.x > -8.8) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + amountToMove * -1.0f, 1); // Move left
						
				}
			}
			// Player 2 can only move right when collided is true
			else if(gameObject.name == "P2" && (rewiredPlayer.GetButtonDown("Horizontal")) || p2RightVal == 1) {
				if(transform.localPosition.x < 8.8) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + amountToMove, 1); // Move right

				}
			}

		}
		
		// Movement logic for when players are separated
		if(collided != true /*&& isKissing != true*/) {
			if (transform.localPosition.x < -8.8f) {
				//Don't move
				transform.localPosition = new Vector3(-8.8f, transform.localPosition.y, transform.localPosition.z);
			}
			// Player is at right barrier, don't move right
			else if(transform.localPosition.x > 8.8f) {
				//Don't move
				transform.localPosition = new Vector3(8.8f, transform.localPosition.y, transform.localPosition.z);
				Debug.Log("snap to -8.8");
			}
			
			
			// If Player's are within the barriers, move normally

			if(transform.localPosition.x >= -8.8 && transform.localPosition.x <= 8.8) {
				if(rewiredPlayer.GetButtonDown("Horizontal") /*|| p1RightVal == 1 this value moves the player to the right but they move far away really fast*/) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + amountToMove, 1); // Move right
				}
				else if(rewiredPlayer.GetNegativeButtonDown("Horizontal") /*|| p1LeftVal == 1*/) {
					StartCoroutine(Wiggle()); //Start wiggle corouitine
					transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition + amountToMove * -1.0f, 1); // Move left
				}
			}
			
			
		}	
	}




}