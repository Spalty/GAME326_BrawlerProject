using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;
    private InputHandler InputHandler => Context.InputHandler;
    private int JumpCount => Context.JumpCount;

    public override void EnterState()
    {
        InitializeSubState();
        Context.CurrentRootState = RootStates.Jump;
        

        
        if(JumpCount > 0 && !Context.IsGrounded()) // AIR JUMP
        {
          PlayerRB.linearVelocity = new Vector2(Context.InputHandler.HorizontalInput * FighterData.HorizontalJumpForce/2, FighterData.VerticalJumpForce/2);
            Context.JumpCount--;
        }
        else if (Context.IsGrounded()) // GROUND JUMP
        {
            PlayerRB.linearVelocity = new Vector2(Context.InputHandler.HorizontalInput * FighterData.HorizontalJumpForce, FighterData.VerticalJumpForce);
            Context.JumpCount--;
        }
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (!Context.IsGrounded())
        {
            SwitchState(Factory.Airborne());
        }
        else
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void ExitState()
    {
        InputHandler.WasJumpPressed = false; // Reset the input flag
    }
}
