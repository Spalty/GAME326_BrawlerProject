using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool _wasDashPressed;

    private bool _isLightAttackPressed;
    private bool _isMediumAttackPressed;
    private bool _isHeavyAttackPressed;
    
    private float _moveDirection;

    #region ---Getter/Setters---
    public bool IsLightAttackPressed { get { return _isLightAttackPressed; } set { _isLightAttackPressed = value; } }
    public bool IsMediumAttackPressed { get { return _isMediumAttackPressed; } set { _isMediumAttackPressed = value; } }
    public bool IsHeavyAttackPressed { get { return _isHeavyAttackPressed; } set { _isHeavyAttackPressed = value; } }
    public bool WasDashPressed { get { return _wasDashPressed; } set { _wasDashPressed = value; } }
    public float MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
    #endregion
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        MoveDirection = value.Get<float>();
    }
    public void OnCrouch(InputValue value)
    {
        // Implementation for crouch input
    }
    public void OnJump(InputValue value)
    {
        // Implementation for jump input
    }

    public void OnDash(InputValue value)
    {
        WasDashPressed = value.isPressed;
    }
    public void OnLightAttack(InputValue value)
    {
        IsLightAttackPressed = value.isPressed;
    }
    public void OnMediumAttack(InputValue value)
    {
        IsMediumAttackPressed = value.isPressed;
    }
    public void OnHeavyAttack(InputValue value)
    {
        IsHeavyAttackPressed = value.isPressed;
    }
}
