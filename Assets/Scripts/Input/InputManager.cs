using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement playerMovement;
    // interact
    // onfoot.interact.triggered
    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        playerMovement = GetComponent<PlayerMovement>();

        onFoot.Jump.performed += ctx => playerMovement.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Tell player movement to move by reading movement value
        playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {

    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
