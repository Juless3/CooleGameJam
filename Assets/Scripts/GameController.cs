using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerMovement player;

    private DialogueController dialogueController;

    public bool cantMove;
    
    #region Unity Event Functions

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player == null)
        {
            Debug.LogError("No player found in scene.", this);
        }
        
        dialogueController = FindObjectOfType<DialogueController>();

        if (dialogueController == null)
        {
            Debug.LogError("No dialogueController found in scene.", this);
        }
    }

    private void OnEnable()
    {
        DialogueController.DialogueClosed += EndDialogue;
    }

    private void Start()
    {
        EnterPlayMode();
    }

    private void OnDisable()
    {
        DialogueController.DialogueClosed -= EndDialogue;
    }

    #endregion

    #region Modes

    private void EnterPlayMode()
    {
        cantMove = false;
        // In the editor: Unlock with ESC.
        Cursor.lockState = CursorLockMode.Locked;
        player.EnableInput();
        player.moveSpeed = 3f;
    }

    private void EnterDialogueMode()
    {
        cantMove = true;
        Cursor.lockState = CursorLockMode.None;
        player.DisableInput();
        player.moveSpeed = 0f;
    }

    #endregion

    public void StartDialogue(string dialoguePath)
    {
        EnterDialogueMode();
        dialogueController.StartDialogue(dialoguePath);
    }

    private void EndDialogue()
    {
        EnterPlayMode();
    }
}