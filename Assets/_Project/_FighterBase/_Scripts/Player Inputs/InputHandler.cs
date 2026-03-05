using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private FighterController _fighterController;
    private bool _wasDashPressed;
    private bool _wasJumpPressed;

    private bool _wasLightAttackPressed;
    private bool _wasMediumAttackPressed;
    private bool _wasHeavyAttackPressed;
    
    private float _horizontalInput;
    private float _verticalInput;

    #region ---Getter/Setters---
    public bool WasLightAttackPressed { get { return _wasLightAttackPressed; } set { _wasLightAttackPressed = value; } }
    public bool WasMediumAttackPressed { get { return _wasMediumAttackPressed; } set { _wasMediumAttackPressed = value; } }
    public bool WasHeavyAttackPressed { get { return _wasHeavyAttackPressed; } set { _wasHeavyAttackPressed = value; } }
    public bool WasJumpPressed { get { return _wasJumpPressed; } set { _wasJumpPressed = value; } }
    public bool WasDashPressed { get { return _wasDashPressed; } set { _wasDashPressed = value; } }
    public float HorizontalInput { get { return _horizontalInput; } set { _horizontalInput = value; } }
    public float VerticalInput { get { return _verticalInput; } set { _verticalInput = value; } }
    #endregion
    
    void Awake()
    {
        _fighterController = new FighterController();
    }


    // Update is called once per frame
    void Update()
    {
        // Reset WasJumpPressed so it only lasts one frame
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HorizontalInput = context.ReadValue<Vector2>().x;
        }
        else if (context.canceled)
        {
            HorizontalInput = 0f;
        }
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            VerticalInput = context.ReadValue<Vector2>().y;
        }
        else if (context.canceled)
        {
            VerticalInput = 0f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WasDashPressed = true;
        }
        else
        {
            WasDashPressed = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            WasJumpPressed = true;
        }
    }
    public void OnLight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            WasLightAttackPressed = true;
        }
        
    }
    public void OnMedium(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            WasMediumAttackPressed = true;
        }
    }
    public void OnHeavy(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            WasHeavyAttackPressed = true;
        }
    }

    private void OnEnable()
    {
        _fighterController.Enable();
        _fighterController.StandingActions.Enable();
        _fighterController.CrouchingActions.Enable();

        //--STANDING ACTIONS--
        _fighterController.StandingActions.Horizontal.performed += OnHorizontal;
        _fighterController.StandingActions.Horizontal.canceled += OnHorizontal;
        _fighterController.StandingActions.Vertical.performed += OnVertical;
        _fighterController.StandingActions.Vertical.canceled += OnVertical;

        _fighterController.StandingActions.Jump.started += OnJump;
        _fighterController.StandingActions.Jump.canceled += OnJump;
        
        
        _fighterController.StandingActions.Dash.performed += OnDash;
        _fighterController.StandingActions.Dash.canceled += OnDash;
        
        _fighterController.StandingActions.Light.started += OnLight;
        _fighterController.StandingActions.Light.canceled += OnLight;
        
        _fighterController.StandingActions.Medium.started += OnMedium;
        _fighterController.StandingActions.Medium.canceled += OnMedium;
        
        _fighterController.StandingActions.Heavy.started += OnHeavy;
        _fighterController.StandingActions.Heavy.canceled += OnHeavy;
        
        //--CROUCHING ACTIONS--    
        _fighterController.CrouchingActions.CrouchLight.performed += OnLight;
        _fighterController.CrouchingActions.CrouchLight.canceled += OnLight;
        
        _fighterController.CrouchingActions.CrouchMedium.performed += OnMedium;
        _fighterController.CrouchingActions.CrouchMedium.canceled += OnMedium;
        
        _fighterController.CrouchingActions.CrouchHeavy.performed += OnHeavy;
        _fighterController.CrouchingActions.CrouchHeavy.canceled += OnHeavy;
    }
    private void OnDisable()
    {
        _fighterController.Disable();
        _fighterController.StandingActions.Disable();
        _fighterController.CrouchingActions.Disable();

        //--STANDING ACTIONS--
        _fighterController.StandingActions.Horizontal.performed -= OnHorizontal;
        _fighterController.StandingActions.Horizontal.canceled -= OnHorizontal;
        _fighterController.StandingActions.Vertical.performed -= OnVertical;
        _fighterController.StandingActions.Vertical.canceled -= OnVertical;

        _fighterController.StandingActions.Jump.started -= OnJump;
        _fighterController.StandingActions.Jump.canceled -= OnJump;
        
        
        _fighterController.StandingActions.Dash.performed -= OnDash;
        _fighterController.StandingActions.Dash.canceled -= OnDash;
        
        _fighterController.StandingActions.Light.started -= OnLight;
        _fighterController.StandingActions.Light.canceled -= OnLight;
        
        _fighterController.StandingActions.Medium.started -= OnMedium;
        _fighterController.StandingActions.Medium.canceled -= OnMedium;
        
        _fighterController.StandingActions.Heavy.started -= OnHeavy;
        _fighterController.StandingActions.Heavy.canceled -= OnHeavy;

        //--CROUNCHING ACTIONS--      
        _fighterController.CrouchingActions.CrouchLight.performed -= OnLight;
        _fighterController.CrouchingActions.CrouchLight.canceled -= OnLight;
        
        _fighterController.CrouchingActions.CrouchMedium.performed -= OnMedium;
        _fighterController.CrouchingActions.CrouchMedium.canceled -= OnMedium;
        
        _fighterController.CrouchingActions.CrouchHeavy.performed -= OnHeavy;
        _fighterController.CrouchingActions.CrouchHeavy.canceled -= OnHeavy;
    }
}
