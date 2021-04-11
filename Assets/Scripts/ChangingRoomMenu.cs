using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* By Lex Yu, lsyu@usc.edu
*/
public class ChangingRoomMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("scene2-Movie");
    }

    public void Quit(){
        Application.Quit();
    }

    public void LoadChangingRoom()
    {
        SceneManager.LoadScene("scene1-ChangingRoom");
    }
}
