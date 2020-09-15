using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* created by Aubrey Isaacman
 * 
 * This script will handle other things that happen in the menu
 **** scene change on button click
 **** restart game on "R" pressed
 **** quit game on "Q" pressed
 **** and you can add these functions to UI buttons
 *
 * Following this Bracky's tutorials
 **** https://www.youtube.com/watch?v=zc8ac_qUXQY&t=596s&ab_channel=Brackeys
 * and this tutorial for changing the scene
 **** https://www.youtube.com/watch?v=Xl8xgmsrKwE
*/

public class MenuControls : MonoBehaviour
{

    [SerializeField] private string scene;

    
    void Start()
    {
        
    }

    void Update()
    {
        if((Input.GetKey(KeyCode.Q))/* && (Input.GetKey(KeyCode.Equals))*/)
        {
            QuitGame();
        }

        if((Input.GetKey(KeyCode.R))/* && (Input.GetKey(KeyCode.Equals))*/)
        {
            Debug.Log("r pressed");
            RestartGame();
        }
    }
    
    public void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
