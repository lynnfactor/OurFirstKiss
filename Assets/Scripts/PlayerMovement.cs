using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour {

	[Header("Rewired")]
    public Player rewiredPlayer;
    public int playerID;

	[Header("Movement")]
	private float moveH;	
	public float amountToMoveModifier;
	Vector3 amountToMove;
	public float minimum = 5.0f;
	public float maximum = 10.0f;
	public float duration = 5.0f;
	float startTime;
	
	[Header("Movement")]
	// public gameObject Player;
	public bool collided;

	[Header("Rotation")]
	public float speed = 2f;
	public float time;
	public float maxRotation = 45f;

	void Start() {
		startTime = Time.time;
		Debug.Log("Player " + playerID + " Position: " + transform.position.x);
		print("Start: " + gameObject.name);
	}

	private void InitializeControls()
    {
        rewiredPlayer = ReInput.players.GetPlayer(playerID);
    }

	private void Awake()
    {
        //Initializes controls
        InitializeControls();
    }

	void GetMovementInput()
    {
        moveH = rewiredPlayer.GetAxisRaw("Horizontal");
    }

	void Update () {
		Move ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameObject.name == "P1" || gameObject.name == "P2") {
			Debug.Log("Touching: " + gameObject.name);
			collided = true;	
        }
	}
	void Move()
	{
		
		amountToMove = new Vector3(amountToMoveModifier,0,0);
		if(transform.position.x < -8.8 && rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
			//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
		}
		else if(transform.position.x > 8.8 && rewiredPlayer.GetButtonDown("Horizontal")) {
			//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
		}
		else if(transform.position.x >= -8.8 || transform.position.x <= 8.8) {
			if(rewiredPlayer.GetButtonDown("Horizontal")) {
				transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove, 1);
				// transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed * time), 0f, 0f);
				//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
			}
			else if(rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
				transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove * -1.0f, 1);
				// transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed * time), 0f, 0f);
				//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
			}
		}
		else if(transform.position.x >= -8.8 || transform.position.x <= 8.8 && gameObject.name == "P1" && collided == true){
			if(rewiredPlayer.GetNegativeButtonDown("Horizontal")) {
				transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove * -1.0f, 1);
				// transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed * time), 0f, 0f);
				//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
			}
		}
		else if(transform.position.x >= -8.8 || transform.position.x <= 8.8 && gameObject.name == "P2" && collided == true){
			if(rewiredPlayer.GetButtonDown("Horizontal")) {
				transform.position = Vector3.Lerp(transform.position, transform.position + amountToMove, 1);
				// transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * speed * time), 0f, 0f);
				//Debug.Log("Player " + playerID + " Position: " + transform.position.x);
			}
		}
	}
}