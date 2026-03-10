using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    private bool _wasDashPressed;
    private bool _wasJumpPressed;

    private bool _wasLightAttackPressed;
    private bool _wasMediumAttackPressed;
    private bool _wasHeavyAttackPressed;
    
    #region ---Getter/Setters---
    public float HorizontalInput { get { return _horizontalInput; } set { _horizontalInput = value; } }
    public float VerticalInput { get { return _verticalInput; } set { _verticalInput = value; } }

    public bool WasJumpPressed { get { return _wasJumpPressed; } set { _wasJumpPressed = value; } }
    public bool WasDashPressed { get { return _wasDashPressed; } set { _wasDashPressed = value; } }
   
    public bool WasLightAttackPressed { get { return _wasLightAttackPressed; } set { _wasLightAttackPressed = value; } }
    public bool WasMediumAttackPressed { get { return _wasMediumAttackPressed; } set { _wasMediumAttackPressed = value; } }
    public bool WasHeavyAttackPressed { get { return _wasHeavyAttackPressed; } set { _wasHeavyAttackPressed = value; } }
    #endregion

    PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>(); 
    }

    private void OnEnable()
    {
        _playerInput.actions["Horizontal"].performed += OnHorizontal;
        _playerInput.actions["Horizontal"].canceled += OnHorizontal;

        _playerInput.actions["Vertical"].performed += OnVertical;
        _playerInput.actions["Vertical"].canceled += OnVertical;

        _playerInput.actions["Dash"].started += OnDash;
        _playerInput.actions["Jump"].started += OnJump;

        _playerInput.actions["Light"].started += OnLight;
        _playerInput.actions["Medium"].started += OnMedium;
        _playerInput.actions["Heavy"].started += OnHeavy;
    }

    private void OnDisable()
    {
        _playerInput.actions["Horizontal"].performed -= OnHorizontal;
        _playerInput.actions["Horizontal"].canceled -= OnHorizontal;

        _playerInput.actions["Vertical"].performed -= OnVertical;
        _playerInput.actions["Vertical"].canceled -= OnVertical;

        _playerInput.actions["Dash"].started -= OnDash;
        _playerInput.actions["Jump"].started -= OnJump;

        _playerInput.actions["Light"].started -= OnLight;
        _playerInput.actions["Medium"].started -= OnMedium;
        _playerInput.actions["Heavy"].started -= OnHeavy;
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x; 
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        _verticalInput = context.ReadValue<Vector2>().y;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        WasDashPressed = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        WasJumpPressed = true;
    }

    public void OnLight(InputAction.CallbackContext context)
    {
        WasLightAttackPressed = true;
    }

    public void OnMedium(InputAction.CallbackContext context)
    {
        WasMediumAttackPressed = true;
    }

    public void OnHeavy(InputAction.CallbackContext context)
    {
        WasHeavyAttackPressed = true;
    }
}
