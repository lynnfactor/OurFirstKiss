using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by Aubrey Isaacman
 *
 * This script allows players to change their appearance by adding an accessory!
 * This can be a hat, hair style, headband, etc.
 *
 * Following this tutorial: https://www.youtube.com/watch?v=jPbdFOPkfy0&list=PLBIb_auVtBwBq9S1R-j4oL0HnlDh_rpLW&index=5&ab_channel=Blackthornprod
*/

public class ChangeAppearance : MonoBehaviour
{

    // scroll
    public KeyCode left;
    public KeyCode right;
    // select
    public KeyCode select;

    // the sprite things
    public SpriteRenderer part;
    public Sprite[] options;
    public int index;

    // Update is called once per frame
    void Update()
    {
        // go through the array of accessory options
        for (int i = 0; i<options.Length; i++)
        {
            if (i==index)
            {
                // actually selecting the accessory!
                part.sprite = options[i];
            }
        }

        // call Swap function on button presses
        if(Input.GetKeyDown(select))
        {
            Swap();
        }
    }

    public void Swap()
    {
        /*
        if(Input.GetKeyDown(left))
        {
            Debug.Log("left button");
            // if you've changed your selection
            if (options.Length - 1 > index)
            {
                // change the accessory
                index--;
            } else {
                // if you haven't, keep it the same
                index = 0;
            }
        }
        */

        
        //if(Input.GetKeyDown(select))
        //{
         //   Debug.Log("right button");
            if (index<options.Length - 1)
            {
                // change the accessory
                index++;
            } else {
                // if you haven't, keep it the same
                index = 0;
            }
        //}
        

        /*
        // if you've changed your selection
        if (index<options.Length - 1)
        {
            // change the accessory
            index++;
        } else {
            // if you haven't, keep it the same
            index = 0;
        }
        */
    }
}
