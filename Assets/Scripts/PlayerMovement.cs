using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rigidbody;
    private Vector2 movement;
    public PlayerInput input;
    private InputAction interact;
    private InputAction pause;
    private Interactable selectedInteractable;
    public Animator animator;
    private GameController gameController;
    private Footsteps footsteps;
    
    [Header ("Menu")]
    private static bool isPaused;

    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject backButton;

    private void Awake()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
        
        rigidbody = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();

        input = new PlayerInput();
        interact = input.Player.Interact;
        pause = input.Player.Pause;
        
        input.Player.Interact.performed += Interact;
        input.Player.Pause.performed += PauseGame;

    }


    private void Update()
    {
        if (gameController.cantMove == false)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Speed", 0f);
        }
        
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }


    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    private void OnDisable()
    {
        DisableInput();
    }
  private void OnEnable()
    {
        EnableInput();
    }

    private void OnDestroy()
    {
        input.Player.Interact.performed -= Interact;
    }

    #region Interaction

    private void Interact(InputAction.CallbackContext _)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    private void TrySelectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null)
        {
            return;
        }

        if (selectedInteractable != null)
        {
            selectedInteractable.Deselect();
        }

        selectedInteractable = interactable;
        selectedInteractable.Select();
    }

    private void TryDeselectInteractable(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable == null)
        {
            return;
        }

        if (interactable == selectedInteractable)
        {
            selectedInteractable.Deselect();
            selectedInteractable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrySelectInteractable(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        TryDeselectInteractable(other);
    }

    public void DisableInput()
    {
        input.Disable(); 
    }
    
    public void EnableInput()
    {
        input.Enable();
    }
    
    #endregion

    #region Sounds

    private void PlayFootstep()
    {
        footsteps.WalkSound();
    }

    #endregion

    #region Menu

    private void PauseGame(InputAction.CallbackContext _)
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(resumeButton, new BaseEventData(eventSystem));
            gameController.cantMove = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameController.cantMove = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.None;
        isPaused = false;
        gameController.cantMove = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OptionsOpened()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(backButton, new BaseEventData(eventSystem));
    }
    public void PauseOpened()
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(resumeButton, new BaseEventData(eventSystem));
    }
    
    #endregion
}
