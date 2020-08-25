using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* created by Aubrey Isaacman
 * following this tutorial: https://www.youtube.com/watch?v=ailbszpt_AI
 *
 * this script stops the players from moving outside of the screen boundaries
 * edited to work with ORTHOGRAPHIC CAMERA via the tutorial's update https://pressstart.vip/tutorials/2018/06/28/41/keep-object-in-bounds.html
*/

public class Boundaries : MonoBehaviour
{
/*
    // variables
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        /* remember: screen coordinate system is reversed from the world coordinate system
         * so our values will be NEGATIVE
        /
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // extents = size of width/2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // extents  = size of height/2
    }

    // update happens AFTER our movement script
    void LateUpdate()
    {
        // so we can alter our object's x and y coordinates
        Vector3 viewPos = transform.position;

        // reverse values to make them positive
        // clamp x position to screen boundaries
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        //do the same for y
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        // change position to equal our new altered position
        transform.position = viewPos;
    }
*/
}
