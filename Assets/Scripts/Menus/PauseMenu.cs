using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private PlayerMovement _playerMovement;
    private bool isPaused;

    private void Awake()
    {
        
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    

    public void PauseGame(InputAction.CallbackContext _)
    {
        isPaused = true;
        if (isPaused == true)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
}
