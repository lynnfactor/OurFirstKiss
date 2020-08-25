using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* created by Aubrey Isaacman
 * 
 * This script lets you change the scene by pressing a UI button
 * Can go on an empty game object
 * I personally like it on the Main Camera so that's where this is!
 * Following this tutorial: https://www.youtube.com/watch?v=Xl8xgmsrKwE
*/

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string scene;

    public void ButtonMoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
