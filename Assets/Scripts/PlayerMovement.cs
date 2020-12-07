using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Uduino;

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

	// So we can swap out the player sprite so they can look at each other when sitting next to each other
	private SpriteRenderer spriteRend;
	public Sprite spriteRest; // facing movie
	public Sprite spriteReady; // facing partner

	// So we can rotate the players towards each other for kissing
	// target marker
	public Transform target;
	// angular speed in radians per sec.
	public float rotSpeed = 1.0f;
	public float kissRot;

	//public ParticleSystem smoochParticle;
	public Transform kissparticle;

	//are they kissing?
	private bool isKissing = false;

	void Start() {
		// Debug position information
		//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
		//print("Start: " + gameObject.name);

		// Uduino pin setup
		UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
		UduinoManager.Instance.pinMode(7, PinMode.Input_pullup);

		// the particle system is OFF
		//smoochParticle.GetComponent<ParticleSystem>().enableEmission = false;

		// sprite stuff
		spriteRend = GetComponent<SpriteRenderer>();
		// if the sprite is null, set it to resting sprite

		var kisseffect = kissparticle.GetComponent<ParticleSystem>().emission;
		kisseffect.enabled = false;
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
			//Debug.Log("Touching: " + gameObject.name); // Debug alerts for when they touch
			collided = true;	
        }
	}

	// Sets collided to false if either player's box collider exits the other player's box collider
	void OnTriggerExit2D(Collider2D other) {
		if (gameObject.name == "P1" || gameObject.name == "P2") {
			//Debug.Log("Not Touching: " + gameObject.name); // Debug alerts for when they stop touching
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
		// player input via arduino
		int p1val = UduinoManager.Instance.digitalRead(2);
		int p2val = UduinoManager.Instance.digitalRead(2);


		// Assigns the amountToMoveModifier to the x (horizontal) variable
		amountToMove = new Vector3(amountToMoveModifier,0,0);
		
		// Logic for when the players have collided	
		if(collided == true || isKissing == true) {
			// KISS LOGIC:
			// change player sprites to look at each other
			if (spriteRend.sprite == spriteRest)
			{
				spriteRend.sprite = spriteReady;
			}

			// then, if players both hit their kiss buttons, spawn cool shit
			
			if ((p1val == 1 && p2val == 1) || (Input.GetKey("e") && Input.GetKey("u")) )
			{
				Kiss();
			}
			else // if they stop hitting kiss buttons stop the particles
			{
				if ((p1val == 0 && p2val == 0) || (Input.GetKeyUp("e") && Input.GetKeyUp("u")))
				{
					Debug.Log(gameObject.name + " pos: " + transform.position.x);
					Debug.Log(gameObject.name + "target pos: " + target.position.x);
					if ((gameObject.name == "P1" && transform.position.x >= target.position.x - 3f) || (gameObject.name == "P2" && transform.position.x <= target.position.x + 3f))
					{
					Debug.Log(gameObject.name);
					Vector3 targetDirection = target.position - transform.position;
					//transform.rotation = Quaternion.Euler(0f, 0f, 5 * targetDirection.x);
					transform.position = Vector3.Lerp(transform.position, -0.5f*targetDirection + transform.position, 0.5f);
					Debug.Log(spriteRend.sprite == spriteReady);
					Debug.Log(transform.rotation);
					}
				}
				StartCoroutine(stopParticles());
				
			}

			// MOVE LOGIC:
			// Player 2 can only move left when collided is true
				if (gameObject.name == "P1" && rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
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

			// change sprites back to resting position
			if (spriteRend.sprite == spriteReady)
			{
				spriteRend.sprite = spriteRest;
			}

			// turn off kissing particle system
			StartCoroutine(stopParticles());
			// Player is at left barrier, don't move left
			if (transform.position.x < -8.8 && rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
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
		
		isKissing = true;
		// lean both players towards each other
		// players need to stay in the ready position
		if (spriteRend.sprite == spriteRest)
		{
			spriteRend.sprite = spriteReady;
		}

		// turn on particle system
		//kissparticle.GetComponent<ParticleSystem>().enableEmission = true;
		var kisseffect = kissparticle.GetComponent<ParticleSystem>().emission;
		kisseffect.enabled = true;
		kissparticle.GetComponent<ParticleSystem>().Play();
		//Debug.Log(kissparticle.GetComponent<ParticleSystem>().emission.enabled);
		Debug.Log("kissing");


		//lean in to kiss
		
		if ((gameObject.name == "P1" && transform.position.x < target.position.x - 3f) || (gameObject.name == "P2" && transform.position.x > target.position.x + 3f))
		{
			
			Vector3 targetDirection = target.position - transform.position;
			transform.rotation = Quaternion.Euler(0f, 0f, 5 * -1*targetDirection.x);
			transform.position = Vector3.Lerp(transform.position, 0.01f*targetDirection + transform.position, 0.1f);
			Debug.Log(spriteRend.sprite == spriteReady);
			Debug.Log(transform.rotation);
		}
		
		// trying to figure out how to get characters to rotate into each other's lips
		// following this tutorial: https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
		/*
		// determine which direction to rotate towards
		Vector3 targetDirection = target.position - transform.position;
		// step size is equal to speed times frame time
		float singleStep = speed * Time.deltaTime;
		Debug.Log(singleStep);
		// rotate the forward vector towards the target direction by one step
		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
		// draw a ray pointing at our target in
		Debug.DrawRay(transform.position, newDirection, Color.red);
		// calculate a rotation a step closer to the target and applies rotation to this object
		transform.rotation = Quaternion.LookRotation(newDirection);
		*/


		//following this unity answers: https://answers.unity.com/questions/650460/rotating-a-2d-sprite-to-face-a-target-on-a-single.html
		/*
		Vector3 vectorToTarget = target.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
		*/
		
	}

	IEnumerator stopParticles()
	{
		isKissing = false;
		yield return new WaitForSeconds(0.4f);

		var kisseffect = kissparticle.GetComponent<ParticleSystem>().emission;
		kisseffect.enabled = false;
		kissparticle.GetComponent<ParticleSystem>().Pause();
		kissparticle.GetComponent<ParticleSystem>().Clear();

		
	}


}