using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/* created by Aubrey Isaacman
 * 
 * Following this tutorial specifically for rotation: https://www.youtube.com/watch?v=lJ1jv5OhZZE
 * Following "Complete C# Unity Game Developer 2D" course for player movement
 *
 * This script moves the players right and left
 * and rotates them to the left or to the right (to lean in for smooches)
        * As of 2020.09.03, Aubrey commented out rotation
 * 
 * TEMPORARILY works only with KEYBOARD PRESS
 * Eventually this script will be controlled via accelerometer (rotation) and proximity sensor (movement)
 *
*/


public class PlayerMovement : MonoBehaviour {


    //Movement
    public float moveH;
    public Vector3 movement;
    public KeyCode left;
    public KeyCode right;
    public float degrees;

    [Header("Rewired")]
    public Player rewiredPlayer;
    public int playerID;
    public bool isPlayer1 = false;
    private string playerName;

    //initializes controls for Rewired
    private void InitializeControls()
    {
        
        rewiredPlayer = ReInput.players.GetPlayer(playerID);

    }

    void GetMovementInput()
    {

        moveH = rewiredPlayer.GetAxisRaw("Horizontal");

    }

    private void Awake()
    {
        //initializes controls
        InitializeControls();

        if (isPlayer1)
        {

            playerName = "Player1";

        }
        else
        {

            playerName = "Player2";

        }
    }

    void Update () {

        GetMovementInput();
        movement = new Vector2(moveH,0f);
        movement = movement * moveH * Time.deltaTime;
        transform.position += movement;

    //    if(game)
       
    //    if(Input.GetKey(left))
    //     {
    //             
    //     }
    //     // move silhouette right
    //     if(Input.GetKey(right))
    //     {
    //         movement = movement * moveH * Time.deltaTime;
    //         transform.position += movement;
    //     }
    }

}

// public class PlayerMovement : MonoBehaviour
// {

//     // movement
//     public KeyCode left;
//     public KeyCode right;

//     // range of walking speed
//     [Range (0f, 500f)]
//     // set floating speed at 1
//     public float moveSpeed = 1f;
//     Vector2 movement;
    
//     // rotation
//     public KeyCode rotLeft;
//     public KeyCode rotRight;

//     private float rotZ;
//     public int rotSpeed = 0;
//     public Vector3 point; // position of point you want to rotate around

//     // player input
//     private float _horizontalInput = 0;
//     private float _verticalInput = 0;

//     // this smooch collider
//     public Collider2D smoochZone;
//     public Collider2D otherSmoochZone;
//     // are they smooching now?
//     public bool smoochingNow = false;

//     Rigidbody2D rb2D;

//     // Start is called before the first frame update
//     void Start()
//     {
//         rb2D = GetComponent<Rigidbody2D>();
//     }

//     // reference player input
//     public void GetPlayerInput()
//     {
//         _horizontalInput = Input.GetAxisRaw("Horizontal");
//         _verticalInput = Input.GetAxisRaw("Vertical");
//     }

//     // once per frame
//     void Update()
//     {
//         movement.x = Input.GetAxisRaw("Horizontal");


//         //GetPlayerInput();

//         // move silhouette left
//                 // "GetKey" lets you keep doing something even when you're holding the button down
//                 // "GetKeyDown" only does it the one time the button is pressed
        
//         if(Input.GetKeyDown(left))
//         {
//             transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
            
//         }
//         // move silhouette right
//         if(Input.GetKeyDown(right))
//         {
//             transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
            
//         }
        
//     }

//     // for physics
//     /*
//     void FixedUpdate()
//     {
//         // rotate player by calling that function
//         //RotatePlayer();  

//         //rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);

//     }
//     */

//     /*
//     // rotate
//     private void RotatePlayer()
//     {
//         // rotate left
//         if(Input.GetKey(rotLeft))
//         {
//             rotZ += Time.deltaTime * rotSpeed;
//         }
//         // rotate right
//         if(Input.GetKey(rotRight))
//         {
//             rotZ += -Time.deltaTime * rotSpeed;
//         }

//         transform.rotation = Quaternion.Euler(0,0, rotZ);
//     }
//     */

//     // detect if players are kissing
//     // give feedback for smooching
//     // THIS IS GIVING ME ISSUES, revisit
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//        if(other.gameObject.tag == "Smooch")
//        {
//            // decrease b&w filter by X amount

           
//            // play random kissy sounds
//                 // this will probably mean calling a random audio script from KB or Cloud
//            // spawn hearts of random sizes

//            // if players have smooched X number of times, spawn random fireworks too
//            Debug.Log("smooching");
//        }
//     }


// }