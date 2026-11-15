using UnityEngine;
using UnityEngine.InputSystem;

public class DisableCursor : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction quit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    void OnEnable()
    {
        quit = playerInput.Interface.Quit;
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        // QUIT GAME
        if (quit.WasPressedThisFrame())
        {
            Application.Quit();
        }
    }
}
