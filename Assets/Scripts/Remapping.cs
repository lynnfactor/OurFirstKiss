using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remapping : MonoBehaviour
{
    public Text p1left;
    public Text p1right;
    public Text p1kiss;
    public Text p2left;
    public Text p2right;
    public Text p2kiss;
    public GameObject bottom; // bottom text
    private string buttonClicked = null;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("P1Left", "A");
        PlayerPrefs.SetString("P1Right", "D");
        PlayerPrefs.SetString("P1Kiss", "E");
        
        PlayerPrefs.SetString("P1Left", "J");
        PlayerPrefs.SetString("P1Right", "L");
        PlayerPrefs.SetString("P1Kiss", "U");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonClicked != null)   
        {
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                Debug.Log(kcode);
                if (Input.GetKey(kcode))
                {
                    PlayerPrefs.SetString(buttonClicked,kcode.ToString());
                    if (buttonClicked == "P1Left")
                    {
                        p1left.text = kcode.ToString();
                    }
                    else if (buttonClicked == "P1Right")
                    {
                        p1right.text = kcode.ToString();
                    }
                    else if (buttonClicked == "P1Kiss")
                    {
                        p1kiss.text = kcode.ToString();
                    }
                    else if (buttonClicked == "P2Left")
                    {
                        p2left.text = kcode.ToString();
                    }
                    else if (buttonClicked == "P2Right")
                    {
                        p2right.text = kcode.ToString();
                    }
                    else if (buttonClicked == "P2Kiss")
                    {
                        p2kiss.text = kcode.ToString();
                    }
                    buttonClicked = null;
                    bottom.SetActive(false);
                    break;
                }
            }    

        }
    }

    public void P1Left()
    {
        buttonClicked = "P1Left";
    }
    public void P1Right()
    {
        buttonClicked = "P1Right";
    }
    public void P1Kiss()
    {
        buttonClicked = "P1Kiss";
    }

    public void P2Left()
    {
        buttonClicked = "P2Left";
    }
    public void P2Right()
    {
        buttonClicked = "P2Right";
    }
    public void P2Kiss()
    {
        buttonClicked = "P2Kiss";
    }
}
