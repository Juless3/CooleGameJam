using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private Footsteps footsteps;
    
    private static bool isPaused;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);
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

    public void PauseGame(InputAction.CallbackContext _)
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            gameController.cantMove = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            gameController.cantMove = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    
    #endregion
}
