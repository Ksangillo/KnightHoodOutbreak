using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Pausemenu : MonoBehaviour
{
   private _2D _2d;
   private InputAction UI;
  
    [SerializeField]
    private GameObject pauseMenuUI;

  
    public bool isPaused;
    //disables the player's input when paused;
    public PlayerInput playerControls;
    public GameObject player;
    string playerName = "Player";
    


    // Start is called before the first frame update
    void Awake()
    {
        _2d = new _2D();
       

        player = GameObject.FindGameObjectWithTag(playerName);
        player.GetComponent<PlayerInput>();
        PauseMenuOff();//menu fix bug
    }

    private void OnEnable()
    {
        //getting the controls from the actionmap
        UI = _2d.UI.PauseMenu;
        UI.Enable();
        //whenever the action is performed calls the Pause method
        UI.performed += Pause; 
    }

    private void OnDisable()
    {
        UI.Disable();
    }

     void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;


        if(isPaused)
        {
            PausMenuON();
            
        }
        else
            PauseMenuOff();
    }
    void PausMenuON()
    {

        Time.timeScale = 0f;
        //can pause audio 
        pauseMenuUI.SetActive(true);
        isPaused = true;
        playerControls.enabled = false;

    }

    public void PauseMenuOff()
    {
        Time.timeScale = 1f;
        //can pause audio 
        pauseMenuUI.SetActive(false);
        playerControls.enabled = true;
        isPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Debug.Log("You Exited the Game");
        Application.Quit();
    }

}
