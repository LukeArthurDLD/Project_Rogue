using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement playerMovement;
    private Weapon currentWeapon;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        // get components
        playerMovement = GetComponent<PlayerMovement>();
        currentWeapon = GetComponentInChildren<Weapon>();

        onFoot.Jump.performed += ctx => playerMovement.Jump(); // when button pressed

        onFoot.Attack.started += ctx => currentWeapon.StartAttack(); // when input starts
        onFoot.Attack.canceled += ctx => currentWeapon.EndAttack(); // when input ends
    }

    void FixedUpdate()
    {
        // Tell player movement to move by reading movement value
        playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
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
