using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private FighterController _fighterController;
    private bool _wasDashPressed;
    private bool _wasJumpPressed;

    private bool _isLightAttackPressed;
    private bool _isMediumAttackPressed;
    private bool _isHeavyAttackPressed;
    
    private float _horizontalInput;
    private float _verticalInput;

    #region ---Getter/Setters---
    public bool IsLightAttackPressed { get { return _isLightAttackPressed; } set { _isLightAttackPressed = value; } }
    public bool IsMediumAttackPressed { get { return _isMediumAttackPressed; } set { _isMediumAttackPressed = value; } }
    public bool IsHeavyAttackPressed { get { return _isHeavyAttackPressed; } set { _isHeavyAttackPressed = value; } }
    public bool WasJumpPressed { get { return _wasJumpPressed; } set { _wasJumpPressed = value; } }
    public bool WasDashPressed { get { return _wasDashPressed; } set { _wasDashPressed = value; } }
    public float moveDirection { get { return _horizontalInput; } set { _horizontalInput = value; } }
    public float verticalInput { get { return _verticalInput; } set { _verticalInput = value; } }
    #endregion
    
    void Awake()
    {
        _fighterController = new FighterController();
    }


    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>().x;
        }
        else if (context.canceled)
        {
            moveDirection = 0f;
        }
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            verticalInput = context.ReadValue<Vector2>().y;
        }
        else if (context.canceled)
        {
            verticalInput = 0f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WasDashPressed = true;
        }
        else if (context.canceled)
        {
            WasDashPressed = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            WasJumpPressed = true;   
        }
        else if (context.canceled)
        {
            WasJumpPressed = false;
        }
    }
    public void OnLight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsLightAttackPressed = true;
        }
        else if (context.canceled)
        {
            IsLightAttackPressed = false;
        }
    }
    public void OnMedium(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsMediumAttackPressed = true;
        }
        else if (context.canceled)
        {
            IsMediumAttackPressed = false;
        }
    }
    public void OnHeavy(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsHeavyAttackPressed = true;
        }
        else if (context.canceled)
        {
            IsHeavyAttackPressed = false;
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
        
        
        _fighterController.StandingActions.Dash.performed += OnDash;
        _fighterController.StandingActions.Dash.canceled += OnDash;
        
        _fighterController.StandingActions.Light.performed += OnLight;
        _fighterController.StandingActions.Light.canceled += OnLight;
        
        _fighterController.StandingActions.Medium.performed += OnMedium;
        _fighterController.StandingActions.Medium.canceled += OnMedium;
        
        _fighterController.StandingActions.Heavy.performed += OnHeavy;
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
        
        
        _fighterController.StandingActions.Dash.performed -= OnDash;
        _fighterController.StandingActions.Dash.canceled -= OnDash;
        
        _fighterController.StandingActions.Light.performed -= OnLight;
        _fighterController.StandingActions.Light.canceled -= OnLight;
        
        _fighterController.StandingActions.Medium.performed -= OnMedium;
        _fighterController.StandingActions.Medium.canceled -= OnMedium;
        
        _fighterController.StandingActions.Heavy.performed -= OnHeavy;
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
