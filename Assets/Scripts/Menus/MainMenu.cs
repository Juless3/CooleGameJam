using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject controlButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject backButton;

    
    
    public void ControlsOpened()
    {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(backButton, new BaseEventData(eventSystem));
    }
    
    public void ControlsClosed()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(startButton, new BaseEventData(eventSystem));
    }

    public void StartGame()
    {
        SceneManager.LoadScene("City");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
