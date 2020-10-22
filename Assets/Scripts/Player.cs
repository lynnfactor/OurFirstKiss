using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by Aubrey Isaacman
 *
 * This script makes sure whatever options the player selects from the start scene stay
 * AKA whatever hat they chose to wear stays on when the Movie scene starts
 *
 * Following this tutorial: https://www.youtube.com/watch?v=DsCbXaLJj20&list=PLBIb_auVtBwBq9S1R-j4oL0HnlDh_rpLW&index=6&ab_channel=Blackthornprod
 *
 */

public class OFKPlayer : MonoBehaviour
{
    public static OFKPlayer instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        } else
        {
            Destroy(this);
        }
    }

}
