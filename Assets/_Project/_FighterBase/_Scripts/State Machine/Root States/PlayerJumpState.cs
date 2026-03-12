using UnityEngine;

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
        //Debug
        InitializeSubState();
        Context.CurrentRootState = RootStates.Jump;
        
        //Logic
        if (Context.IsGrounded() && JumpCount > 0) // GROUND JUMP
        {
            PlayerRB.linearVelocity = new Vector2(Context.InputHandler.HorizontalInput * FighterData.HorizontalJumpForce, FighterData.VerticalJumpForce);
        }
        else if(!Context.IsGrounded() && JumpCount > 0) // AIR JUMP
        {
            PlayerRB.linearVelocity = new Vector2(Context.InputHandler.HorizontalInput * FighterData.HorizontalJumpForce/2, FighterData.VerticalJumpForce/2);
            Context.JumpCount--;
        }

        //Animation & Effects
        Quaternion rotationX = Quaternion.Euler(90, 0, 0);
        Context.ParticlePool.SpawnFromPool(ParticleTypes.Jump, Context.GroundCheck.position, rotationX);

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
        else if(Context.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
    }

    public override void ExitState()
    {
        InputHandler.WasJumpPressed = false; // Reset the input flag
    }
}
