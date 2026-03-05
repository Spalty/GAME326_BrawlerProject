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

    public void OnHorizontal(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
    }

    public void OnVertical(InputValue value)
    {
        _verticalInput = value.Get<Vector2>().y;
    }

    public void OnDash()
    {
        WasDashPressed = true;
    }

    public void OnJump()
    {
        WasJumpPressed = true;
    }
    
    public void OnLight()
    {
        WasLightAttackPressed = true;
    }
    
    public void OnMedium()
    {
        WasMediumAttackPressed = true;
    }

    public void OnHeavy()
    {
        WasHeavyAttackPressed = true;
    }
}
